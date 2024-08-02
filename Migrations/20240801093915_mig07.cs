using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountUserId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AccountUserId",
                table: "Payments",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AccountUsers_AccountUserId",
                table: "Payments",
                column: "AccountUserId",
                principalTable: "AccountUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AccountUsers_AccountUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AccountUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "Payments");
        }
    }
}
