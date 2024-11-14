﻿// <auto-generated />
using System;
using BikeRentalCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BikeRentalCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("BikeRentalCore.Entities.Bike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("BikeType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrakeType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Color")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FrameMaterial")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FrameSize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfGears")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SuspensionType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransmissionType")
                        .HasColumnType("INTEGER");

                    b.Property<float>("WheelSize")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Bikes");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.RentalPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AddressNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Complement")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UnityName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RentalPoints");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Tenancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RentalCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("RentedDays")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly?>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("StartedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UnityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UnityId");

                    b.ToTable("Tenancies");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Unity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BikeId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RentalPointId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BikeId");

                    b.HasIndex("RentalPointId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Bike", b =>
                {
                    b.HasOne("BikeRentalCore.Entities.Brand", "Brand")
                        .WithMany("Bikes")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Tenancy", b =>
                {
                    b.HasOne("BikeRentalCore.Entities.Unity", "Unity")
                        .WithMany("Tenancies")
                        .HasForeignKey("UnityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unity");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Unity", b =>
                {
                    b.HasOne("BikeRentalCore.Entities.Bike", "Bike")
                        .WithMany("Units")
                        .HasForeignKey("BikeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BikeRentalCore.Entities.RentalPoint", "RentalPoint")
                        .WithMany("Units")
                        .HasForeignKey("RentalPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bike");

                    b.Navigation("RentalPoint");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Bike", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Brand", b =>
                {
                    b.Navigation("Bikes");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.RentalPoint", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("BikeRentalCore.Entities.Unity", b =>
                {
                    b.Navigation("Tenancies");
                });
#pragma warning restore 612, 618
        }
    }
}
