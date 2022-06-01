using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairService.DataAccess.Migrations
{
    public partial class createDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TransmissionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransmissionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastMaintenanceDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fuelTypeID = table.Column<int>(type: "int", nullable: false),
                    transmissionTypeID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_FuelTypes_fuelTypeID",
                        column: x => x.fuelTypeID,
                        principalTable: "FuelTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_TransmissionTypes_transmissionTypeID",
                        column: x => x.transmissionTypeID,
                        principalTable: "TransmissionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    userID = table.Column<int>(type: "int", nullable: true),
                    carID = table.Column<int>(type: "int", nullable: true),
                    Technician = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointments_Cars_carID",
                        column: x => x.carID,
                        principalTable: "Cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FuelTypes",
                columns: new[] { "ID", "Type" },
                values: new object[,]
                {
                    { 1, "Gasoline" },
                    { 2, "Diesel" },
                    { 3, "LPG" },
                    { 4, "Gasoline/LPG" },
                    { 5, "Hybrid" }
                });

            migrationBuilder.InsertData(
                table: "TransmissionTypes",
                columns: new[] { "ID", "Type" },
                values: new object[,]
                {
                    { 1, "Manual" },
                    { 2, "Automatic" },
                    { 3, "Semi Automatic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Description", "Role", "Username", "firstName", "isActive", "lastName", "passwordHash", "passwordSalt" },
                values: new object[] { 1, "Yazılım Mühendisi", 3, "hakan", "Hakan", true, "Yunusoğlu", new byte[] { 216, 8, 77, 226, 237, 236, 40, 56, 190, 143, 145, 62, 208, 199, 232, 253, 50, 33, 138, 34, 167, 234, 3, 141, 192, 212, 226, 99, 209, 97, 8, 37, 53, 6, 149, 166, 168, 214, 38, 71, 219, 217, 241, 199, 31, 70, 204, 212, 221, 26, 24, 204, 133, 154, 160, 149, 153, 156, 163, 239, 78, 217, 68, 49 }, new byte[] { 128, 184, 190, 144, 187, 162, 226, 49, 57, 128, 170, 218, 222, 47, 165, 188, 250, 176, 184, 188, 181, 16, 216, 2, 161, 154, 230, 121, 196, 34, 185, 120, 40, 240, 116, 253, 68, 189, 73, 45, 141, 240, 171, 14, 222, 186, 229, 138, 93, 27, 99, 135, 147, 77, 141, 47, 116, 220, 211, 214, 28, 90, 25, 85, 59, 63, 145, 194, 7, 120, 111, 83, 121, 236, 247, 194, 135, 106, 149, 243, 26, 250, 241, 59, 48, 164, 191, 100, 240, 194, 139, 125, 234, 193, 63, 164, 169, 219, 156, 57, 193, 52, 141, 90, 245, 178, 121, 38, 46, 240, 251, 221, 205, 170, 201, 115, 246, 196, 202, 71, 106, 192, 179, 154, 46, 113, 172, 17 } });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_carID",
                table: "Appointments",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_userID",
                table: "Appointments",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_fuelTypeID",
                table: "Cars",
                column: "fuelTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_transmissionTypeID",
                table: "Cars",
                column: "transmissionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_userID",
                table: "Cars",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "FuelTypes");

            migrationBuilder.DropTable(
                name: "TransmissionTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
