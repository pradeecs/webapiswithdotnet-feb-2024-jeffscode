using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssuesApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoftwareCatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateRetired = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    RetirementNotificationsSent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareCatalog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoftwareCatalog");
        }
    }
}
