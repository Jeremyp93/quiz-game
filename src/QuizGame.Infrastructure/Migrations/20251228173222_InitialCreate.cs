using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Tags = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    TextFr = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    TextNl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AnswerFr = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    AnswerNl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    AltSpellings = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListQuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "McqQuestionDetails",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChoiceAFr = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    ChoiceANl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    ChoiceBFr = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    ChoiceBNl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    ChoiceCFr = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    ChoiceCNl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    CorrectChoice = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_McqQuestionDetails", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_McqQuestionDetails_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularQuestionDetails",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AnswerFr = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    AnswerNl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularQuestionDetails", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_RegularQuestionDetails_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListQuestionAnswers_QuestionId",
                table: "ListQuestionAnswers",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListQuestionAnswers");

            migrationBuilder.DropTable(
                name: "McqQuestionDetails");

            migrationBuilder.DropTable(
                name: "RegularQuestionDetails");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
