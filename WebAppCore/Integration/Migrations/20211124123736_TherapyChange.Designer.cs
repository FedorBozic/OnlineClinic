﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Integration.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations
{
    [DbContext(typeof(IntegrationDbContext))]
    [Migration("20211124123736_TherapyChange")]
    partial class TherapyChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Integration.Allergen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<List<string>>("IngredientNames")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("MedicineNames")
                        .HasColumnType("text[]");

                    b.Property<string>("Other")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Allergen");
                });

            modelBuilder.Entity("Integration.Appointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("DurationInHours")
                        .HasColumnType("double precision");

                    b.Property<string>("IDAppointment")
                        .HasColumnType("text");

                    b.Property<string>("IDDoctor")
                        .HasColumnType("text");

                    b.Property<string>("IDpatient")
                        .HasColumnType("text");

                    b.Property<string>("PatientsRecordMedicalRecordID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("PatientsRecordMedicalRecordID");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("Integration.Examination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text");

                    b.Property<string>("MedicalRecordId")
                        .HasColumnType("text");

                    b.Property<int>("TherapyId")
                        .HasColumnType("integer");

                    b.Property<string>("anamnesis")
                        .HasColumnType("text");

                    b.Property<int?>("appointmentID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("dateOfExamination")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("patientVisible")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRecordId");

                    b.HasIndex("TherapyId");

                    b.HasIndex("appointmentID");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("Integration.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("MedicineID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MedicineID");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("Integration.MedicalRecord", b =>
                {
                    b.Property<string>("MedicalRecordID")
                        .HasColumnType("text");

                    b.Property<int?>("AllergenId")
                        .HasColumnType("integer");

                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.Property<string>("HealthCardNumber")
                        .HasColumnType("text");

                    b.Property<bool>("IsInsured")
                        .HasColumnType("boolean");

                    b.Property<string>("ParentName")
                        .HasColumnType("text");

                    b.Property<int?>("PatientId")
                        .HasColumnType("integer");

                    b.HasKey("MedicalRecordID");

                    b.HasIndex("AllergenId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("Integration.Medicine", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("DosageInMg")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<List<string>>("ReplacementMedicineIDs")
                        .HasColumnType("text[]");

                    b.HasKey("ID");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("Integration.Model.MedicineTherapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DurationInDays")
                        .HasColumnType("integer");

                    b.Property<string>("MedicineID")
                        .HasColumnType("text");

                    b.Property<int>("TherapyId")
                        .HasColumnType("integer");

                    b.Property<int>("TimesPerDay")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MedicineID");

                    b.HasIndex("TherapyId");

                    b.ToTable("MedicineTherapys");
                });

            modelBuilder.Entity("Integration.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGuest")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PersonalID")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patient");
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

            modelBuilder.Entity("Integration.Therapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("MedicineId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Therapies");
                });

            modelBuilder.Entity("Integration.Appointment", b =>
                {
                    b.HasOne("Integration.MedicalRecord", "PatientsRecord")
                        .WithMany()
                        .HasForeignKey("PatientsRecordMedicalRecordID");
                });

            modelBuilder.Entity("Integration.Examination", b =>
                {
                    b.HasOne("Integration.MedicalRecord", null)
                        .WithMany("Examination")
                        .HasForeignKey("MedicalRecordId");

                    b.HasOne("Integration.Therapy", "therapy")
                        .WithMany()
                        .HasForeignKey("TherapyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integration.Appointment", "appointment")
                        .WithMany()
                        .HasForeignKey("appointmentID");
                });

            modelBuilder.Entity("Integration.Ingredient", b =>
                {
                    b.HasOne("Integration.Medicine", null)
                        .WithMany("Ingredient")
                        .HasForeignKey("MedicineID");
                });

            modelBuilder.Entity("Integration.MedicalRecord", b =>
                {
                    b.HasOne("Integration.Allergen", "Allergen")
                        .WithMany()
                        .HasForeignKey("AllergenId");

                    b.HasOne("Integration.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("Integration.Model.MedicineTherapy", b =>
                {
                    b.HasOne("Integration.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineID");

                    b.HasOne("Integration.Therapy", null)
                        .WithMany("Medicine")
                        .HasForeignKey("TherapyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
