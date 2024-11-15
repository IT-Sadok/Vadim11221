using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogicOfTime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bde33560-558d-4bb6-af62-0ec4320ebbbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7deb317-4492-4ab4-82f8-43bf61309c18");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "WorkingHours",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Appointments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15ad20ff-62ee-4e9f-80d2-d695e62574c1", null, "Doctor", "DOCTOR" },
                    { "34b4234a-0317-49eb-94ba-26ebf2fa0335", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15ad20ff-62ee-4e9f-80d2-d695e62574c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34b4234a-0317-49eb-94ba-26ebf2fa0335");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "WorkingHours",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bde33560-558d-4bb6-af62-0ec4320ebbbf", null, "Patient", "PATIENT" },
                    { "c7deb317-4492-4ab4-82f8-43bf61309c18", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
