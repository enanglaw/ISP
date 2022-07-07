using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class FieldModificationOnLeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaders_LeadersGroups_LeadersId",
                table: "Leaders");

            migrationBuilder.DropIndex(
                name: "IX_Leaders_LeadersId",
                table: "Leaders");

            migrationBuilder.DropColumn(
                name: "LeadersId",
                table: "Leaders");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationLeaderId",
                table: "Leaders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubOrganizationLeaderId",
                table: "Leaders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationLeaderId",
                table: "Leaders");

            migrationBuilder.DropColumn(
                name: "SubOrganizationLeaderId",
                table: "Leaders");

            migrationBuilder.AddColumn<int>(
                name: "LeadersId",
                table: "Leaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leaders_LeadersId",
                table: "Leaders",
                column: "LeadersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaders_LeadersGroups_LeadersId",
                table: "Leaders",
                column: "LeadersId",
                principalTable: "LeadersGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
