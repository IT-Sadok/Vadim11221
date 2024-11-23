using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d320224-f8d5-4cd4-9292-6e7dceaa516a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f1f27fc-ca43-4e5c-acc4-4e23250e8166");

            migrationBuilder.DropColumn(
                name: "WorkingHoursJson",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                table: "AspNetUsers",
                type: "jsonb",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "131fc64b-3d70-4265-97bf-5a4a3cc66e89", null, "Patient", "PATIENT" },
                    { "a6aa9144-92d0-4bcd-bd21-43e345e5d126", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "131fc64b-3d70-4265-97bf-5a4a3cc66e89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6aa9144-92d0-4bcd-bd21-43e345e5d126");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "WorkingHoursJson",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d320224-f8d5-4cd4-9292-6e7dceaa516a", null, "Doctor", "DOCTOR" },
                    { "0f1f27fc-ca43-4e5c-acc4-4e23250e8166", null, "Patient", "PATIENT" }
                });
        }
    }
}
