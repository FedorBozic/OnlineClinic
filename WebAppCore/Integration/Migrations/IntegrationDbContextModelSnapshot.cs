﻿// <auto-generated />
using System;
using Integration.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations
{
    [DbContext(typeof(IntegrationDbContext))]
    partial class IntegrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Integration.Partnership.Model.OfferItem", b =>
                {
                    b.Property<int>("OfferItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Dosage")
                        .HasColumnType("double precision");

                    b.Property<string>("MedicineName")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int?>("TenderOfferId")
                        .HasColumnType("integer");

                    b.HasKey("OfferItemId");

                    b.HasIndex("TenderOfferId");

                    b.ToTable("OfferItems");
                });

            modelBuilder.Entity("Integration.Partnership.Model.Tender", b =>
                {
                    b.Property<int>("TenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("TenderHash")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("TenderId");

                    b.ToTable("Tenders");
                });

            modelBuilder.Entity("Integration.Partnership.Model.TenderItem", b =>
                {
                    b.Property<int>("TenderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Dosage")
                        .HasColumnType("double precision");

                    b.Property<string>("MedicineName")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int?>("TenderId")
                        .HasColumnType("integer");

                    b.HasKey("TenderItemId");

                    b.HasIndex("TenderId");

                    b.ToTable("TenderItems");
                });

            modelBuilder.Entity("Integration.Partnership.Model.TenderOffer", b =>
                {
                    b.Property<int>("TenderOfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("PharmacyName")
                        .HasColumnType("text");

                    b.Property<int?>("TenderId")
                        .HasColumnType("integer");

                    b.Property<string>("TenderOfferHash")
                        .HasColumnType("text");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double precision");

                    b.Property<bool>("Winner")
                        .HasColumnType("boolean");

                    b.HasKey("TenderOfferId");

                    b.HasIndex("TenderId");

                    b.ToTable("TenderOffers");
                });

            modelBuilder.Entity("Integration.Pharmacy.Model.Credential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApiKey")
                        .HasColumnType("text");

                    b.Property<string>("PharmacyLocalhost")
                        .HasColumnType("text");

                    b.Property<string>("PharmacyName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("Integration.Pharmacy.Model.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Integration.Pharmacy.Model.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IdEncoded")
                        .HasColumnType("text");

                    b.Property<string>("PharmacyName")
                        .HasColumnType("text");

                    b.Property<bool>("Posted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Integration.Pharmacy.Model.Objection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("PharmacyId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Objections");
                });

            modelBuilder.Entity("Integration.Pharmacy.Model.PharmacyProfile", b =>
                {
                    b.Property<string>("Localhost")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int>("Protocol")
                        .HasColumnType("integer");

                    b.HasKey("Localhost");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Integration.Pharmacy.Model.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("ObjectionId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("Integration.Partnership.Model.OfferItem", b =>
                {
                    b.HasOne("Integration.Partnership.Model.TenderOffer", null)
                        .WithMany("OfferItems")
                        .HasForeignKey("TenderOfferId");
                });

            modelBuilder.Entity("Integration.Partnership.Model.TenderItem", b =>
                {
                    b.HasOne("Integration.Partnership.Model.Tender", null)
                        .WithMany("TenderItems")
                        .HasForeignKey("TenderId");
                });

            modelBuilder.Entity("Integration.Partnership.Model.TenderOffer", b =>
                {
                    b.HasOne("Integration.Partnership.Model.Tender", null)
                        .WithMany("TenderOffers")
                        .HasForeignKey("TenderId");
                });

            modelBuilder.Entity("Integration.Partnership.Model.Tender", b =>
                {
                    b.Navigation("TenderItems");

                    b.Navigation("TenderOffers");
                });

            modelBuilder.Entity("Integration.Partnership.Model.TenderOffer", b =>
                {
                    b.Navigation("OfferItems");
                });
#pragma warning restore 612, 618
        }
    }
}
