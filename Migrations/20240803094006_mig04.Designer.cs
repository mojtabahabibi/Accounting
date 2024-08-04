﻿// <auto-generated />
using System;
using EcoBar.Accounting.Data.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcoBar.Accounting.Migrations
{
    [DbContext(typeof(AccountingDbContext))]
    [Migration("20240803094006_mig04")]
    partial class mig04
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("AccountUserId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccountNumber = "123",
                            AccountUserId = 1L,
                            CreatedBy = 0L,
                            CreatedDate = new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8528),
                            Title = "حساب صندوق"
                        });
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.AccountBook", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("AccountTransactionId")
                        .HasColumnType("bigint");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountTransactionId");

                    b.ToTable("AccountBooks");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.AccountTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("InvoiceId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("PaymentId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TransactionNumber")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("TransactionTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("AccountTransactions");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.AccountUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountUsers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedBy = 0L,
                            CreatedDate = new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8285),
                            Name = "Company",
                            Password = "123456",
                            UserName = "Company"
                        });
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.AccountingFinancialYear", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsClose")
                        .HasColumnType("bit");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FinancialYears");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountUserId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Economicalnumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Invoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountUserId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Off")
                        .HasColumnType("bigint");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.InvoiceItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long>("InvoiceId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Off")
                        .HasColumnType("bigint");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ItemId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountUserId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.TransactionType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedBy = 0L,
                            CreatedDate = new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8667),
                            Title = "واریز به حساب"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedBy = 0L,
                            CreatedDate = new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8673),
                            Title = "خرید از حساب"
                        });
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Wallet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WalletNumber")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Wallets");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccountId = 1L,
                            Amount = 0L,
                            CreatedBy = 0L,
                            CreatedDate = new DateTime(2024, 8, 3, 13, 10, 3, 999, DateTimeKind.Local).AddTicks(8585),
                            WalletNumber = new Guid("06aa9a49-7425-47af-8329-c76ed57a4137")
                        });
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Account", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.AccountUser", "AccountUser")
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AccountUser");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.AccountBook", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoBar.Accounting.Data.Entities.AccountTransaction", "AccountTransaction")
                        .WithMany()
                        .HasForeignKey("AccountTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("AccountTransaction");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.AccountTransaction", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.Invoice", "Invocie")
                        .WithMany()
                        .HasForeignKey("InvoiceId");

                    b.HasOne("EcoBar.Accounting.Data.Entities.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("EcoBar.Accounting.Data.Entities.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invocie");

                    b.Navigation("Payment");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Company", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.AccountUser", "AccountUser")
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountUser");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Invoice", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.AccountUser", "AccountUser")
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountUser");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.InvoiceItem", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoBar.Accounting.Data.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Payment", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.AccountUser", "AccountUser")
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountUser");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Wallet", b =>
                {
                    b.HasOne("EcoBar.Accounting.Data.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("EcoBar.Accounting.Data.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceItems");
                });
#pragma warning restore 612, 618
        }
    }
}
