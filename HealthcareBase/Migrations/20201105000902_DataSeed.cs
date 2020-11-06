using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareBase.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "EmployeeID", "Name", "PatientMedicalRecordID" },
                values: new object[,]
                {
                    { 1, "381", null, "Serbia", null },
                    { 2, "389", null, "Macedonia", null },
                    { 3, "355", null, "Albania", null },
                    { 4, "382", null, "Montenegro", null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name", "PostalCode" },
                values: new object[] { 1, 1, "Novi Sad", "21000" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name", "PostalCode" },
                values: new object[] { 2, 1, "Belgrade", "11000" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name", "PostalCode" },
                values: new object[] { 3, 2, "Skopje", null });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "MedicalRecordID", "Address", "CityOfBirthId", "CityOfResidenceId", "DateOfBirth", "Gender", "InsuranceNumber", "Jmbg", "MartialStatus", "MiddleName", "Name", "Status", "Surname", "TelephoneNumber" },
                values: new object[] { 1, null, 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", null, null, "Married", null, "Milos", "Alive", "Milanovic", null });

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "Id", "Discriminator", "Password", "Username", "PatientId", "RespondedToSurvey" },
                values: new object[] { 1, "PatientAccount", "password", "milosmilanovic", 1, false });

            migrationBuilder.InsertData(
                table: "UserFeedbacks",
                columns: new[] { "Id", "Date", "UserComment", "UserId", "isAnonymous", "isPublic", "isPublished" },
                values: new object[] { 1, new DateTime(2020, 11, 5, 1, 9, 1, 404, DateTimeKind.Local).AddTicks(3682), "odlicno", 1, false, false, false });

            migrationBuilder.InsertData(
                table: "UserFeedbacks",
                columns: new[] { "Id", "Date", "UserComment", "UserId", "isAnonymous", "isPublic", "isPublished" },
                values: new object[] { 2, new DateTime(2020, 11, 5, 1, 9, 1, 411, DateTimeKind.Local).AddTicks(3622), "bravo", 1, false, false, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserFeedbacks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserFeedbacks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "MedicalRecordID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
