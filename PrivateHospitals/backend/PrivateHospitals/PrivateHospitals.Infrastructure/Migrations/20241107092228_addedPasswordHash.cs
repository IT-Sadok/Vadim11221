using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a4c8bdf-e1a4-4a7d-b811-8376458d5cb1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6947707e-78ca-46ab-b0fe-5cb052e41433");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Patients",
                newName: "PasswordHash");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bcb98b2-00fe-4bd0-b5ac-dad784d35464", null, "Patient", "PATIENT" },
                    { "d668987f-90b3-4e97-86c6-8f64b4c118d8", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bcb98b2-00fe-4bd0-b5ac-dad784d35464");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d668987f-90b3-4e97-86c6-8f64b4c118d8");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Patients",
                newName: "Password");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a4c8bdf-e1a4-4a7d-b811-8376458d5cb1", null, "Patient", "PATIENT" },
                    { "6947707e-78ca-46ab-b0fe-5cb052e41433", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
