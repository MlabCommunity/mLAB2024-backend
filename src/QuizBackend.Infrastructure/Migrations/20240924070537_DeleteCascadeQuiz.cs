using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}
