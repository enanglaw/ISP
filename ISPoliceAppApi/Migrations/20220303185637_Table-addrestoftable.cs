using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class Tableaddrestoftable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "dateOfInitiation",
                table: "ProfileTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActionDate",
                table: "ProfileTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BailDate",
                table: "ProfileTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "ProfileTransaction",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "ProfileMaster",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "ProfileAssociates",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "ProfileAlias",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CaseDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    ps = table.Column<string>(nullable: true),
                    cr = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    Head = table.Column<string>(nullable: true),
                    io = table.Column<string>(nullable: true),
                    Court = table.Column<string>(nullable: true),
                    Goondas = table.Column<string>(nullable: true),
                    Stage = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Dsr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseDetail_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileAbstract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    DistCity = table.Column<int>(nullable: false),
                    DistCityId = table.Column<int>(nullable: false),
                    Jurisdiction = table.Column<string>(nullable: true),
                    Murder = table.Column<int>(nullable: false),
                    AttmptMurder = table.Column<int>(nullable: false),
                    Ndps = table.Column<int>(nullable: false),
                    Robbery = table.Column<int>(nullable: false),
                    ChainSnatch = table.Column<int>(nullable: false),
                    MobileSnatch = table.Column<int>(nullable: false),
                    HbDay = table.Column<int>(nullable: false),
                    HbNight = table.Column<int>(nullable: false),
                    OtherCase = table.Column<int>(nullable: false),
                    TechCase = table.Column<int>(nullable: false),
                    TotalCase = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAbstract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileAbstract_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileChildren",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileChildren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileChildren_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileSibling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    relation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSibling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileSibling_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileSpouse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSpouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileSpouse_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseDetail_ProfileId",
                table: "CaseDetail",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAbstract_ProfileId",
                table: "ProfileAbstract",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileChildren_ProfileId",
                table: "ProfileChildren",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSibling_ProfileId",
                table: "ProfileSibling",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSpouse_ProfileId",
                table: "ProfileSpouse",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseDetail");

            migrationBuilder.DropTable(
                name: "ProfileAbstract");

            migrationBuilder.DropTable(
                name: "ProfileChildren");

            migrationBuilder.DropTable(
                name: "ProfileSibling");

            migrationBuilder.DropTable(
                name: "ProfileSpouse");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "ProfileTransaction");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "ProfileMaster");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "ProfileAssociates");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "ProfileAlias");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateOfInitiation",
                table: "ProfileTransaction",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActionDate",
                table: "ProfileTransaction",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BailDate",
                table: "ProfileTransaction",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
