using Microsoft.EntityFrameworkCore.Migrations;

namespace FA20.P05.Web.Migrations
{
    public partial class newfieldqrcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Qrcode",
                table: "TemperatureRecord",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TemperatureRecordDto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<int>(nullable: false),
                    temperatureFahrenheit = table.Column<double>(nullable: false),
                    Qrcode = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureRecordDto", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemperatureRecordDto");

            migrationBuilder.DropColumn(
                name: "Qrcode",
                table: "TemperatureRecord");
        }
    }
}
