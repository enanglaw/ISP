using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryMaster",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaster", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    VenueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<int>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowMaster",
                columns: table => new
                {
                    WorkflowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WorkflowMaster", x => x.WorkflowId);
                });

            migrationBuilder.CreateTable(
                name: "ZoneMaster",
                columns: table => new
                {
                    ZoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zone = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneMaster", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryMaster",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryMaster", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategoryMaster_CategoryMaster",
                        column: x => x.CategoryId,
                        principalTable: "CategoryMaster",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VenuePermissionType",
                columns: table => new
                {
                    VenuePermissionTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenuePermissionTypeName = table.Column<string>(maxLength: 100, nullable: false),
                    VenueId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<int>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenuePermissionType", x => x.VenuePermissionTypeId);
                    table.ForeignKey(
                        name: "FK_VenuePermissionType_Venue",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistrictMaster",
                columns: table => new
                {
                    DistrictId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneId = table.Column<int>(nullable: false),
                    District = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictMaster", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_DistrictMaster_ZoneMaster",
                        column: x => x.ZoneId,
                        principalTable: "ZoneMaster",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StationMaster",
                columns: table => new
                {
                    StationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(nullable: false),
                    StationName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationMaster", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_StationMaster_DistrictMaster",
                        column: x => x.DistrictId,
                        principalTable: "DistrictMaster",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMaster_ZoneId",
                table: "DistrictMaster",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_StationMaster_DistrictId",
                table: "StationMaster",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryMaster_CategoryId",
                table: "SubCategoryMaster",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VenuePermissionType_VenueId",
                table: "VenuePermissionType",
                column: "VenueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StationMaster");

            migrationBuilder.DropTable(
                name: "SubCategoryMaster");

            migrationBuilder.DropTable(
                name: "VenuePermissionType");

            migrationBuilder.DropTable(
                name: "WorkflowMaster");

            migrationBuilder.DropTable(
                name: "DistrictMaster");

            migrationBuilder.DropTable(
                name: "CategoryMaster");

            migrationBuilder.DropTable(
                name: "Venue");

            migrationBuilder.DropTable(
                name: "ZoneMaster");
        }
    }
}
