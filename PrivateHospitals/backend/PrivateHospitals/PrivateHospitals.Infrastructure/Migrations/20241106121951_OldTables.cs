using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class OldTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69e40086-a998-4a97-818d-6e5f507054e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9243335-73e8-4a80-b902-c65195a36948");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c20a2a61-75d3-4550-a9e7-8205755b27fc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72df2287-8fe3-438f-a7be-496b9561bec0", null, "Admin", "ADMIN" },
                    { "7b68c474-5e94-40d1-bfcc-5f0d45a5538d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72df2287-8fe3-438f-a7be-496b9561bec0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b68c474-5e94-40d1-bfcc-5f0d45a5538d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69e40086-a998-4a97-818d-6e5f507054e6", null, "Doctor", "DOCTOR" },
                    { "b9243335-73e8-4a80-b902-c65195a36948", null, "Admin", "ADMIN" },
                    { "c20a2a61-75d3-4550-a9e7-8205755b27fc", null, "Patient", "PATIENT" }
                });
        }
    }
}
