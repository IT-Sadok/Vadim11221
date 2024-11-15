using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46025930-082c-49a1-9728-d3afa5840528");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3ec4232-5ea5-40f7-a539-fa00fe9f6d1b");

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    HoursId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.HoursId);
                    table.ForeignKey(
                        name: "FK_WorkingHours_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e139008-c270-4a2e-a0de-aa99dd349da8", null, "Patient", "PATIENT" },
                    { "a5e66940-c292-4dfd-b9f1-1957ce14307a", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_DoctorId",
                table: "WorkingHours",
                column: "DoctorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingHours");

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
                    { "46025930-082c-49a1-9728-d3afa5840528", null, "Patient", "PATIENT" },
                    { "d3ec4232-5ea5-40f7-a539-fa00fe9f6d1b", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
