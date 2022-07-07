using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class addDRSTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlRoomDSRCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlRoomDSRCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ControlRoomDSR",
                columns: table => new
                {
                    ControlRoomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    PSName = table.Column<int>(nullable: false),
                    GivenBy = table.Column<string>(nullable: false),
                    TakenBy = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    CaseNo = table.Column<string>(nullable: true),
                    Do = table.Column<DateTime>(nullable: false),
                    Dr = table.Column<DateTime>(nullable: false),
                    DrSource = table.Column<string>(nullable: true),
                    SOC = table.Column<string>(nullable: true),
                    Complainant = table.Column<string>(nullable: true),
                    ComplainantAddress = table.Column<string>(nullable: true),
                    PL = table.Column<string>(nullable: true),
                    PR = table.Column<string>(nullable: true),
                    TotalAccused = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    PSNote = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlRoomDSR", x => x.ControlRoomId);
                    table.ForeignKey(
                        name: "FK_ControlRoomDSR_ControlRoomDSRCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ControlRoomDSRCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlRoomDSRAccused",
                columns: table => new
                {
                    ControlRoomAccusedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControlRoomId = table.Column<int>(nullable: false),
                    HSNbr = table.Column<string>(nullable: true),
                    AccusedName = table.Column<string>(nullable: true),
                    AccusedAddress = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CrimeNumber = table.Column<string>(nullable: true),
                    SectionNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlRoomDSRAccused", x => x.ControlRoomAccusedId);
                    table.ForeignKey(
                        name: "FK_ControlRoomDSRAccused_ControlRoomDSR_ControlRoomId",
                        column: x => x.ControlRoomId,
                        principalTable: "ControlRoomDSR",
                        principalColumn: "ControlRoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlRoomDSR_CategoryId",
                table: "ControlRoomDSR",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlRoomDSRAccused_ControlRoomId",
                table: "ControlRoomDSRAccused",
                column: "ControlRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlRoomDSRAccused");

            migrationBuilder.DropTable(
                name: "ControlRoomDSR");

            migrationBuilder.DropTable(
                name: "ControlRoomDSRCategory");
        }
    }
}
