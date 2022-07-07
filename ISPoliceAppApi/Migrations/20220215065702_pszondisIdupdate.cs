using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class pszondisIdupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PSName",
                table: "ControlRoomDSR",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DistrictId",
                table: "ControlRoomDSR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PSId",
                table: "ControlRoomDSR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZoneId",
                table: "ControlRoomDSR",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "ControlRoomDSR");

            migrationBuilder.DropColumn(
                name: "PSId",
                table: "ControlRoomDSR");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "ControlRoomDSR");

            migrationBuilder.AlterColumn<int>(
                name: "PSName",
                table: "ControlRoomDSR",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
