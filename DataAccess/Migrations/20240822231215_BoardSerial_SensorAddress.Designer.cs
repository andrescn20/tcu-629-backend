﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(MonitoringDbContext))]
    [Migration("20240822231215_BoardSerial_SensorAddress")]
    partial class BoardSerial_SensorAddress
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoardId"));

                    b.Property<string>("BoardSerial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("Microcontroller")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoardId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("DataAccess.Models.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeviceId"));

                    b.Property<DateTime>("Added_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeviceTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("DataAccess.Models.DeviceType", b =>
                {
                    b.Property<int>("DeviceTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeviceTypeId"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceTypeId");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("DataAccess.Models.DispenserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SensorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("DispenserData");
                });

            modelBuilder.Entity("DataAccess.Models.DispenserLevelData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("LiquidLevel")
                        .HasColumnType("bit");

                    b.Property<int>("SensorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("DispenserLevelData");
                });

            modelBuilder.Entity("DataAccess.Models.Sensor", b =>
                {
                    b.Property<int>("SensorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SensorId"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SensorAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SensorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SensorTypeId")
                        .HasColumnType("int");

                    b.HasKey("SensorId");

                    b.HasIndex("BoardId");

                    b.HasIndex("SensorTypeId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("DataAccess.Models.SensorType", b =>
                {
                    b.Property<int>("SensorTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SensorTypeId"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SensorTypeId");

                    b.ToTable("SensorTypes");
                });

            modelBuilder.Entity("DataAccess.Models.TemperatureData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SensorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("TemperatureData");
                });

            modelBuilder.Entity("DataAccess.Models.Board", b =>
                {
                    b.HasOne("DataAccess.Models.Device", "Device")
                        .WithMany("Boards")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DataAccess.Models.Device", b =>
                {
                    b.HasOne("DataAccess.Models.DeviceType", "DeviceType")
                        .WithMany("Devices")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceType");
                });

            modelBuilder.Entity("DataAccess.Models.DispenserData", b =>
                {
                    b.HasOne("DataAccess.Models.Sensor", "Sensor")
                        .WithMany("DispenserDataList")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("DataAccess.Models.DispenserLevelData", b =>
                {
                    b.HasOne("DataAccess.Models.Sensor", "Sensor")
                        .WithMany("DispenserLevelDataList")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("DataAccess.Models.Sensor", b =>
                {
                    b.HasOne("DataAccess.Models.Board", "Board")
                        .WithMany("Sensors")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.SensorType", "SensorType")
                        .WithMany("Sensors")
                        .HasForeignKey("SensorTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("SensorType");
                });

            modelBuilder.Entity("DataAccess.Models.TemperatureData", b =>
                {
                    b.HasOne("DataAccess.Models.Sensor", "Sensor")
                        .WithMany("TemperatureDataList")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("DataAccess.Models.Board", b =>
                {
                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("DataAccess.Models.Device", b =>
                {
                    b.Navigation("Boards");
                });

            modelBuilder.Entity("DataAccess.Models.DeviceType", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("DataAccess.Models.Sensor", b =>
                {
                    b.Navigation("DispenserDataList");

                    b.Navigation("DispenserLevelDataList");

                    b.Navigation("TemperatureDataList");
                });

            modelBuilder.Entity("DataAccess.Models.SensorType", b =>
                {
                    b.Navigation("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}
