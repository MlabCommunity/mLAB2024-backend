using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesParticipations_AspNetUsers_ParticipantId",
                table: "QuizzesParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Answers_AnswerId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_AnswerId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizzesParticipations",
                table: "QuizzesParticipations");

            migrationBuilder.RenameTable(
                name: "QuizzesParticipations",
                newName: "QuizParticipations");

            migrationBuilder.RenameIndex(
                name: "IX_QuizzesParticipations_QuizId",
                table: "QuizParticipations",
                newName: "IX_QuizParticipations_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizzesParticipations_ParticipantId",
                table: "QuizParticipations",
                newName: "IX_QuizParticipations_ParticipantId");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "QuizParticipations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "GuestParticipant",
                table: "QuizParticipations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipationType",
                table: "QuizParticipations",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizParticipations",
                table: "QuizParticipations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizParticipations_AspNetUsers_ParticipantId",
                table: "QuizParticipations",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizParticipations_Quizzes_QuizId",
                table: "QuizParticipations",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_QuizParticipations_QuizParticipationId",
                table: "QuizResults",
                column: "QuizParticipationId",
                principalTable: "QuizParticipations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizParticipations_QuizParticipationId",
                table: "UserAnswers",
                column: "QuizParticipationId",
                principalTable: "QuizParticipations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizParticipations_AspNetUsers_ParticipantId",
                table: "QuizParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizParticipations_Quizzes_QuizId",
                table: "QuizParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_QuizParticipations_QuizParticipationId",
                table: "QuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizParticipations_QuizParticipationId",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizParticipations",
                table: "QuizParticipations");

            migrationBuilder.DropColumn(
                name: "GuestParticipant",
                table: "QuizParticipations");

            migrationBuilder.DropColumn(
                name: "ParticipationType",
                table: "QuizParticipations");

            migrationBuilder.RenameTable(
                name: "QuizParticipations",
                newName: "QuizzesParticipations");

            migrationBuilder.RenameIndex(
                name: "IX_QuizParticipations_QuizId",
                table: "QuizzesParticipations",
                newName: "IX_QuizzesParticipations_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizParticipations_ParticipantId",
                table: "QuizzesParticipations",
                newName: "IX_QuizzesParticipations_ParticipantId");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "QuizzesParticipations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizzesParticipations",
                table: "QuizzesParticipations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_AnswerId",
                table: "UserAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_QuizzesParticipations_QuizParticipationId",
                table: "QuizResults",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesParticipations_AspNetUsers_ParticipantId",
                table: "QuizzesParticipations",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesParticipations_Quizzes_QuizId",
                table: "QuizzesParticipations",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Answers_AnswerId",
                table: "UserAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizzesParticipations_QuizParticipationId",
                table: "UserAnswers",
                column: "QuizParticipationId",
                principalTable: "QuizzesParticipations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
