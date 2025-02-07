using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCircuits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceBuyer_Invoices_InvoiceId",
                table: "InvoiceBuyer");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceReceiver_Invoices_InvoiceId",
                table: "InvoiceReceiver");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSeller_Invoices_InvoiceId",
                table: "InvoiceSeller");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Invoices",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Invoices",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3);

            migrationBuilder.CreateTable(
                name: "InvoiceCircuit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    InvoiceId = table.Column<string>(type: "text", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCircuit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceCircuit_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceCircuit_InvoiceId",
                table: "InvoiceCircuit",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceBuyer_Invoices_InvoiceId",
                table: "InvoiceBuyer",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceReceiver_Invoices_InvoiceId",
                table: "InvoiceReceiver",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSeller_Invoices_InvoiceId",
                table: "InvoiceSeller",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceBuyer_Invoices_InvoiceId",
                table: "InvoiceBuyer");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceReceiver_Invoices_InvoiceId",
                table: "InvoiceReceiver");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSeller_Invoices_InvoiceId",
                table: "InvoiceSeller");

            migrationBuilder.DropTable(
                name: "InvoiceCircuit");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Invoices",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Invoices",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceBuyer_Invoices_InvoiceId",
                table: "InvoiceBuyer",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceReceiver_Invoices_InvoiceId",
                table: "InvoiceReceiver",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSeller_Invoices_InvoiceId",
                table: "InvoiceSeller",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
