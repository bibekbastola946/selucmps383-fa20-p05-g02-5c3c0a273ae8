using Microsoft.EntityFrameworkCore.Migrations;

namespace FA20.P05.Web.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qrcode",
                table: "TemperatureRecordDto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Qrcode",
                table: "TemperatureRecordDto",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
