using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JellyDev.WH40K.Infrastructure.Migrations.StratagemDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WH");

            migrationBuilder.CreateTable(
                name: "Stratagems",
                schema: "WH",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommandPoints = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stratagems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stratagem_Phases",
                schema: "WH",
                columns: table => new
                {
                    Phase = table.Column<int>(type: "int", nullable: false),
                    StratagemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stratagem_Phases", x => new { x.StratagemId, x.Phase });
                    table.ForeignKey(
                        name: "FK_Stratagem_Phases_Stratagems_StratagemId",
                        column: x => x.StratagemId,
                        principalSchema: "WH",
                        principalTable: "Stratagems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stratagem_Phases",
                schema: "WH");

            migrationBuilder.DropTable(
                name: "Stratagems",
                schema: "WH");
        }
    }
}
