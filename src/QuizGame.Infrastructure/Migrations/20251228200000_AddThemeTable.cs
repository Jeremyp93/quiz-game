using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddThemeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Themes table
            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NameFr = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    NameNl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            // Create unique index on Code
            migrationBuilder.CreateIndex(
                name: "IX_Themes_Code",
                table: "Themes",
                column: "Code",
                unique: true);

            // Add ThemeId column to Questions table
            migrationBuilder.AddColumn<Guid>(
                name: "ThemeId",
                table: "Questions",
                type: "TEXT",
                nullable: true);

            // Create foreign key
            migrationBuilder.CreateIndex(
                name: "IX_Questions_ThemeId",
                table: "Questions",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Themes_ThemeId",
                table: "Questions",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Themes_ThemeId",
                table: "Questions");

            // Drop index
            migrationBuilder.DropIndex(
                name: "IX_Questions_ThemeId",
                table: "Questions");

            // Drop ThemeId column
            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Questions");

            // Drop Themes table
            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
