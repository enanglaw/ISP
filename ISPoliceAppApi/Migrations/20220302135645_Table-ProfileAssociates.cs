using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class TableProfileAssociates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileAssociates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AssociatesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAssociates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileAssociates_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAssociates_ProfileId",
                table: "ProfileAssociates",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileAssociates");
        }
    }
}
