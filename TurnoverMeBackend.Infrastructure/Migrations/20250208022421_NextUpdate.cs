using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                table: "Invoices",
                newName: "CreationTime");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InvoiceCircuit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "ApplicationUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "ApplicationUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InvoiceCircuit");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Invoices",
                newName: "DeliveryDate");
        }
    }
}
