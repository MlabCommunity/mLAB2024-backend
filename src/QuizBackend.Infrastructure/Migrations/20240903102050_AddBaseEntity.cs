using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBackend.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddBaseEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Description",
            table: "Questions");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAtUtc",
            table: "QuizzesParticipations",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedAtUtc",
            table: "QuizzesParticipations",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAtUtc",
            table: "Answers",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedAtUtc",
            table: "Answers",
            type: "datetime2",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedAtUtc",
            table: "QuizzesParticipations");

        migrationBuilder.DropColumn(
            name: "UpdatedAtUtc",
            table: "QuizzesParticipations");

        migrationBuilder.DropColumn(
            name: "CreatedAtUtc",
            table: "Answers");

        migrationBuilder.DropColumn(
            name: "UpdatedAtUtc",
            table: "Answers");

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Questions",
            type: "nvarchar(max)",
            nullable: true);
    }
}
