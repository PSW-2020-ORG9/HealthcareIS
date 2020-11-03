using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareBase.Migrations
{
    public partial class UserFeedbackModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAnonymous",
                table: "UserFeedbacks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublic",
                table: "UserFeedbacks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "UserFeedbacks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAnonymous",
                table: "UserFeedbacks");

            migrationBuilder.DropColumn(
                name: "isPublic",
                table: "UserFeedbacks");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "UserFeedbacks");
        }
    }
}
