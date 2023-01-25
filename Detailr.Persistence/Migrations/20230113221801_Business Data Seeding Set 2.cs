using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Detailr.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BusinessDataSeedingSet2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "Description", "LastModifiedBy", "LastModifiedDate", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Business 2 Description", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 51.507969111981254, -0.021353916973630996, "Test Business 2" },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Business 3 Description", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 51.508883280810551, -0.07323854928618917, "Test Business 3" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Business 4 Description", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 51.458805203539583, -0.10797751340028962, "Test Business 4" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Business 5 Description", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 51.767187701669833, -0.3409916280412093, "Test Business 5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
