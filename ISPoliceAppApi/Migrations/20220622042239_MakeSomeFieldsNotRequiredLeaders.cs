using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class MakeSomeFieldsNotRequiredLeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaders_Genders_GenderId",
                table: "Leaders");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaders_Maritals_MaritalStatusId",
                table: "Leaders");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaders_Religions_ReligionId",
                table: "Leaders");

            migrationBuilder.DropIndex(
                name: "IX_Leaders_GenderId",
                table: "Leaders");

            migrationBuilder.DropIndex(
                name: "IX_Leaders_MaritalStatusId",
                table: "Leaders");

            migrationBuilder.DropIndex(
                name: "IX_Leaders_ReligionId",
                table: "Leaders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Leaders_GenderId",
                table: "Leaders",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaders_MaritalStatusId",
                table: "Leaders",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaders_ReligionId",
                table: "Leaders",
                column: "ReligionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaders_Genders_GenderId",
                table: "Leaders",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaders_Maritals_MaritalStatusId",
                table: "Leaders",
                column: "MaritalStatusId",
                principalTable: "Maritals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaders_Religions_ReligionId",
                table: "Leaders",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
