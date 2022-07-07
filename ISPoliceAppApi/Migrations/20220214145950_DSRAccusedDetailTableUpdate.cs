using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class DSRAccusedDetailTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ControlRoomDSRAccusedDetail");

            migrationBuilder.DropColumn(
                name: "number",
                table: "ControlRoomDSRAccusedDetail");

            migrationBuilder.AddColumn<string>(
                name: "CrimeNumber",
                table: "ControlRoomDSRAccusedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectionNumber",
                table: "ControlRoomDSRAccusedDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrimeNumber",
                table: "ControlRoomDSRAccusedDetail");

            migrationBuilder.DropColumn(
                name: "SectionNumber",
                table: "ControlRoomDSRAccusedDetail");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ControlRoomDSRAccusedDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "number",
                table: "ControlRoomDSRAccusedDetail",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
