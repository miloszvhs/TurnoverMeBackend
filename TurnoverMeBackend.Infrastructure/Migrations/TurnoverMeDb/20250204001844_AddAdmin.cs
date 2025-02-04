using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurnoverMeBackend.Infrastructure.Migrations.TurnoverMeDb
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4ad1383d-2aae-42a7-a43a-a35c373d5806", 0, "f722c8ac-1a1c-41c7-8f4f-526a594503d1", "admin@admin.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEPfmsUb5Soxv6f/tgYIaMZ8xEj8JSl13x64bsnVN6dUw+8PyBL3TLQtX1857+wdL3A==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "20a5afba-7c59-4811-8a9d-efd545ec23d5", "4ad1383d-2aae-42a7-a43a-a35c373d5806" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d4256a2-07eb-4aa7-9c60-33557e56e711");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ba848199-5e5f-415b-ba24-af4eb8ea66b3", "4ad1383d-2aae-42a7-a43a-a35c373d5806" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba848199-5e5f-415b-ba24-af4eb8ea66b3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4ad1383d-2aae-42a7-a43a-a35c373d5806");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f61f8b1-42c7-4b2b-ab5e-eae163641f7c", null, "User", "USER" },
                    { "8782de60-10db-4437-af57-698f7c9e383c", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "65151b20-6f5f-4ba9-b0df-dfcb6dad4869", 0, "35edf290-5604-448e-b062-039d8b995bcc", "admin@admin.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEAdcr42D2GgRbPZ35JsH9I7wAfUrVmJNjPhs+yFaA2m6Xs8jfobdkPACojbxyFL9hw==", null, false, "", false, "admin" });
        }
    }
}
