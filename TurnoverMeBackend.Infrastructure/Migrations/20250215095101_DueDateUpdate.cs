using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DueDateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "InvoiceApprovals",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { "212d06e7-7463-49a3-8a34-53b513de5cb7", "UsersGroup" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "InvoiceApprovals");
            
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { "78ea8df4-b734-4c64-95e0-af88ce24495d", "UsersGroup" });
        }
    }
}
