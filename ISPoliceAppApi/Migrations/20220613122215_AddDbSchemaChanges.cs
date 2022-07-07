using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class AddDbSchemaChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allegations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Complainant = table.Column<string>(nullable: false),
                    PersonalProfileId = table.Column<int>(nullable: false),
                    DateOfComplaint = table.Column<DateTime>(nullable: false),
                    AccusedName = table.Column<string>(nullable: false),
                    AccusedPosting = table.Column<string>(nullable: false),
                    AccusedRank = table.Column<string>(nullable: false),
                    ComplaintDetails = table.Column<string>(nullable: false),
                    AttachmentPath = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allegations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maritals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maritals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization_Sub_Events_Rs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    SubOrganizationId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization_Sub_Events_Rs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization_Sub_Leaders_Rs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeadersId = table.Column<int>(nullable: false),
                    SubOrganizationId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization_Sub_Leaders_Rs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Ideology = table.Column<string>(nullable: false),
                    SymbolUrl = table.Column<string>(maxLength: 256, nullable: false),
                    SymbolPath = table.Column<string>(maxLength: 256, nullable: true),
                    FlagPath = table.Column<string>(maxLength: 512, nullable: true),
                    FlagUrl = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FatherName = table.Column<string>(nullable: false),
                    MotherName = table.Column<string>(nullable: false),
                    Spouse = table.Column<string>(nullable: true),
                    ChildFullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelFamilies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    PlanUrl = table.Column<string>(nullable: true),
                    PlanPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadersGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Designation = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadersGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadersGroups_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationEvents_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    MediaUrl = table.Column<string>(nullable: true),
                    MediaPath = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationMedias_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubOrganizationCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOrganizationCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubOrganizationCategories_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Alias = table.Column<string>(nullable: false),
                    PlaceOfBirth = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Caste = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: false),
                    PresentAddress = table.Column<string>(nullable: false),
                    NativeDistrict = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    StrinkingPersonalityTrait = table.Column<string>(nullable: true),
                    PresentPartyAffiliation = table.Column<string>(nullable: true),
                    PositionInTheParty = table.Column<string>(nullable: true),
                    ReligionId = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    MaritalStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaders_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leaders_Maritals_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "Maritals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leaders_Religions_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    PersonnelPhotoUrl = table.Column<string>(nullable: true),
                    PersonnelPhotoPath = table.Column<string>(nullable: true),
                    CurrentRank = table.Column<string>(nullable: false),
                    PersonalNumber = table.Column<string>(nullable: false),
                    DateOffBirth = table.Column<DateTime>(nullable: false),
                    DateOfEnlistment = table.Column<DateTime>(nullable: false),
                    PresentPosting = table.Column<string>(nullable: false),
                    DateOfJoiningPresentPosting = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: true),
                    MaritalStatusId = table.Column<int>(nullable: true),
                    ReligionId = table.Column<int>(nullable: true),
                    PersonnelFamilyId = table.Column<int>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: false),
                    PresentAddress = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnels_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_Maritals_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "Maritals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_PersonnelFamilies_PersonnelFamilyId",
                        column: x => x.PersonnelFamilyId,
                        principalTable: "PersonnelFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnels_Religions_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubOrganizationEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    subOrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOrganizationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubOrganizationEvents_SubOrganizationCategories_subOrganizationId",
                        column: x => x.subOrganizationId,
                        principalTable: "SubOrganizationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubOrganizationLeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Designation = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    SubOrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOrganizationLeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubOrganizationLeaders_SubOrganizationCategories_SubOrganizationId",
                        column: x => x.SubOrganizationId,
                        principalTable: "SubOrganizationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderEvents_Leaders_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Leaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LeaderMediaPath = table.Column<string>(nullable: true),
                    LeaderMediaUrl = table.Column<string>(maxLength: 256, nullable: true),
                    LeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderMedias_Leaders_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Leaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderPoliticalBackgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionYear = table.Column<DateTime>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    LeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderPoliticalBackgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderPoliticalBackgrounds_Leaders_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Leaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllegationEnquiries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ParticipantPath = table.Column<string>(nullable: false),
                    ParticipantUrl = table.Column<string>(nullable: true),
                    OutComePath = table.Column<string>(nullable: true),
                    OutComeUrl = table.Column<string>(nullable: true),
                    MOMPath = table.Column<string>(nullable: true),
                    MOMUrl = table.Column<string>(nullable: true),
                    NotesPath = table.Column<string>(nullable: true),
                    NotesUrl = table.Column<string>(nullable: true),
                    MemorandumPath = table.Column<string>(nullable: true),
                    MemorandumUrl = table.Column<string>(nullable: true),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllegationEnquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllegationEnquiries_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelCaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseCreatedDate = table.Column<DateTime>(nullable: false),
                    CaseNumber = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CaseSection = table.Column<string>(nullable: false),
                    CurrentStatus = table.Column<string>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelCaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelCaseDetails_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelEducationalBackgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionName = table.Column<string>(nullable: true),
                    CourseOfStudy = table.Column<string>(nullable: true),
                    QualificationName = table.Column<string>(nullable: false),
                    AdmissionYear = table.Column<DateTime>(nullable: false),
                    GraduationYear = table.Column<DateTime>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelEducationalBackgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelEducationalBackgrounds_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelGallantryAwards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    IssueingAuthority = table.Column<string>(nullable: false),
                    IssuingDate = table.Column<DateTime>(nullable: false),
                    AwardDocumentUrl = table.Column<string>(nullable: true),
                    GallantryAwardPath = table.Column<string>(nullable: true),
                    GallantryAwardUrl = table.Column<string>(maxLength: 256, nullable: true),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelGallantryAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelGallantryAwards_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelPostings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post = table.Column<string>(nullable: false),
                    Place = table.Column<string>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelPostings_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelPreviousAllegations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    AttachmentPath = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPreviousAllegations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelPreviousAllegations_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelWarningOrPunishments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    WarningCreatedDate = table.Column<DateTime>(nullable: false),
                    AttachmentPath = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true),
                    PersonnelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelWarningOrPunishments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelWarningOrPunishments_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllegationEnquiries_PersonnelId",
                table: "AllegationEnquiries",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderEvents_LeaderId",
                table: "LeaderEvents",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderMedias_LeaderId",
                table: "LeaderMedias",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderPoliticalBackgrounds_LeaderId",
                table: "LeaderPoliticalBackgrounds",
                column: "LeaderId");

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

            migrationBuilder.CreateIndex(
                name: "IX_LeadersGroups_OrganizationId",
                table: "LeadersGroups",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationEvents_OrganizationId",
                table: "OrganizationEvents",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationMedias_OrganizationId",
                table: "OrganizationMedias",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelCaseDetails_PersonnelId",
                table: "PersonnelCaseDetails",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelEducationalBackgrounds_PersonnelId",
                table: "PersonnelEducationalBackgrounds",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelGallantryAwards_PersonnelId",
                table: "PersonnelGallantryAwards",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPostings_PersonnelId",
                table: "PersonnelPostings",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPreviousAllegations_PersonnelId",
                table: "PersonnelPreviousAllegations",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_GenderId",
                table: "Personnels",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_MaritalStatusId",
                table: "Personnels",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_PersonnelFamilyId",
                table: "Personnels",
                column: "PersonnelFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_ReligionId",
                table: "Personnels",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelWarningOrPunishments_PersonnelId",
                table: "PersonnelWarningOrPunishments",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubOrganizationCategories_OrganizationId",
                table: "SubOrganizationCategories",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubOrganizationEvents_subOrganizationId",
                table: "SubOrganizationEvents",
                column: "subOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubOrganizationLeaders_SubOrganizationId",
                table: "SubOrganizationLeaders",
                column: "SubOrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllegationEnquiries");

            migrationBuilder.DropTable(
                name: "Allegations");

            migrationBuilder.DropTable(
                name: "LeaderEvents");

            migrationBuilder.DropTable(
                name: "LeaderMedias");

            migrationBuilder.DropTable(
                name: "LeaderPoliticalBackgrounds");

            migrationBuilder.DropTable(
                name: "LeadersGroups");

            migrationBuilder.DropTable(
                name: "Organization_Sub_Events_Rs");

            migrationBuilder.DropTable(
                name: "Organization_Sub_Leaders_Rs");

            migrationBuilder.DropTable(
                name: "OrganizationEvents");

            migrationBuilder.DropTable(
                name: "OrganizationMedias");

            migrationBuilder.DropTable(
                name: "PersonnelCaseDetails");

            migrationBuilder.DropTable(
                name: "PersonnelEducationalBackgrounds");

            migrationBuilder.DropTable(
                name: "PersonnelGallantryAwards");

            migrationBuilder.DropTable(
                name: "PersonnelPostings");

            migrationBuilder.DropTable(
                name: "PersonnelPreviousAllegations");

            migrationBuilder.DropTable(
                name: "PersonnelWarningOrPunishments");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "SubOrganizationEvents");

            migrationBuilder.DropTable(
                name: "SubOrganizationLeaders");

            migrationBuilder.DropTable(
                name: "Leaders");

            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropTable(
                name: "SubOrganizationCategories");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Maritals");

            migrationBuilder.DropTable(
                name: "PersonnelFamilies");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
