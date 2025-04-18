﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mosef;

#nullable disable

namespace Mosef.Migrations
{
    [DbContext(typeof(MosefDbContext))]
    [Migration("20250413102250_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mosef.Appointment", b =>
                {
                    b.Property<string>("AppointmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppointmentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Mosef.Feedback", b =>
                {
                    b.Property<string>("FeedbackId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FeedbackId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Mosef.Nurse", b =>
                {
                    b.Property<string>("NurseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NurseEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NursePassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NursePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseSpecialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NurseId");

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("Mosef.Patient", b =>
                {
                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientSSN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Mosef.Service", b =>
                {
                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssignedNurseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ServiceEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServiceStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
