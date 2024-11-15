using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrivateHospitals.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogicOfTime3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingHours_DoctorId",
                table: "WorkingHours");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15ad20ff-62ee-4e9f-80d2-d695e62574c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34b4234a-0317-49eb-94ba-26ebf2fa0335");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a2ad768e-fad7-47e1-b9eb-4603397c45b1", null, "Doctor", "DOCTOR" },
                    { "f4b0bfbd-43bb-4949-9781-b799f3ce2a94", null, "Patient", "PATIENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_DoctorId",
                table: "WorkingHours",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingHours_DoctorId",
                table: "WorkingHours");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2ad768e-fad7-47e1-b9eb-4603397c45b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4b0bfbd-43bb-4949-9781-b799f3ce2a94");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15ad20ff-62ee-4e9f-80d2-d695e62574c1", null, "Doctor", "DOCTOR" },
                    { "34b4234a-0317-49eb-94ba-26ebf2fa0335", null, "Patient", "PATIENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_DoctorId",
                table: "WorkingHours",
                column: "DoctorId",
                unique: true);
        }
    }
}
