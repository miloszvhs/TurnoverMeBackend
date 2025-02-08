using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurnoverMeBackend.Infrastructure.Migrations.TurnoverMeDb
{
    /// <inheritdoc />
    public partial class NextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "InvoiceCircuit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "791689f4-8409-4eca-8452-fa1660495a08", null, "User", "USER" },
                    { "dda8fa89-e050-4217-82fe-1250bc051938", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BranchId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "GroupId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "da6da752-040a-4350-8175-6c04bedd0441", 0, 0, "97464b2d-6a45-4800-9e7a-129bcf3817d6", "ApplicationUser", "admin@admin.com", true, 0, false, null, null, null, "AQAAAAIAAYagAAAAEOsyexF1vFBy/wtFTIpXd74a75FSWO/o/yhwYtzcNJq4ICmZeZJcuqHltPfeHW52XA==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda8fa89-e050-4217-82fe-1250bc051938", "da6da752-040a-4350-8175-6c04bedd0441" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "791689f4-8409-4eca-8452-fa1660495a08");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda8fa89-e050-4217-82fe-1250bc051938", "da6da752-040a-4350-8175-6c04bedd0441" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dda8fa89-e050-4217-82fe-1250bc051938");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da6da752-040a-4350-8175-6c04bedd0441");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InvoiceCircuit");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetUsers");

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
        }
    }
}
