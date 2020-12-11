using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aries1211.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Readings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pressure = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: true),
                    Oxygen = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readings");
        }
    }
}
