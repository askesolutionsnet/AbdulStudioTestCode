using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcmeStudios.ApiRefactor.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudioItemTypes",
                columns: table => new
                {
                    StudioItemTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItemTypes", x => x.StudioItemTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StudioItems",
                columns: table => new
                {
                    StudioItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acquired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sold = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoldFor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Eurorack = table.Column<bool>(type: "bit", nullable: false),
                    StudioItemTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItems", x => x.StudioItemId);
                    table.ForeignKey(
                        name: "FK_StudioItems_StudioItemTypes_StudioItemTypeId",
                        column: x => x.StudioItemTypeId,
                        principalTable: "StudioItemTypes",
                        principalColumn: "StudioItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[,]
                {
                    { 1, "Synthesiser" },
                    { 2, "Drum Machine" },
                    { 3, "Effect" },
                    { 4, "Sequencer" },
                    { 5, "Mixer" },
                    { 6, "Oscillator" },
                    { 7, "Utility" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudioItems_StudioItemTypeId",
                table: "StudioItems",
                column: "StudioItemTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudioItems");

            migrationBuilder.DropTable(
                name: "StudioItemTypes");
        }
    }
}
