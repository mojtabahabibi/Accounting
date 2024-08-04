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
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_TransactionType_TransactionTypeId",
                table: "AccountTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionType",
                table: "TransactionType");

            migrationBuilder.RenameTable(
                name: "TransactionType",
                newName: "TransactionTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionTypes",
                table: "TransactionTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccountBooks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<long>(type: "bigint", nullable: false),
                    AccountTransactionId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBooks_AccountTransactions_AccountTransactionId",
                        column: x => x.AccountTransactionId,
                        principalTable: "AccountTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountBooks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AccountBooks_AccountId",
                table: "AccountBooks",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBooks_AccountTransactionId",
                table: "AccountBooks",
                column: "AccountTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_TransactionTypes_TransactionTypeId",
                table: "AccountTransactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_TransactionTypes_TransactionTypeId",
                table: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "AccountBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionTypes",
                table: "TransactionTypes");

            migrationBuilder.RenameTable(
                name: "TransactionTypes",
                newName: "TransactionType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionType",
                table: "TransactionType",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_TransactionType_TransactionTypeId",
                table: "AccountTransactions",
                column: "TransactionTypeId",
                principalTable: "TransactionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
