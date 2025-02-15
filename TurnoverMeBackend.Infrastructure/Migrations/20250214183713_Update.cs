using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "BFE154C0-CB46-4E46-B2B5-1419BE462FB4", "66b870ea-1c7b-4f2d-ad41-c97468f29f2c" });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "13f40632-f8ae-4162-91ce-f7007efe6d22");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66b870ea-1c7b-4f2d-ad41-c97468f29f2c");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "GroupId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiry", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "066988c9-0aae-4bac-b28b-21bd21fef4a2", 0, "f1ef1c88-c7c1-4591-884a-992396c42ffd", "admin@admin.com", true, null, false, null, null, null, "AQAAAAIAAYagAAAAEEtXZJkApijxTfb/8mGKckaVkDjfN1/DlmauB4zjiGBOouAwIudyzHUSAPPDCcCk8A==", null, false, null, null, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { "78ea8df4-b734-4c64-95e0-af88ce24495d", "UsersGroup" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "BFE154C0-CB46-4E46-B2B5-1419BE462FB4", "066988c9-0aae-4bac-b28b-21bd21fef4a2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "BFE154C0-CB46-4E46-B2B5-1419BE462FB4", "066988c9-0aae-4bac-b28b-21bd21fef4a2" });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: "78ea8df4-b734-4c64-95e0-af88ce24495d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "066988c9-0aae-4bac-b28b-21bd21fef4a2");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "GroupId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "66b870ea-1c7b-4f2d-ad41-c97468f29f2c", 0, "b8c0f437-d02b-48be-a843-2fc57f7e6f17", "admin@admin.com", true, null, false, null, null, null, "AQAAAAIAAYagAAAAENCB1Rgacs5jfv39zpB6K/zR31dk5M1DjG/65N0GDnghFgZLMm9x7ig+ehgur30drg==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { "13f40632-f8ae-4162-91ce-f7007efe6d22", "UsersGroup" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "BFE154C0-CB46-4E46-B2B5-1419BE462FB4", "66b870ea-1c7b-4f2d-ad41-c97468f29f2c" });
        }
    }
}
