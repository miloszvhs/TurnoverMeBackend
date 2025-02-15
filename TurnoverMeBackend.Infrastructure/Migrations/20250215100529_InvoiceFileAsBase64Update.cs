using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceFileAsBase64Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceFileAsBase64",
                table: "Invoices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceFileAsBase64",
                table: "Invoices");
        }
    }
}
