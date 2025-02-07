using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRemarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmountDue",
                table: "Invoices",
                newName: "TotalGrossAmount");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Invoices",
                type: "text",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "TotalGrossAmount",
                table: "Invoices",
                newName: "TotalAmountDue");
        }
    }
}
