﻿// <auto-generated />
using System;
using FlightService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlightService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220625131158_cost4")]
    partial class cost4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlightService.Airline", b =>
                {
                    b.Property<int>("AirlineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Addtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("AirlineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modtime")
                        .HasColumnType("datetime2");

                    b.HasKey("AirlineId");

                    b.ToTable("Airline");
                });

            modelBuilder.Entity("FlightService.Model.Booking", b =>
                {
                    b.Property<int>("PNRNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AddTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FromTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCancel")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NoOfSeats")
                        .HasColumnType("int");

                    b.Property<string>("PNR")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("N'PNR' + RIGHT('00000'+CAST(PNRNo as NVARCHAR(5)),5)");

                    b.Property<string>("ToCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ToTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("TotalAmount")
                        .HasColumnType("bigint");

                    b.Property<string>("TripType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PNRNo");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("FlightService.Model.ScheduleAirline", b =>
                {
                    b.Property<int>("SchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<int>("BCSeats")
                        .HasColumnType("int");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("FlightName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrumentUsed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meals")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NBCSeats")
                        .HasColumnType("int");

                    b.Property<string>("ScheduledDays")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketCost")
                        .HasColumnType("int");

                    b.Property<string>("ToCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SchId");

                    b.ToTable("ScheduleAirline");
                });
#pragma warning restore 612, 618
        }
    }
}