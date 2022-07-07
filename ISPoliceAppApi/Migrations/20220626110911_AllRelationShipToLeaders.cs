using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class AllRelationShipToLeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "SubOrganizationLeaders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "LeadersGroups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "SubOrganizationLeaders");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "LeadersGroups");
        }
    }
}
