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
            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8650));

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8657));

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8427));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8711));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8831));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8768), "واریز به حساب نقدی" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8773), "خرید از حساب نقدی" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8774), "واریز به حساب کیف پول" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8776), "خرید از حساب کیف پول" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 22, 58, 637, DateTimeKind.Local).AddTicks(8777));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2631));

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2636));

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2405));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2684));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2796));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2735), "واریز به حساب" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2740), "خرید از حساب" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2741), "واریز به کیف پول" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2742), "خرید از کیف پول" });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2744));
        }
    }
}
