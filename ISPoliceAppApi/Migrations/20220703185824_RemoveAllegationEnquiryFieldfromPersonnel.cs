using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class RemoveAllegationEnquiryFieldfromPersonnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllegationEnquiries_Personnels_PersonnelId",
                table: "AllegationEnquiries");

            migrationBuilder.DropIndex(
                name: "IX_AllegationEnquiries_PersonnelId",
                table: "AllegationEnquiries");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "AllegationEnquiries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonnelId",
                table: "AllegationEnquiries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllegationEnquiries_PersonnelId",
                table: "AllegationEnquiries",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllegationEnquiries_Personnels_PersonnelId",
                table: "AllegationEnquiries",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
