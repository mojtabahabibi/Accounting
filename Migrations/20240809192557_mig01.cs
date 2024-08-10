using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_AccountUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYears",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_FinancialYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_InvoicePayTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
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
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
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
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Economicalnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AccountUsers_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "AccountUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUserId = table.Column<long>(type: "bigint", nullable: false),
                    AccountTypeId = table.Column<long>(type: "bigint", nullable: false),
                    WalletId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
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
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_AccountUsers_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "AccountUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUserId = table.Column<long>(type: "bigint", nullable: false),
                    AccountTransactionId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Off = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_AccountTransactions_AccountTransactionId",
                        column: x => x.AccountTransactionId,
                        principalTable: "AccountTransactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_AccountUsers_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "AccountUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountBooks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTransactionId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountBooks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    AccountTransactionId = table.Column<long>(type: "bigint", nullable: true),
                    InvoicePayTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
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
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AccountTransactions_AccountTransactionId",
                        column: x => x.AccountTransactionId,
                        principalTable: "AccountTransactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_InvoicePayTypes_InvoicePayTypeId",
                        column: x => x.InvoicePayTypeId,
                        principalTable: "InvoicePayTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Off = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Price = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
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
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AccountType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "ModifiedBy", "ModifiedDate", "Type" },
                values: new object[,]
                {
                    { 1L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 459, DateTimeKind.Local).AddTicks(565), null, null, null, null, null, "حساب نقدی" },
                    { 2L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 459, DateTimeKind.Local).AddTicks(574), null, null, null, null, null, "حساب کیف پول" }
                });

            migrationBuilder.InsertData(
                table: "AccountUsers",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "ModifiedBy", "ModifiedDate", "Password", "UserName" },
                values: new object[] { 1L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 459, DateTimeKind.Local).AddTicks(1009), null, null, null, null, null, "123456", "Company" });

            migrationBuilder.InsertData(
                table: "InvoicePayTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "ModifiedBy", "ModifiedDate", "Type" },
                values: new object[,]
                {
                    { 1L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 460, DateTimeKind.Local).AddTicks(7412), null, null, null, null, null, "نقدی" },
                    { 2L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 460, DateTimeKind.Local).AddTicks(7420), null, null, null, null, null, "کیف پول" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "ModifiedBy", "ModifiedDate", "Name", "Price" },
                values: new object[] { 1L, "1", 0L, new DateTime(2024, 8, 9, 22, 55, 54, 460, DateTimeKind.Local).AddTicks(7910), null, null, null, null, null, "خرید شارژ", 1000L });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "ModifiedBy", "ModifiedDate", "Title" },
                values: new object[,]
                {
                    { 1L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 461, DateTimeKind.Local).AddTicks(5630), null, null, null, null, null, "واریز به حساب نقدی" },
                    { 2L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 461, DateTimeKind.Local).AddTicks(5640), null, null, null, null, null, "خرید از حساب نقدی" },
                    { 3L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 461, DateTimeKind.Local).AddTicks(5641), null, null, null, null, null, "واریز به حساب کیف پول" },
                    { 4L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 461, DateTimeKind.Local).AddTicks(5642), null, null, null, null, null, "خرید از حساب کیف پول" },
                    { 5L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 461, DateTimeKind.Local).AddTicks(5644), null, null, null, null, null, "مرجوعی" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountTypeId", "AccountUserId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "ModifiedBy", "ModifiedDate", "Title", "WalletId" },
                values: new object[] { 1L, "79971757", 1L, 1L, 0L, new DateTime(2024, 8, 9, 22, 55, 54, 457, DateTimeKind.Local).AddTicks(1548), null, null, null, null, null, "حساب نقدی صندوق", null });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBooks_AccountId",
                table: "AccountBooks",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBooks_AccountTransactionId",
                table: "AccountBooks",
                column: "AccountTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountUserId",
                table: "Accounts",
                column: "AccountUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_WalletId",
                table: "Accounts",
                column: "WalletId",
                unique: true,
                filter: "[WalletId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_TransactionTypeId",
                table: "AccountTransactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AccountUserId",
                table: "Companies",
                column: "AccountUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ItemId",
                table: "InvoiceItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AccountTransactionId",
                table: "Invoices",
                column: "AccountTransactionId",
                unique: true,
                filter: "[AccountTransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AccountUserId",
                table: "Invoices",
                column: "AccountUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AccountId",
                table: "Payments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AccountTransactionId",
                table: "Payments",
                column: "AccountTransactionId",
                unique: true,
                filter: "[AccountTransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoicePayTypeId",
                table: "Payments",
                column: "InvoicePayTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBooks");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "FinancialYears");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "InvoicePayTypes");

            migrationBuilder.DropTable(
                name: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "AccountUsers");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}
