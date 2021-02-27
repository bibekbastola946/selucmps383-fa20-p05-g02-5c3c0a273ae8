﻿// <auto-generated />

using System;
using FA20.P05.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FA20.P05.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200904171917_SimpleDomain")]
    partial class SimpleDomain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FA20.P03.Web.Features.SchoolStaffMembers.SchoolStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.HasIndex("StaffId");

                    b.ToTable("SchoolStaff");
                });

            modelBuilder.Entity("FA20.P03.Web.Features.Schools.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("SchoolPopulation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("School");
                });

            modelBuilder.Entity("FA20.P03.Web.Features.StaffMembers.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("FA20.P03.Web.Features.TemperatureRecords.TemperatureRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("MeasuredUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<double>("temperatureFahrenheit")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.HasIndex("StaffId");

                    b.ToTable("TemperatureRecord");
                });

            modelBuilder.Entity("FA20.P03.Web.Features.SchoolStaffMembers.SchoolStaff", b =>
                {
                    b.HasOne("FA20.P03.Web.Features.Schools.School", "School")
                        .WithMany("Staff")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FA20.P03.Web.Features.StaffMembers.Staff", "Staff")
                        .WithMany("Schools")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FA20.P03.Web.Features.Schools.School", b =>
                {
                    b.OwnsOne("FA20.P03.Web.Features.Shared.Address", "Address", b1 =>
                        {
                            b1.Property<int>("SchoolId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("AddressLine1")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("AddressLine2")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(2)")
                                .HasMaxLength(2);

                            b1.Property<string>("Zip")
                                .HasColumnType("nvarchar(5)")
                                .HasMaxLength(5);

                            b1.HasKey("SchoolId");

                            b1.ToTable("School");

                            b1.WithOwner()
                                .HasForeignKey("SchoolId");
                        });
                });

            modelBuilder.Entity("FA20.P03.Web.Features.TemperatureRecords.TemperatureRecord", b =>
                {
                    b.HasOne("FA20.P03.Web.Features.Schools.School", "School")
                        .WithMany("TemperatureRecords")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FA20.P03.Web.Features.StaffMembers.Staff", "Staff")
                        .WithMany("TemperatureRecords")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
