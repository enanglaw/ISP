using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class AddAllegationEnquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllegationEnquiries_Personnels_PersonnelId",
                table: "AllegationEnquiries");

            migrationBuilder.AlterColumn<int>(
                name: "PersonnelId",
                table: "AllegationEnquiries",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AllegationId",
                table: "AllegationEnquiries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enquiries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Participant = table.Column<string>(nullable: false),
                    OutCome = table.Column<string>(nullable: true),
                    MOM = table.Column<string>(nullable: true),
                    AllegationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enquiries_Allegations_AllegationId",
                        column: x => x.AllegationId,
                        principalTable: "Allegations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllegationEnquiryDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DocumentUrl = table.Column<string>(nullable: true),
                    AllegationEnquiryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllegationEnquiryDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllegationEnquiryDocuments_Enquiries_AllegationEnquiryId",
                        column: x => x.AllegationEnquiryId,
                        principalTable: "Enquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllegationEnquiries_AllegationId",
                table: "AllegationEnquiries",
                column: "AllegationId");

            migrationBuilder.CreateIndex(
                name: "IX_AllegationEnquiryDocuments_AllegationEnquiryId",
                table: "AllegationEnquiryDocuments",
                column: "AllegationEnquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_AllegationId",
                table: "Enquiries",
                column: "AllegationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllegationEnquiries_Allegations_AllegationId",
                table: "AllegationEnquiries",
                column: "AllegationId",
                principalTable: "Allegations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AllegationEnquiries_Personnels_PersonnelId",
                table: "AllegationEnquiries",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllegationEnquiries_Allegations_AllegationId",
                table: "AllegationEnquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_AllegationEnquiries_Personnels_PersonnelId",
                table: "AllegationEnquiries");

            migrationBuilder.DropTable(
                name: "AllegationEnquiryDocuments");

            migrationBuilder.DropTable(
                name: "Enquiries");

            migrationBuilder.DropIndex(
                name: "IX_AllegationEnquiries_AllegationId",
                table: "AllegationEnquiries");

            migrationBuilder.DropColumn(
                name: "AllegationId",
                table: "AllegationEnquiries");

            migrationBuilder.AlterColumn<int>(
                name: "PersonnelId",
                table: "AllegationEnquiries",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AllegationEnquiries_Personnels_PersonnelId",
                table: "AllegationEnquiries",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
