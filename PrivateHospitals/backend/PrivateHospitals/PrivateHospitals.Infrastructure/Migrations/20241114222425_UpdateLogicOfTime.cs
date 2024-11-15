using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogicOfTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01de6bbf-a9ae-45a6-8e79-e0ff40fe3057");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ea68a5d-702e-4855-b6bf-58514f8766a8");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WorkingHours",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bde33560-558d-4bb6-af62-0ec4320ebbbf", null, "Patient", "PATIENT" },
                    { "c7deb317-4492-4ab4-82f8-43bf61309c18", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bde33560-558d-4bb6-af62-0ec4320ebbbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7deb317-4492-4ab4-82f8-43bf61309c18");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "WorkingHours",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01de6bbf-a9ae-45a6-8e79-e0ff40fe3057", null, "Doctor", "DOCTOR" },
                    { "9ea68a5d-702e-4855-b6bf-58514f8766a8", null, "Patient", "PATIENT" }
                });
        }
    }
}
