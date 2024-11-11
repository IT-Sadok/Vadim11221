using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MedicalCard_MedicalCardId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MedicalCard");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MedicalCardId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "860a79e4-21c7-496b-9396-16e3aa57134f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8691eecc-01c5-4669-b29c-ec90e28ea0e8");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MedicalCardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a06c75f-b301-4d40-a173-2879504f2fb0", null, "Doctor", "DOCTOR" },
                    { "9f72a658-98a6-440b-8396-040cc00015c0", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a06c75f-b301-4d40-a173-2879504f2fb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f72a658-98a6-440b-8396-040cc00015c0");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MedicalCardId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Speciality",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicalCard",
                columns: table => new
                {
                    MedicalCardId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<string>(type: "text", nullable: false),
                    PatientId = table.Column<string>(type: "text", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCard", x => x.MedicalCardId);
                    table.ForeignKey(
                        name: "FK_MedicalCard_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalCard_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "860a79e4-21c7-496b-9396-16e3aa57134f", null, "Patient", "PATIENT" },
                    { "8691eecc-01c5-4669-b29c-ec90e28ea0e8", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MedicalCardId",
                table: "AspNetUsers",
                column: "MedicalCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCard_DoctorId",
                table: "MedicalCard",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCard_PatientId",
                table: "MedicalCard",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MedicalCard_MedicalCardId",
                table: "AspNetUsers",
                column: "MedicalCardId",
                principalTable: "MedicalCard",
                principalColumn: "MedicalCardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
