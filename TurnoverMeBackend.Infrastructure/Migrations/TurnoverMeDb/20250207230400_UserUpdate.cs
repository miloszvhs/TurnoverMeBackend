using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurnoverMeBackend.Infrastructure.Migrations.TurnoverMeDb
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "InvoiceCircuit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    InvoiceId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCircuit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceCircuit_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ac18572-0002-4f7b-b574-df5f8dd4c22f", null, "User", "USER" },
                    { "da71242b-b1f8-4321-a5b9-d810545457df", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ec8f2759-565f-47a0-bd2d-623f6a88064b", 0, "7a5b4d6a-1a45-402f-bae5-82427abb52fb", "ApplicationUser", "admin@admin.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEItBN8dYy1W5f5pTPJplJp5xTNtVPf9etj+rEmPxrGGbCuWowWOCaGVZNKaIViPT4g==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "da71242b-b1f8-4321-a5b9-d810545457df", "ec8f2759-565f-47a0-bd2d-623f6a88064b" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceCircuit_ApplicationUserId",
                table: "InvoiceCircuit",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceCircuit");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ac18572-0002-4f7b-b574-df5f8dd4c22f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "da71242b-b1f8-4321-a5b9-d810545457df", "ec8f2759-565f-47a0-bd2d-623f6a88064b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da71242b-b1f8-4321-a5b9-d810545457df");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ec8f2759-565f-47a0-bd2d-623f6a88064b");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7d4256a2-07eb-4aa7-9c60-33557e56e711", null, "User", "USER" },
                    { "ba848199-5e5f-415b-ba24-af4eb8ea66b3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4ad1383d-2aae-42a7-a43a-a35c373d5806", 0, "f722c8ac-1a1c-41c7-8f4f-526a594503d1", "admin@admin.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEPfmsUb5Soxv6f/tgYIaMZ8xEj8JSl13x64bsnVN6dUw+8PyBL3TLQtX1857+wdL3A==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ba848199-5e5f-415b-ba24-af4eb8ea66b3", "4ad1383d-2aae-42a7-a43a-a35c373d5806" });
        }
    }
}
