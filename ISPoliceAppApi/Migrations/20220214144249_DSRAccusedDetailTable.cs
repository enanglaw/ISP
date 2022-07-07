using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class DSRAccusedDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlRoomDSRAccusedDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSRAccusedId = table.Column<int>(nullable: false),
                    number = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlRoomDSRAccusedDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlRoomDSRAccusedDetail_ControlRoomDSRAccused_DSRAccusedId",
                        column: x => x.DSRAccusedId,
                        principalTable: "ControlRoomDSRAccused",
                        principalColumn: "ControlRoomAccusedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlRoomDSRAccusedDetail_DSRAccusedId",
                table: "ControlRoomDSRAccusedDetail",
                column: "DSRAccusedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlRoomDSRAccusedDetail");
        }
    }
}
