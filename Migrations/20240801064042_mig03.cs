using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AccountUsers_AccountUserId1",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AccountUserId1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AccountUserId1",
                table: "Companies");

            migrationBuilder.AlterColumn<long>(
                name: "AccountUserId",
                table: "Companies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AccountUserId",
                table: "Companies",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AccountUsers_AccountUserId",
                table: "Companies",
                column: "AccountUserId",
                principalTable: "AccountUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AccountUsers_AccountUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AccountUserId",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "AccountUserId",
                table: "Companies",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
    }
}
