﻿// <auto-generated />
using System;
using ISPoliceAppApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ISPoliceAppApi.Migrations
{
    [DbContext(typeof(ISPoliceAppApiDbContext))]
    [Migration("20220215064238_pszondisId")]
    partial class pszondisId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ISPoliceAppApi.DSR.ControlRoomDSR", b =>
                {
                    b.Property<int>("ControlRoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CaseNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Complainant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComplainantAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Do")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dr")
                        .HasColumnType("datetime2");

                    b.Property<string>("DrSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GivenBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PSName")
                        .HasColumnType("int");

                    b.Property<string>("PSNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SOC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TakenBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<string>("TotalAccused")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("ControlRoomId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ControlRoomDSR");
                });

            modelBuilder.Entity("ISPoliceAppApi.DSR.ControlRoomDSRAccused", b =>
                {
                    b.Property<int>("ControlRoomAccusedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccusedAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccusedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ControlRoomId")
                        .HasColumnType("int");

                    b.Property<string>("CrimeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HSNbr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SectionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ControlRoomAccusedId");

                    b.HasIndex("ControlRoomId");

                    b.ToTable("ControlRoomDSRAccused");
                });

            modelBuilder.Entity("ISPoliceAppApi.DSR.ControlRoomDSRAccusedDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CrimeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DSRAccusedId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SectionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DSRAccusedId");

                    b.ToTable("ControlRoomDSRAccusedDetail");
                });

            modelBuilder.Entity("ISPoliceAppApi.DSR.ControlRoomDSRCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("ControlRoomDSRCategory");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.CategoryMaster", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("CategoryId");

                    b.ToTable("CategoryMaster");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.DistrictMaster", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("DistrictId");

                    b.HasIndex("ZoneId");

                    b.ToTable("DistrictMaster");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.StationMaster", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("StationId");

                    b.HasIndex("DistrictId");

                    b.ToTable("StationMaster");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.SubCategoryMaster", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("SubCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategoryMaster");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("VenueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("VenueId");

                    b.ToTable("Venue");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.VenuePermissionType", b =>
                {
                    b.Property<int>("VenuePermissionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.Property<string>("VenuePermissionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("VenuePermissionTypeId");

                    b.HasIndex("VenueId");

                    b.ToTable("VenuePermissionType");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.WorkflowMaster", b =>
                {
                    b.Property<int>("WorkflowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("WorkflowName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("WorkflowId")
                        .HasName("PK__WorkflowMaster");

                    b.ToTable("WorkflowMaster");
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.ZoneMaster", b =>
                {
                    b.Property<int>("ZoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Zone")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ZoneId");

                    b.ToTable("ZoneMaster");
                });

            modelBuilder.Entity("ISPoliceAppApi.DSR.ControlRoomDSR", b =>
                {
                    b.HasOne("ISPoliceAppApi.DSR.ControlRoomDSRCategory", "ControlRoomCategory")
                        .WithMany("ControlRoomDSRs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ISPoliceAppApi.DSR.ControlRoomDSRAccused", b =>
                {
                    b.HasOne("ISPoliceAppApi.DSR.ControlRoomDSR", "ControlRoom")
                        .WithMany("ControlRoomDSRAccuseds")
                        .HasForeignKey("ControlRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ISPoliceAppApi.DSR.ControlRoomDSRAccusedDetail", b =>
                {
                    b.HasOne("ISPoliceAppApi.DSR.ControlRoomDSRAccused", "DSRAccused")
                        .WithMany("ControlRoomDSRAccusedDetails")
                        .HasForeignKey("DSRAccusedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.DistrictMaster", b =>
                {
                    b.HasOne("ISPoliceAppApi.Models.ZoneMaster", "Zone")
                        .WithMany("DistrictMaster")
                        .HasForeignKey("ZoneId")
                        .HasConstraintName("FK_DistrictMaster_ZoneMaster")
                        .IsRequired();
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.StationMaster", b =>
                {
                    b.HasOne("ISPoliceAppApi.Models.DistrictMaster", "District")
                        .WithMany("StationMaster")
                        .HasForeignKey("DistrictId")
                        .HasConstraintName("FK_StationMaster_DistrictMaster")
                        .IsRequired();
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.SubCategoryMaster", b =>
                {
                    b.HasOne("ISPoliceAppApi.Models.CategoryMaster", "Category")
                        .WithMany("SubCategoryMaster")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_SubCategoryMaster_CategoryMaster")
                        .IsRequired();
                });

            modelBuilder.Entity("ISPoliceAppApi.Models.VenuePermissionType", b =>
                {
                    b.HasOne("ISPoliceAppApi.Models.Venue", "Venue")
                        .WithMany("VenuePermissionType")
                        .HasForeignKey("VenueId")
                        .HasConstraintName("FK_VenuePermissionType_Venue")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
