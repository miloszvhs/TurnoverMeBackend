using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceHistoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceApprovalsHistories_Invoices_InvoiceId",
                table: "InvoiceApprovalsHistories");
            
            migrationBuilder.AlterColumn<string>(
                name: "InvoiceId",
                table: "InvoiceApprovalsHistories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
            
            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceApprovalsHistories_Invoices_InvoiceId",
                table: "InvoiceApprovalsHistories",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "InvoiceApprovalId",
                table: "InvoiceApprovalsHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceApprovalsHistories_Invoices_InvoiceId",
                table: "InvoiceApprovalsHistories");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceId",
                table: "InvoiceApprovalsHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceApprovalId",
                table: "InvoiceApprovalsHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceApprovalsHistories_Invoices_InvoiceId",
                table: "InvoiceApprovalsHistories",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
