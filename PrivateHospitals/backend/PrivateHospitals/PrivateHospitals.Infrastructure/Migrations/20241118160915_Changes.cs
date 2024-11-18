using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f0d6f14-7e55-431e-a87a-d39f923f441d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c80f6a9f-054e-479e-9162-e97a76bac751");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d320224-f8d5-4cd4-9292-6e7dceaa516a", null, "Doctor", "DOCTOR" },
                    { "0f1f27fc-ca43-4e5c-acc4-4e23250e8166", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d320224-f8d5-4cd4-9292-6e7dceaa516a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f1f27fc-ca43-4e5c-acc4-4e23250e8166");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f0d6f14-7e55-431e-a87a-d39f923f441d", null, "Patient", "PATIENT" },
                    { "c80f6a9f-054e-479e-9162-e97a76bac751", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
