namespace IssuesApi.Features.Catalog;

// Before we got here, ASP.NET CORE create a scope, created our controller with it, and the softwarecatalog manager, and the datacontext
public class SoftwareCatalogController(IManageTheSoftwareCatalog catalog, ILogger<SoftwareCatalogController> logger) : ControllerBase
{

    [HttpGet("/software")]
    public async Task<ActionResult<CollectionResponse<SoftwareCatalogSummaryResponseItem>>> GetAllSoftwareAsync(CancellationToken token)
    {
        var data = await catalog.GetAllCurrentSoftwareAsync(token);
        return Ok(data);
    }

    [HttpDelete("/software/{id:guid}")]
    public async Task<ActionResult> RemoveSoftwareItemAsync(Guid id, CancellationToken token)
    {
        await catalog.RetireSoftwareItemAsync(id, token);

        return NoContent(); // It worked, not much to say about "fine"
    }

    [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Any)]
    [HttpPost("/software")]
    public async Task<ActionResult> AddSoftwareItemAsync([FromBody] SoftwareItemRequestModel request, CancellationToken token)
    {
        // todo: 1) validate it. 2) save it to the database 3) send a response.
        SoftwareCatalogSummaryResponseItem response = await catalog.AddSoftwareItemAsync(request, token);

        // 201, with a Location Header, and an entity (data) that is EXACTLY the same as they'd get if they did the get request to that location
        return CreatedAtRoute("softwarecatalog#getsoftwareitembyid", new { id = response.Id }, response);
    }

    [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Any)]
    [HttpGet("/software/{id:guid}", Name = "softwarecatalog#getsoftwareitembyid")]
    public async Task<ActionResult> GetSoftwareItemByIdAsync(Guid id, CancellationToken token)
    {
        logger.LogInformation("Looking up software item {id}", id);
        SoftwareCatalogSummaryResponseItem? item = await catalog.GetItemByIdAsync(id, token);

        if (item is not null)
        {
            return Ok(item);
        }
        else
        {
            return NotFound();
        }
    }
}
