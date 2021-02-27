using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FA20.P05.Web.Migrations
{
    public partial class SimpleDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    SchoolPopulation = table.Column<int>(nullable: false),
                    Address_AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    Address_AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    Address_City = table.Column<string>(maxLength: 100, nullable: true),
                    Address_State = table.Column<string>(maxLength: 2, nullable: true),
                    Address_Zip = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedUtc = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolStaff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolStaff_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    temperatureFahrenheit = table.Column<double>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    MeasuredUtc = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureRecord_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemperatureRecord_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolStaff_SchoolId",
                table: "SchoolStaff",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolStaff_StaffId",
                table: "SchoolStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureRecord_SchoolId",
                table: "TemperatureRecord",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureRecord_StaffId",
                table: "TemperatureRecord",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolStaff");

            migrationBuilder.DropTable(
                name: "TemperatureRecord");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
