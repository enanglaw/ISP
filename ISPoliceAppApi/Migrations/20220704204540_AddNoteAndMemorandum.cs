using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class AddNoteAndMemorandum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Memoranda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AllegationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memoranda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memoranda_Allegations_AllegationId",
                        column: x => x.AllegationId,
                        principalTable: "Allegations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AllegationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Allegations_AllegationId",
                        column: x => x.AllegationId,
                        principalTable: "Allegations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memoranda_AllegationId",
                table: "Memoranda",
                column: "AllegationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AllegationId",
                table: "Notes",
                column: "AllegationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memoranda");

            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
