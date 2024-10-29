using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_OwnerId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_OwnerId",
                table: "Quizzes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_OwnerId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_OwnerId",
                table: "Quizzes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id");
        }
    }
}
