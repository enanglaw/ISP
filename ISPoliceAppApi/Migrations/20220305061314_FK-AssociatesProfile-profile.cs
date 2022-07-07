using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class FKAssociatesProfileprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileAssociates_ProfileMaster_ProfileId",
                table: "ProfileAssociates");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAssociates_AssociatesId",
                table: "ProfileAssociates",
                column: "AssociatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociatesProfile_Profile",
                table: "ProfileAssociates",
                column: "AssociatesId",
                principalTable: "ProfileMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryProfileDetail_Profile",
                table: "ProfileAssociates",
                column: "ProfileId",
                principalTable: "ProfileMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociatesProfile_Profile",
                table: "ProfileAssociates");

            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryProfileDetail_Profile",
                table: "ProfileAssociates");

            migrationBuilder.DropIndex(
                name: "IX_ProfileAssociates_AssociatesId",
                table: "ProfileAssociates");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileAssociates_ProfileMaster_ProfileId",
                table: "ProfileAssociates",
                column: "ProfileId",
                principalTable: "ProfileMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
