using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class Profileandalias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonStatusMaster",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonStatusMaster", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypeMaster",
                columns: table => new
                {
                    PersonTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypeMaster", x => x.PersonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ProfileMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Hs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(maxLength: 50, nullable: false),
                    ParentName = table.Column<string>(maxLength: 50, nullable: true),
                    PrimaryAddress = table.Column<string>(maxLength: 500, nullable: true),
                    HistorySheetNo = table.Column<string>(maxLength: 50, nullable: true),
                    CurrentActivity = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    ModusOperandi = table.Column<string>(nullable: true),
                    GangId = table.Column<int>(nullable: true),
                    GangMemberType = table.Column<int>(nullable: true),
                    PhotoPath = table.Column<string>(maxLength: 256, nullable: true),
                    PhotoUrl = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Person_PersonStatusMaster",
                        column: x => x.StatusId,
                        principalTable: "PersonStatusMaster",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileAlias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileAlias_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    PermanentAddres = table.Column<string>(nullable: true),
                    PresentAddress = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    MartialStatus = table.Column<int>(nullable: false),
                    SpouseName = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    NoOfGoondas = table.Column<string>(nullable: true),
                    SecurityProceeding = table.Column<string>(nullable: true),
                    dateOfInitiation = table.Column<DateTime>(nullable: false),
                    LastAction = table.Column<string>(nullable: true),
                    LastActionDate = table.Column<DateTime>(nullable: false),
                    Bail = table.Column<string>(nullable: true),
                    BailDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileTransaction_ProfileMaster_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    AddressLabel = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "(N'')"),
                    AddressText = table.Column<string>(maxLength: 500, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_PersonAddress_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAliasName",
                columns: table => new
                {
                    AliasNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    AliasName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAliasName", x => x.AliasNameId);
                    table.ForeignKey(
                        name: "FK_PersonAliasName_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonCaseHistory",
                columns: table => new
                {
                    CaseHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    CaseId = table.Column<int>(nullable: false),
                    CaseStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCaseHistory", x => x.CaseHistoryId);
                    table.ForeignKey(
                        name: "FK_PersonCaseHistory_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonMedia",
                columns: table => new
                {
                    MediaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    MediaLabel = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "(N'')"),
                    MediaActualName = table.Column<string>(maxLength: 256, nullable: false),
                    MediaPath = table.Column<string>(maxLength: 50, nullable: false),
                    MediaUrl = table.Column<string>(maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMedia", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_PersonMedia_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPersonType",
                columns: table => new
                {
                    PersonPersonTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    PersonTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPersonType", x => x.PersonPersonTypeId);
                    table.ForeignKey(
                        name: "FK_PersonPersonType_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPersonType_PersonTypeMaster",
                        column: x => x.PersonTypeId,
                        principalTable: "PersonTypeMaster",
                        principalColumn: "PersonTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonRivalGang",
                columns: table => new
                {
                    RivalGangId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    GangId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRivalGang", x => x.RivalGangId);
                    table.ForeignKey(
                        name: "FK_PersonRivalGang_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_StatusId",
                table: "Person",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddress_PersonId",
                table: "PersonAddress",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAliasName_PersonId",
                table: "PersonAliasName",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCaseHistory_PersonId",
                table: "PersonCaseHistory",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMedia_PersonId",
                table: "PersonMedia",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPersonType_PersonId",
                table: "PersonPersonType",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPersonType_PersonTypeId",
                table: "PersonPersonType",
                column: "PersonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRivalGang_PersonId",
                table: "PersonRivalGang",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAlias_ProfileId",
                table: "ProfileAlias",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTransaction_ProfileId",
                table: "ProfileTransaction",
                column: "ProfileId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonAddress");

            migrationBuilder.DropTable(
                name: "PersonAliasName");

            migrationBuilder.DropTable(
                name: "PersonCaseHistory");

            migrationBuilder.DropTable(
                name: "PersonMedia");

            migrationBuilder.DropTable(
                name: "PersonPersonType");

            migrationBuilder.DropTable(
                name: "PersonRivalGang");

            migrationBuilder.DropTable(
                name: "ProfileAlias");

            migrationBuilder.DropTable(
                name: "ProfileTransaction");

            migrationBuilder.DropTable(
                name: "PersonTypeMaster");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "ProfileMaster");

            migrationBuilder.DropTable(
                name: "PersonStatusMaster");
        }
    }
}
