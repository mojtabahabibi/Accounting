using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2735));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2741), "واریز به کیف پول" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "ModifiedBy", "ModifiedDate", "Title" },
                values: new object[,]
                {
                    { 4L, 0L, new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2742), null, null, null, null, null, "خرید از کیف پول" },
                    { 5L, 0L, new DateTime(2024, 8, 5, 13, 19, 19, 762, DateTimeKind.Local).AddTicks(2744), null, null, null, null, null, "مرجوعی" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8645));

            migrationBuilder.UpdateData(
                table: "AccountType",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8651));

            migrationBuilder.UpdateData(
                table: "AccountUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8425));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8777));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8888));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8844));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2024, 8, 5, 12, 30, 33, 805, DateTimeKind.Local).AddTicks(8846), "مرجوعی" });
        }
    }
}
