using Microsoft.EntityFrameworkCore.Migrations;

namespace tmetricstatistics.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfig",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfig", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    projectId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    projectName = table.Column<string>(nullable: true),
                    projectStatus = table.Column<int>(nullable: false),
                    clientId = table.Column<int>(nullable: false),
                    accountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.projectId);
                });

            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    endTime = table.Column<string>(nullable: true),
                    timerDuration = table.Column<int>(nullable: false),
                    startTime = table.Column<string>(nullable: true),
                    timeEntryId = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    projectName = table.Column<string>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfig");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TimeEntries");
        }
    }
}
