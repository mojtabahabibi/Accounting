using System;
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
            migrationBuilder.AddColumn<long>(
                name: "TransactionTypeId",
                table: "AccountTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 16, 20, 758, DateTimeKind.Local).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 16, 20, 758, DateTimeKind.Local).AddTicks(2118));

            migrationBuilder.UpdateData(
                table: "TransactionType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 16, 20, 758, DateTimeKind.Local).AddTicks(2248));

            migrationBuilder.UpdateData(
                table: "TransactionType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 16, 20, 758, DateTimeKind.Local).AddTicks(2253));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "WalletNumber" },
                values: new object[] { new DateTime(2024, 8, 3, 12, 16, 20, 758, DateTimeKind.Local).AddTicks(2173), new Guid("6eabd196-a1f1-483f-bd17-5fe8d5aa6f2b") });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_TransactionTypeId",
                table: "AccountTransactions",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_TransactionType_TransactionTypeId",
                table: "AccountTransactions",
                column: "TransactionTypeId",
                principalTable: "TransactionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_TransactionType_TransactionTypeId",
                table: "AccountTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_TransactionTypeId",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "AccountTransactions");

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 11, 44, 34, 407, DateTimeKind.Local).AddTicks(8340));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 11, 44, 34, 407, DateTimeKind.Local).AddTicks(8598));

            migrationBuilder.UpdateData(
                table: "TransactionType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 11, 44, 34, 410, DateTimeKind.Local).AddTicks(3661));

            migrationBuilder.UpdateData(
                table: "TransactionType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 11, 44, 34, 410, DateTimeKind.Local).AddTicks(3674));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "WalletNumber" },
                values: new object[] { new DateTime(2024, 8, 3, 11, 44, 34, 407, DateTimeKind.Local).AddTicks(8651), new Guid("ee920e73-a8b2-41d1-b205-fd61138db220") });
        }
    }
}
