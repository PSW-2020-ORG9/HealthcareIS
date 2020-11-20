using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareBase.Migrations
{
    public partial class AddDoctorSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatedSurveyQuestions_Employees_DoctorId",
                table: "RatedSurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_RatedSurveyQuestions_DoctorSurveySectionId",
                table: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_RatedSurveyQuestions_DoctorId",
                table: "RatedSurveyQuestions");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "RatedSurveyQuestions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "RatedSurveyQuestions");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "RatedSurveySections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "RatedSurveySections",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_RatedSurveySections_DoctorId",
                table: "RatedSurveySections",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatedSurveySections_Employees_DoctorId",
                table: "RatedSurveySections",
                column: "DoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_RatedSurveySections_DoctorSurveySectionId",
                table: "SurveyResponses",
                column: "DoctorSurveySectionId",
                principalTable: "RatedSurveySections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatedSurveySections_Employees_DoctorId",
                table: "RatedSurveySections");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_RatedSurveySections_DoctorSurveySectionId",
                table: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_RatedSurveySections_DoctorId",
                table: "RatedSurveySections");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "RatedSurveySections");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "RatedSurveySections");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "RatedSurveyQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "RatedSurveyQuestions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RatedSurveyQuestions_DoctorId",
                table: "RatedSurveyQuestions",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatedSurveyQuestions_Employees_DoctorId",
                table: "RatedSurveyQuestions",
                column: "DoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_RatedSurveyQuestions_DoctorSurveySectionId",
                table: "SurveyResponses",
                column: "DoctorSurveySectionId",
                principalTable: "RatedSurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
