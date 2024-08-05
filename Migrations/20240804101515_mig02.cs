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
            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9315));

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9322));

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9083));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9375));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9512));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9517));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9519));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "WalletNumber" },
                values: new object[] { new DateTime(2024, 8, 4, 13, 8, 14, 483, DateTimeKind.Local).AddTicks(9435), new Guid("04bdf2c3-888a-4379-947c-b91a16046e0c") });
        }
    }
}
