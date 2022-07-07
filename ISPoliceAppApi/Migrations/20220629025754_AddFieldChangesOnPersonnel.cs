using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class AddFieldChangesOnPersonnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Genders_GenderId",
                table: "Personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Maritals_MaritalStatusId",
                table: "Personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_PersonnelFamilies_PersonnelFamilyId",
                table: "Personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Religions_ReligionId",
                table: "Personnels");

            migrationBuilder.DropIndex(
                name: "IX_Personnels_PersonnelFamilyId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "PersonnelFamilyId",
                table: "Personnels");

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "Personnels",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "Personnels",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Personnels",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Personnels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "Personnels",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonnelChildrens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelChildrens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelChildrens_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelSpouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelSpouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelSpouses_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelChildrens_PersonnelId",
                table: "PersonnelChildrens",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelSpouses_PersonnelId",
                table: "PersonnelSpouses",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Genders_GenderId",
                table: "Personnels",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Maritals_MaritalStatusId",
                table: "Personnels",
                column: "MaritalStatusId",
                principalTable: "Maritals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Religions_ReligionId",
                table: "Personnels",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Genders_GenderId",
                table: "Personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Maritals_MaritalStatusId",
                table: "Personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Religions_ReligionId",
                table: "Personnels");

            migrationBuilder.DropTable(
                name: "PersonnelChildrens");

            migrationBuilder.DropTable(
                name: "PersonnelSpouses");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "Personnels");

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "Personnels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "Personnels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Personnels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PersonnelFamilyId",
                table: "Personnels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_PersonnelFamilyId",
                table: "Personnels",
                column: "PersonnelFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Genders_GenderId",
                table: "Personnels",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Maritals_MaritalStatusId",
                table: "Personnels",
                column: "MaritalStatusId",
                principalTable: "Maritals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_PersonnelFamilies_PersonnelFamilyId",
                table: "Personnels",
                column: "PersonnelFamilyId",
                principalTable: "PersonnelFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Religions_ReligionId",
                table: "Personnels",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
