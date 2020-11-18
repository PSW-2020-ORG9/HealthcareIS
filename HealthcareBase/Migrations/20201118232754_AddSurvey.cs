using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareBase.Migrations
{
    public partial class AddSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ratedSurveyQuestions_RatedSurveySections_RatedSurveySectionId",
                table: "ratedSurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ratedSurveyQuestions_SurveyQuestions_SurveyQuestionId",
                table: "ratedSurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ratedSurveyQuestions",
                table: "ratedSurveyQuestions");

            migrationBuilder.RenameTable(
                name: "ratedSurveyQuestions",
                newName: "RatedSurveyQuestions");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "RatedSurveyQuestions",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_ratedSurveyQuestions_SurveyQuestionId",
                table: "RatedSurveyQuestions",
                newName: "IX_RatedSurveyQuestions_SurveyQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ratedSurveyQuestions_RatedSurveySectionId",
                table: "RatedSurveyQuestions",
                newName: "IX_RatedSurveyQuestions_RatedSurveySectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatedSurveyQuestions",
                table: "RatedSurveyQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatedSurveyQuestions_RatedSurveySections_RatedSurveySectionId",
                table: "RatedSurveyQuestions",
                column: "RatedSurveySectionId",
                principalTable: "RatedSurveySections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatedSurveyQuestions_SurveyQuestions_SurveyQuestionId",
                table: "RatedSurveyQuestions",
                column: "SurveyQuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatedSurveyQuestions_RatedSurveySections_RatedSurveySectionId",
                table: "RatedSurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_RatedSurveyQuestions_SurveyQuestions_SurveyQuestionId",
                table: "RatedSurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatedSurveyQuestions",
                table: "RatedSurveyQuestions");

            migrationBuilder.RenameTable(
                name: "RatedSurveyQuestions",
                newName: "ratedSurveyQuestions");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "ratedSurveyQuestions",
                newName: "rating");

            migrationBuilder.RenameIndex(
                name: "IX_RatedSurveyQuestions_SurveyQuestionId",
                table: "ratedSurveyQuestions",
                newName: "IX_ratedSurveyQuestions_SurveyQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_RatedSurveyQuestions_RatedSurveySectionId",
                table: "ratedSurveyQuestions",
                newName: "IX_ratedSurveyQuestions_RatedSurveySectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ratedSurveyQuestions",
                table: "ratedSurveyQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ratedSurveyQuestions_RatedSurveySections_RatedSurveySectionId",
                table: "ratedSurveyQuestions",
                column: "RatedSurveySectionId",
                principalTable: "RatedSurveySections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratedSurveyQuestions_SurveyQuestions_SurveyQuestionId",
                table: "ratedSurveyQuestions",
                column: "SurveyQuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
