using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkingHours2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e139008-c270-4a2e-a0de-aa99dd349da8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5e66940-c292-4dfd-b9f1-1957ce14307a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52e88e85-978a-425d-803f-a784c8cdbdcf", null, "Doctor", "DOCTOR" },
                    { "96e748e0-a1f6-4b14-bb79-2399eb75c25a", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52e88e85-978a-425d-803f-a784c8cdbdcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96e748e0-a1f6-4b14-bb79-2399eb75c25a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e139008-c270-4a2e-a0de-aa99dd349da8", null, "Patient", "PATIENT" },
                    { "a5e66940-c292-4dfd-b9f1-1957ce14307a", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
