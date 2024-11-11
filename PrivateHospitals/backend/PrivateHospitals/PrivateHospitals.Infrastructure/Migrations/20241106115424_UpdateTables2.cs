using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19a7ff2d-45a7-41ae-b557-298f7098b985");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2496edfc-fe86-4549-bb38-3a515f7d1fbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61184d91-29d6-40dd-ac55-1b8b0de22d41");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "19a7ff2d-45a7-41ae-b557-298f7098b985", null, "Admin", "ADMIN" },
                    { "2496edfc-fe86-4549-bb38-3a515f7d1fbf", null, "Doctor", "DOCTOR" },
                    { "61184d91-29d6-40dd-ac55-1b8b0de22d41", null, "Patient", "PATIENT" }
                });
        }
    }
}
