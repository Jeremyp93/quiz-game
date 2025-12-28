using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddThemeIconSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Themes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Themes");
        }
    }
}
