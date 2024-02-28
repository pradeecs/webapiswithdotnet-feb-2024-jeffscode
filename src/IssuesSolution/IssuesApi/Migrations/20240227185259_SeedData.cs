using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssuesApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SoftwareCatalog",
                columns: new[] { "Id", "DateAdded", "DateRetired", "RetirementNotificationsSent", "Title", "Version" },
                values: new object[] { new Guid("c8454e3c-406c-43f8-9c94-2cc4ab639487"), new DateTimeOffset(new DateTime(2024, 2, 27, 13, 52, 59, 147, DateTimeKind.Unspecified).AddTicks(9385), new TimeSpan(0, -5, 0, 0, 0)), null, false, "Microsoft Word", "97" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SoftwareCatalog",
                keyColumn: "Id",
                keyValue: new Guid("c8454e3c-406c-43f8-9c94-2cc4ab639487"));
        }
    }
}
