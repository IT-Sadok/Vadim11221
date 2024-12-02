using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "2794bf9a-fa79-4b07-abb8-6664942c57a0", null, "Doctor", "DOCTOR" },
                    { "f481cf3c-c2e3-45ad-862a-9a16c84796ff", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2794bf9a-fa79-4b07-abb8-6664942c57a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f481cf3c-c2e3-45ad-862a-9a16c84796ff");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "421b70f6-d49a-4244-947c-58433e1d3fe8", null, "Patient", "PATIENT" },
                    { "9908d014-c99a-4947-8bd3-beae1a4d0c61", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
