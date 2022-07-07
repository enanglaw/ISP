using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISPoliceAppApi.Migrations
{
    public partial class eventsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    SubOrganizationId = table.Column<int>(nullable: false),
                    LeaderId = table.Column<int>(nullable: false),
                    SubLeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
