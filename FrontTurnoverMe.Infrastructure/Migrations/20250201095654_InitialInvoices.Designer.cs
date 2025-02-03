﻿// <auto-generated />
using System;
using FrontTurnoverMe.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FrontTurnoverMe.Infrastructure.Migrations
{
    [DbContext(typeof(InvoicesDbContext))]
    [Migration("20250201095654_InitialInvoices")]
    partial class InitialInvoices
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.Invoice", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTime?>("DeliveryDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("TotalAmountDue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalNetAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalTaxAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceBuyer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId")
                        .IsUnique();

                    b.ToTable("InvoiceBuyer");
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoicePositionItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GrossValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("NetValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UnitNetPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoicePositionItem");
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceReceiver", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId")
                        .IsUnique();

                    b.ToTable("InvoiceReceiver");
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceSeller", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId")
                        .IsUnique();

                    b.ToTable("InvoiceSeller");
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceBuyer", b =>
                {
                    b.HasOne("FrontTurnoverMe.Domain.Entities.Invoices.Invoice", null)
                        .WithOne("Buyer")
                        .HasForeignKey("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceBuyer", "InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("FrontTurnoverMe.Domain.Entities.ValueObjects.InvoiceAddressValueObject", "AddressValueObject", b1 =>
                        {
                            b1.Property<string>("InvoiceBuyerId")
                                .HasColumnType("text");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Country");

                            b1.Property<string>("FlatNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FlatNumber");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PostCode");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Street");

                            b1.Property<string>("StreetNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("StreetNumber");

                            b1.HasKey("InvoiceBuyerId");

                            b1.ToTable("InvoiceBuyer");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceBuyerId");
                        });

                    b.Navigation("AddressValueObject")
                        .IsRequired();
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoicePositionItem", b =>
                {
                    b.HasOne("FrontTurnoverMe.Domain.Entities.Invoices.Invoice", null)
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceReceiver", b =>
                {
                    b.HasOne("FrontTurnoverMe.Domain.Entities.Invoices.Invoice", null)
                        .WithOne("Receiver")
                        .HasForeignKey("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceReceiver", "InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("FrontTurnoverMe.Domain.Entities.ValueObjects.InvoiceAddressValueObject", "AddressValueObject", b1 =>
                        {
                            b1.Property<string>("InvoiceReceiverId")
                                .HasColumnType("text");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Country");

                            b1.Property<string>("FlatNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FlatNumber");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PostCode");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Street");

                            b1.Property<string>("StreetNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("StreetNumber");

                            b1.HasKey("InvoiceReceiverId");

                            b1.ToTable("InvoiceReceiver");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceReceiverId");
                        });

                    b.Navigation("AddressValueObject")
                        .IsRequired();
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceSeller", b =>
                {
                    b.HasOne("FrontTurnoverMe.Domain.Entities.Invoices.Invoice", null)
                        .WithOne("Seller")
                        .HasForeignKey("FrontTurnoverMe.Domain.Entities.Invoices.InvoiceSeller", "InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("FrontTurnoverMe.Domain.Entities.ValueObjects.InvoiceAddressValueObject", "AddressValueObject", b1 =>
                        {
                            b1.Property<string>("InvoiceSellerId")
                                .HasColumnType("text");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Country");

                            b1.Property<string>("FlatNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FlatNumber");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PostCode");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Street");

                            b1.Property<string>("StreetNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("StreetNumber");

                            b1.HasKey("InvoiceSellerId");

                            b1.ToTable("InvoiceSeller");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceSellerId");
                        });

                    b.Navigation("AddressValueObject")
                        .IsRequired();
                });

            modelBuilder.Entity("FrontTurnoverMe.Domain.Entities.Invoices.Invoice", b =>
                {
                    b.Navigation("Buyer")
                        .IsRequired();

                    b.Navigation("Items");

                    b.Navigation("Receiver");

                    b.Navigation("Seller")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
