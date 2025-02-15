using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurnoverMeBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForcePasswordChangeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForcePasswordChange",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForcePasswordChange",
                table: "AspNetUsers");

        }
    }
}
