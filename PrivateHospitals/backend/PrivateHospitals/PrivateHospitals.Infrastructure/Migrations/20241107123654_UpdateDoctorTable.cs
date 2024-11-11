using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "104e6dee-fb3e-4452-a0a4-1612ab9129b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fadd080-db5a-4f30-b181-2aa90daa8648");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Doctors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9d138728-ca4a-4716-ae40-f9e039a3c18b", null, "Patient", "PATIENT" },
                    { "e79a1c79-ee3c-4661-b84c-03061145ee98", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d138728-ca4a-4716-ae40-f9e039a3c18b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a1c79-ee3c-4661-b84c-03061145ee98");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Doctors");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "104e6dee-fb3e-4452-a0a4-1612ab9129b8", null, "Doctor", "DOCTOR" },
                    { "6fadd080-db5a-4f30-b181-2aa90daa8648", null, "Patient", "PATIENT" }
                });
        }
    }
}
