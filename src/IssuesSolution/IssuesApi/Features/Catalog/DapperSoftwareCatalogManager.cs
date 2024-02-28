
using Dapper;
using Npgsql;
using System.Data;
using System.Threading.Channels;

namespace IssuesApi.Features.Catalog;

public class DapperSoftwareCatalogManager : IManageTheSoftwareCatalog
{
    private readonly IDbConnection _connection;
    private readonly Channel<Guid> _channel;
    public DapperSoftwareCatalogManager(string connectionString, Channel<Guid> channel)
    {
        _connection = new NpgsqlConnection(connectionString);
        _channel = channel;
    }
    public async Task<SoftwareCatalogSummaryResponseItem> AddSoftwareItemAsync(SoftwareItemRequestModel request, CancellationToken token)
    {
        var insertSql = """
            INSERT INTO "SoftwareCatalog" ("Id", "DateAdded",  "RetirementNotificationsSent", "Title", "Version")
            VALUES (@Id, @DateAdded, @RetirementNotificationSent, @Title, @Version);
            """;


        var selectSql = """ 
            SELECT s."Id", s."Title" || ' ' || s."Version" AS "Title", s."Version"
            FROM "SoftwareCatalog" AS s
            WHERE s."DateRetired" IS NULL AND s."Id" = @id
            LIMIT 2
            """;
        var parameters = new
        {
            Id = Guid.NewGuid(),
            DateAdded = DateTimeOffset.UtcNow,

            RetirementNotificationSent = false,
            Title = request.Title,
            Version = request.Version,
        };

        await _connection.ExecuteAsync(insertSql, parameters);
        var response = await _connection.QuerySingleOrDefaultAsync<SoftwareCatalogSummaryResponseItem>(selectSql, new { id = parameters.Id });
        if (response is null)
        {
            throw new Exception("Something is wrong with the database... can't save stuff");
        }
        return response;
    }

    public async Task<CollectionResponse<SoftwareCatalogSummaryResponseItem>> GetAllCurrentSoftwareAsync(CancellationToken token)
    {
        var sql = """ 
            SELECT s."Id", s."Title" || ' ' || s."Version" AS "Title", s."Version"
            FROM "SoftwareCatalog" AS s
            WHERE s."DateRetired" IS NULL
            """;
        var items = await _connection.QueryAsync<SoftwareCatalogSummaryResponseItem>(sql);
        return new CollectionResponse<SoftwareCatalogSummaryResponseItem>(items.ToList());
    }

    public async Task<SoftwareCatalogSummaryResponseItem?> GetItemByIdAsync(Guid id, CancellationToken token)
    {

        var sql = """
            SELECT s."Id", s."Title" || ' ' || s."Version" AS "Title", s."Version"
            FROM "SoftwareCatalog" AS s
            WHERE s."DateRetired" IS NULL AND s."Id" = @id
            LIMIT 2
            """;
        var response = await _connection.QuerySingleOrDefaultAsync<SoftwareCatalogSummaryResponseItem>(sql, new { id });
        return response;
    }

    public async Task RetireSoftwareItemAsync(Guid id, CancellationToken token)
    {
        var sql = """
            UPDATE "SoftwareCatalog" SET "DateRetired" = @now
            WHERE "Id" = @id AND "DateRetired" IS NULL
            """;
        while (await _channel.Writer.WaitToWriteAsync(token))
        {
            if (_channel.Writer.TryWrite(id))
            {
                break;
            }
        }
        await _connection.ExecuteAsync(sql, new { id = id, now = DateTimeOffset.UtcNow });
    }
}
