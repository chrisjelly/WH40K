using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JellyDev.WH40K.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WH");

            migrationBuilder.CreateTable(
                name: "Factions",
                schema: "WH",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "WH",
                table: "Factions",
                columns: new[] { "Id", "Created", "LastUpdated", "Name" },
                values: new object[] { new Guid("16fb2151-3ded-4e74-9215-cde3a68cf661"), new DateTime(2021, 10, 11, 3, 54, 15, 906, DateTimeKind.Utc).AddTicks(9116), null, "Necrons" });

            migrationBuilder.InsertData(
                schema: "WH",
                table: "Factions",
                columns: new[] { "Id", "Created", "LastUpdated", "Name" },
                values: new object[] { new Guid("749a7624-9d4a-4426-b2aa-d00073379e51"), new DateTime(2021, 10, 11, 3, 54, 15, 906, DateTimeKind.Utc).AddTicks(9508), null, "Space Marines" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factions",
                schema: "WH");
        }
    }
}
