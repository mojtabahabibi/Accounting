using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountUserId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "AccountUserId1",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AccountUserId1",
                table: "Companies",
                column: "AccountUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AccountUsers_AccountUserId1",
                table: "Companies",
                column: "AccountUserId1",
                principalTable: "AccountUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AccountUsers_AccountUserId1",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AccountUserId1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AccountUserId1",
                table: "Companies");
        }
    }
}
