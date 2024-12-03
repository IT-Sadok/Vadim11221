using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b9cbaea-1d4e-40a1-9da7-b21dba4b425a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c921064e-1f6d-4f5e-ab3f-5f4237a97dc8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "421b70f6-d49a-4244-947c-58433e1d3fe8", null, "Patient", "PATIENT" },
                    { "9908d014-c99a-4947-8bd3-beae1a4d0c61", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "421b70f6-d49a-4244-947c-58433e1d3fe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9908d014-c99a-4947-8bd3-beae1a4d0c61");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b9cbaea-1d4e-40a1-9da7-b21dba4b425a", null, "Patient", "PATIENT" },
                    { "c921064e-1f6d-4f5e-ab3f-5f4237a97dc8", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
