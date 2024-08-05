using System;
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
            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4081));

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4088));

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(3708));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Amount", "CreatedDate" },
                values: new object[] { 0L, new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4143) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Amount", "CreatedDate" },
                values: new object[] { 0L, new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4149) });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4275));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4280));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4281));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "WalletNumber" },
                values: new object[] { new DateTime(2024, 8, 4, 13, 52, 54, 113, DateTimeKind.Local).AddTicks(4199), new Guid("a3c61dc6-e57c-4573-ac09-bb6a8a69d87b") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8366));

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8002));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8455));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8462));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8630));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8637));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "WalletNumber" },
                values: new object[] { new DateTime(2024, 8, 4, 13, 45, 12, 438, DateTimeKind.Local).AddTicks(8531), new Guid("82859d1f-5239-4042-9f11-3bcbe7688b4d") });
        }
    }
}
