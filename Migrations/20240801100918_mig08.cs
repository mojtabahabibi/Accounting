using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Invoices_InvoiceId",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AccountTransactions");

            migrationBuilder.AlterColumn<long>(
                name: "InvoiceId",
                table: "AccountTransactions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PaymentId",
                table: "AccountTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_PaymentId",
                table: "AccountTransactions",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Invoices_InvoiceId",
                table: "AccountTransactions",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Payments_PaymentId",
                table: "AccountTransactions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Invoices_InvoiceId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Payments_PaymentId",
                table: "AccountTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_PaymentId",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "AccountTransactions");

            migrationBuilder.AlterColumn<long>(
                name: "InvoiceId",
                table: "AccountTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "AccountTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Invoices_InvoiceId",
                table: "AccountTransactions",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
