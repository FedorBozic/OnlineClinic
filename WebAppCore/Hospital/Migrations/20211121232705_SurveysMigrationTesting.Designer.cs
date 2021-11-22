﻿// <auto-generated />
using System;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    [DbContext(typeof(HospitalContext))]
    [Migration("20211121232705_SurveysMigrationTesting")]
    partial class SurveysMigrationTesting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<bool>("isAnonymous")
                        .HasColumnType("boolean");

                    b.Property<bool>("isPublic")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Activated")
                        .HasColumnType("boolean");

                    b.Property<string>("EMail")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.AnsweredSurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Answer")
                        .HasColumnType("integer");

                    b.Property<int>("PatientForeignKey")
                        .HasColumnType("integer");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<int>("Section")
                        .HasColumnType("integer");

                    b.Property<int>("SurveyForeignKey")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AnsweredQuestions");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Appointment", b =>
                {
                    b.Property<int>("IdAppointment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("IdAppointment");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Survey", b =>
                {
                    b.Property<int>("IdSurvey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdAppointment")
                        .HasColumnType("integer");

                    b.HasKey("IdSurvey");

                    b.HasIndex("IdAppointment");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Answer")
                        .HasColumnType("integer");

                    b.Property<int>("IdSurvey")
                        .HasColumnType("integer");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<int>("Section")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdSurvey");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Survey", b =>
                {
                    b.HasOne("Hospital.Schedule.Model.Appointment", "Appointment")
                        .WithMany("Surveys")
                        .HasForeignKey("IdAppointment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.SurveyQuestion", b =>
                {
                    b.HasOne("Hospital.Schedule.Model.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("IdSurvey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Appointment", b =>
                {
                    b.Navigation("Surveys");
                });
#pragma warning restore 612, 618
        }
    }
}
