using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52e88e85-978a-425d-803f-a784c8cdbdcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96e748e0-a1f6-4b14-bb79-2399eb75c25a");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Appointments",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01de6bbf-a9ae-45a6-8e79-e0ff40fe3057", null, "Doctor", "DOCTOR" },
                    { "9ea68a5d-702e-4855-b6bf-58514f8766a8", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52e88e85-978a-425d-803f-a784c8cdbdcf", null, "Doctor", "DOCTOR" },
                    { "96e748e0-a1f6-4b14-bb79-2399eb75c25a", null, "Patient", "PATIENT" }
                });
        }
    }
}
