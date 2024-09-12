using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixUserAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResult_QuizzesParticipations_QuizParticipationId",
                table: "QuizResult");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizResult",
                table: "QuizResult");

            migrationBuilder.RenameTable(
                name: "UserAnswer",
                newName: "UserAnswers");

            migrationBuilder.RenameTable(
                name: "QuizResult",
                newName: "QuizResults");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_QuizParticipationId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_QuizParticipationId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizResult_QuizParticipationId",
                table: "QuizResults",
                newName: "IX_QuizResults_QuizParticipationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizResults",
                table: "QuizResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizResults",
                table: "QuizResults");

            migrationBuilder.RenameTable(
                name: "UserAnswers",
                newName: "UserAnswer");

            migrationBuilder.RenameTable(
                name: "QuizResults",
                newName: "QuizResult");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_QuizParticipationId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_QuizParticipationId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizResults_QuizParticipationId",
                table: "QuizResult",
                newName: "IX_QuizResult_QuizParticipationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizResult",
                table: "QuizResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResult_QuizzesParticipations_QuizParticipationId",
                table: "QuizResult",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswer",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id");
        }
    }
}
