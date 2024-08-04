using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ComapnyId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8285));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8528));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8667));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "WalletNumber" },
                values: new object[] { new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8585), new Guid("06aa9a49-7425-47af-8329-c76ed57a4137") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ComapnyId",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Invoices",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 28, 26, 798, DateTimeKind.Local).AddTicks(4293));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 28, 26, 798, DateTimeKind.Local).AddTicks(4544));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 28, 26, 798, DateTimeKind.Local).AddTicks(4666));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 3, 12, 28, 26, 798, DateTimeKind.Local).AddTicks(4671));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "WalletNumber" },
                values: new object[] { new DateTime(2024, 8, 3, 12, 28, 26, 798, DateTimeKind.Local).AddTicks(4594), new Guid("75c70a3b-ea93-4b56-a407-82369a765102") });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
