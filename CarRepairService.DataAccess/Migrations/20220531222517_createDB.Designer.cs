// <auto-generated />
using System;
using CarRepairService.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRepairService.DataAccess.Migrations
{
    [DbContext(typeof(CarRepairServiceDataContext))]
    [Migration("20220531222517_createDB")]
    partial class createDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarRepairService.Entities.DBO.Appointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Technician")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("carID")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<int?>("userID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("carID");

                    b.HasIndex("userID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("CarRepairService.Entities.DBO.Car", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("carBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("carImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fuelTypeID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("lastMaintenanceDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("modelYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("transmissionTypeID")
                        .HasColumnType("int");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("fuelTypeID");

                    b.HasIndex("transmissionTypeID");

                    b.HasIndex("userID");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarRepairService.Entities.DBO.FuelType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("FuelTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Type = "Gasoline"
                        },
                        new
                        {
                            ID = 2,
                            Type = "Diesel"
                        },
                        new
                        {
                            ID = 3,
                            Type = "LPG"
                        },
                        new
                        {
                            ID = 4,
                            Type = "Gasoline/LPG"
                        },
                        new
                        {
                            ID = 5,
                            Type = "Hybrid"
                        });
                });

            modelBuilder.Entity("CarRepairService.Entities.DBO.TransmissionType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TransmissionTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Type = "Manual"
                        },
                        new
                        {
                            ID = 2,
                            Type = "Automatic"
                        },
                        new
                        {
                            ID = 3,
                            Type = "Semi Automatic"
                        });
                });

            modelBuilder.Entity("CarRepairService.Entities.DBO.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "Yazılım Mühendisi",
                            Role = 3,
                            Username = "hakan",
                            firstName = "Hakan",
                            isActive = true,
                            lastName = "Yunusoğlu",
                            passwordHash = new byte[] { 216, 8, 77, 226, 237, 236, 40, 56, 190, 143, 145, 62, 208, 199, 232, 253, 50, 33, 138, 34, 167, 234, 3, 141, 192, 212, 226, 99, 209, 97, 8, 37, 53, 6, 149, 166, 168, 214, 38, 71, 219, 217, 241, 199, 31, 70, 204, 212, 221, 26, 24, 204, 133, 154, 160, 149, 153, 156, 163, 239, 78, 217, 68, 49 },
                            passwordSalt = new byte[] { 128, 184, 190, 144, 187, 162, 226, 49, 57, 128, 170, 218, 222, 47, 165, 188, 250, 176, 184, 188, 181, 16, 216, 2, 161, 154, 230, 121, 196, 34, 185, 120, 40, 240, 116, 253, 68, 189, 73, 45, 141, 240, 171, 14, 222, 186, 229, 138, 93, 27, 99, 135, 147, 77, 141, 47, 116, 220, 211, 214, 28, 90, 25, 85, 59, 63, 145, 194, 7, 120, 111, 83, 121, 236, 247, 194, 135, 106, 149, 243, 26, 250, 241, 59, 48, 164, 191, 100, 240, 194, 139, 125, 234, 193, 63, 164, 169, 219, 156, 57, 193, 52, 141, 90, 245, 178, 121, 38, 46, 240, 251, 221, 205, 170, 201, 115, 246, 196, 202, 71, 106, 192, 179, 154, 46, 113, 172, 17 }
                        });
                });

            modelBuilder.Entity("CarRepairService.Entities.DBO.Appointment", b =>
                {
                    b.HasOne("CarRepairService.Entities.DBO.Car", "Car")
                        .WithMany()
                        .HasForeignKey("carID");

                    b.HasOne("CarRepairService.Entities.DBO.User", "User")
                        .WithMany()
                        .HasForeignKey("userID");

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarRepairService.Entities.DBO.Car", b =>
                {
                    b.HasOne("CarRepairService.Entities.DBO.FuelType", "FuelType")
                        .WithMany()
                        .HasForeignKey("fuelTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRepairService.Entities.DBO.TransmissionType", "TransmissionType")
                        .WithMany()
                        .HasForeignKey("transmissionTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRepairService.Entities.DBO.User", "User")
                        .WithMany()
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelType");

                    b.Navigation("TransmissionType");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
