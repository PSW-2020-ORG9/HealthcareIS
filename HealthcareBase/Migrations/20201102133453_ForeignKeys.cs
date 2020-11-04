using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareBase.Migrations
{
    public partial class ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergyManifestation_Allergies_AllergyId",
                table: "AllergyManifestation");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthors_Employees_DoctorEmployeeID",
                table: "BlogAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogAuthors_AuthorId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableStorageRecords_MedicalConsumables_ConsumableId",
                table: "ConsumableStorageRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisDetails_Diagnoses_DiagnosisIcd",
                table: "DiagnosisDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Cities_CityOfResidenceId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMemberDiagnosis_Diagnoses_DiagnosisIcd",
                table: "FamilyMemberDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalizationNotifications_Hospitalizations_Hospitalizatio~",
                table: "HospitalizationNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalizationNotifications_UserAccount_UserId",
                table: "HospitalizationNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Diagnoses_DiagnosisIcd",
                table: "Hospitalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Patients_PatientMedicalRecordID",
                table: "Hospitalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptionNotifications_MedicationPrescriptions_~",
                table: "MedicationPrescriptionNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptionNotifications_UserAccount_UserId",
                table: "MedicationPrescriptionNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Diagnoses_DiagnosisIcd",
                table: "MedicationPrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Patients_PatientMedicalRecordID",
                table: "MedicationPrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Employees_PrescribedByEmployeeID",
                table: "MedicationPrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationStorageRecords_Medications_MedicationId",
                table: "MedicationStorageRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Cities_CityOfBirthId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Cities_CityOfResidenceId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyResponses_Employees_BestDoctorEmployeeID",
                table: "PatientSurveyResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyResponses_UserAccount_PatientId",
                table: "PatientSurveyResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Diagnoses_DiagnosisIcd",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Employees_DoctorEmployeeID",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Patients_PatientMedicalRecordID",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_ProcedureTypes_ProcedureTypeId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Procedure_ReferredFromId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Rooms_RoomId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Diagnoses_Surgery_DiagnosisIcd",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureNotifications_Procedure_ProcedureId",
                table: "ProcedureNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureNotifications_UserAccount_UserId",
                table: "ProcedureNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Renovations_Rooms_RoomId",
                table: "Renovations");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Employees_DoctorEmployeeID",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Rooms_RoomId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Medications_MedicationId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_UserAccount_ReviewerId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_UserAccount_SenderId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Patients_PatientMedicalRecordID",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Patients_ScheduleProcedure_PatientMedicalRecordID",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Rooms_Preference_PreferredRoomId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Employees_Preference_PreferredDoctorEmployeeID",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Rooms_ProcedureSchedulingPreference_Preference_Prefe~",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestNotifications_Request_RequestId",
                table: "RequestNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestNotifications_UserAccount_UserId",
                table: "RequestNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Rooms_AssignedExamRoomId",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employees_DoctorEmployeeID",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Employees_EmployeeID",
                table: "UserAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Patients_PatientMedicalRecordID",
                table: "UserAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFeedbacks_UserAccount_UserId",
                table: "UserFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_PatientMedicalRecordID",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_DoctorEmployeeID",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Request_DoctorEmployeeID",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_PatientMedicalRecordID",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_ScheduleProcedure_PatientMedicalRecordID",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_Preference_PreferredDoctorEmployeeID",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_DiagnosisIcd",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_DoctorEmployeeID",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_PatientMedicalRecordID",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_Surgery_DiagnosisIcd",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_PatientSurveyResponses_BestDoctorEmployeeID",
                table: "PatientSurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPrescriptions_DiagnosisIcd",
                table: "MedicationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPrescriptions_PatientMedicalRecordID",
                table: "MedicationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPrescriptions_PrescribedByEmployeeID",
                table: "MedicationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_DiagnosisIcd",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_PatientMedicalRecordID",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_FamilyMemberDiagnosis_DiagnosisIcd",
                table: "FamilyMemberDiagnosis");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisDetails_DiagnosisIcd",
                table: "DiagnosisDetails");

            migrationBuilder.DropIndex(
                name: "IX_BlogAuthors_DoctorEmployeeID",
                table: "BlogAuthors");

            migrationBuilder.DropColumn(
                name: "PatientMedicalRecordID",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "DoctorEmployeeID",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "DoctorEmployeeID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PatientMedicalRecordID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ScheduleProcedure_PatientMedicalRecordID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Preference_PreferredDoctorEmployeeID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "DiagnosisIcd",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "DoctorEmployeeID",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "PatientMedicalRecordID",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "Surgery_DiagnosisIcd",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "BestDoctorEmployeeID",
                table: "PatientSurveyResponses");

            migrationBuilder.DropColumn(
                name: "DiagnosisIcd",
                table: "MedicationPrescriptions");

            migrationBuilder.DropColumn(
                name: "PatientMedicalRecordID",
                table: "MedicationPrescriptions");

            migrationBuilder.DropColumn(
                name: "PrescribedByEmployeeID",
                table: "MedicationPrescriptions");

            migrationBuilder.DropColumn(
                name: "DiagnosisIcd",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "PatientMedicalRecordID",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "DiagnosisIcd",
                table: "FamilyMemberDiagnosis");

            migrationBuilder.DropColumn(
                name: "DiagnosisIcd",
                table: "DiagnosisDetails");

            migrationBuilder.DropColumn(
                name: "DoctorEmployeeID",
                table: "BlogAuthors");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "UserAccount",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAccount_EmployeeID",
                table: "UserAccount",
                newName: "IX_UserAccount_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserFeedbacks",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "UserAccount",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssignedExamRoomId",
                table: "Shifts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Shifts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RequestNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "RequestNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Request",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReviewerId",
                table: "Request",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleProcedure_PatientId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Preference_PreferredDoctorId",
                table: "Request",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Renovations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProcedureNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureId",
                table: "ProcedureNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Procedure",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReferredFromId",
                table: "Procedure",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureTypeId",
                table: "Procedure",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisId",
                table: "Procedure",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Procedure",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Procedure",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Surgery_DiagnosisId",
                table: "Procedure",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientSurveyResponses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BestDoctorId",
                table: "PatientSurveyResponses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CityOfResidenceId",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityOfBirthId",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicationId",
                table: "MedicationStorageRecords",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisId",
                table: "MedicationPrescriptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "MedicationPrescriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrescribedById",
                table: "MedicationPrescriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MedicationPrescriptionNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "MedicationPrescriptionNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisId",
                table: "Hospitalizations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Hospitalizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "HospitalizationNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HospitalizationId",
                table: "HospitalizationNotifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisId",
                table: "FamilyMemberDiagnosis",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityOfResidenceId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisId",
                table: "DiagnosisDetails",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsumableId",
                table: "ConsumableStorageRecords",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "BlogPosts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "BlogAuthors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AllergyId",
                table: "AllergyManifestation",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_PatientId",
                table: "UserAccount",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DoctorId",
                table: "Shifts",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_DoctorId",
                table: "Request",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_PatientId",
                table: "Request",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ScheduleProcedure_PatientId",
                table: "Request",
                column: "ScheduleProcedure_PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Preference_PreferredDoctorId",
                table: "Request",
                column: "Preference_PreferredDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_DiagnosisId",
                table: "Procedure",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_DoctorId",
                table: "Procedure",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_PatientId",
                table: "Procedure",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_Surgery_DiagnosisId",
                table: "Procedure",
                column: "Surgery_DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSurveyResponses_BestDoctorId",
                table: "PatientSurveyResponses",
                column: "BestDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_DiagnosisId",
                table: "MedicationPrescriptions",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_PatientId",
                table: "MedicationPrescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_PrescribedById",
                table: "MedicationPrescriptions",
                column: "PrescribedById");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_DiagnosisId",
                table: "Hospitalizations",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_PatientId",
                table: "Hospitalizations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMemberDiagnosis_DiagnosisId",
                table: "FamilyMemberDiagnosis",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisDetails_DiagnosisId",
                table: "DiagnosisDetails",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogAuthors_DoctorId",
                table: "BlogAuthors",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergyManifestation_Allergies_AllergyId",
                table: "AllergyManifestation",
                column: "AllergyId",
                principalTable: "Allergies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthors_Employees_DoctorId",
                table: "BlogAuthors",
                column: "DoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogAuthors_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "BlogAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableStorageRecords_MedicalConsumables_ConsumableId",
                table: "ConsumableStorageRecords",
                column: "ConsumableId",
                principalTable: "MedicalConsumables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisDetails_Diagnoses_DiagnosisId",
                table: "DiagnosisDetails",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Cities_CityOfResidenceId",
                table: "Employees",
                column: "CityOfResidenceId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMemberDiagnosis_Diagnoses_DiagnosisId",
                table: "FamilyMemberDiagnosis",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalizationNotifications_Hospitalizations_Hospitalizatio~",
                table: "HospitalizationNotifications",
                column: "HospitalizationId",
                principalTable: "Hospitalizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalizationNotifications_UserAccount_UserId",
                table: "HospitalizationNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Diagnoses_DiagnosisId",
                table: "Hospitalizations",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Patients_PatientId",
                table: "Hospitalizations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptionNotifications_MedicationPrescriptions_~",
                table: "MedicationPrescriptionNotifications",
                column: "PrescriptionId",
                principalTable: "MedicationPrescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptionNotifications_UserAccount_UserId",
                table: "MedicationPrescriptionNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Diagnoses_DiagnosisId",
                table: "MedicationPrescriptions",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Patients_PatientId",
                table: "MedicationPrescriptions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Employees_PrescribedById",
                table: "MedicationPrescriptions",
                column: "PrescribedById",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationStorageRecords_Medications_MedicationId",
                table: "MedicationStorageRecords",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Cities_CityOfBirthId",
                table: "Patients",
                column: "CityOfBirthId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Cities_CityOfResidenceId",
                table: "Patients",
                column: "CityOfResidenceId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSurveyResponses_Employees_BestDoctorId",
                table: "PatientSurveyResponses",
                column: "BestDoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSurveyResponses_UserAccount_PatientId",
                table: "PatientSurveyResponses",
                column: "PatientId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Diagnoses_DiagnosisId",
                table: "Procedure",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Employees_DoctorId",
                table: "Procedure",
                column: "DoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Patients_PatientId",
                table: "Procedure",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_ProcedureTypes_ProcedureTypeId",
                table: "Procedure",
                column: "ProcedureTypeId",
                principalTable: "ProcedureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Procedure_ReferredFromId",
                table: "Procedure",
                column: "ReferredFromId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Rooms_RoomId",
                table: "Procedure",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Diagnoses_Surgery_DiagnosisId",
                table: "Procedure",
                column: "Surgery_DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureNotifications_Procedure_ProcedureId",
                table: "ProcedureNotifications",
                column: "ProcedureId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureNotifications_UserAccount_UserId",
                table: "ProcedureNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Renovations_Rooms_RoomId",
                table: "Renovations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Employees_DoctorId",
                table: "Request",
                column: "DoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Rooms_RoomId",
                table: "Request",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Medications_MedicationId",
                table: "Request",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_UserAccount_ReviewerId",
                table: "Request",
                column: "ReviewerId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_UserAccount_SenderId",
                table: "Request",
                column: "SenderId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Patients_PatientId",
                table: "Request",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Patients_ScheduleProcedure_PatientId",
                table: "Request",
                column: "ScheduleProcedure_PatientId",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Rooms_Preference_PreferredRoomId",
                table: "Request",
                column: "Preference_PreferredRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Employees_Preference_PreferredDoctorId",
                table: "Request",
                column: "Preference_PreferredDoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Rooms_ProcedureSchedulingPreference_Preference_Prefe~",
                table: "Request",
                column: "ProcedureSchedulingPreference_Preference_PreferredRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestNotifications_Request_RequestId",
                table: "RequestNotifications",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestNotifications_UserAccount_UserId",
                table: "RequestNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Rooms_AssignedExamRoomId",
                table: "Shifts",
                column: "AssignedExamRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employees_DoctorId",
                table: "Shifts",
                column: "DoctorId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Employees_EmployeeId",
                table: "UserAccount",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Patients_PatientId",
                table: "UserAccount",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFeedbacks_UserAccount_UserId",
                table: "UserFeedbacks",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergyManifestation_Allergies_AllergyId",
                table: "AllergyManifestation");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthors_Employees_DoctorId",
                table: "BlogAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogAuthors_AuthorId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableStorageRecords_MedicalConsumables_ConsumableId",
                table: "ConsumableStorageRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisDetails_Diagnoses_DiagnosisId",
                table: "DiagnosisDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Cities_CityOfResidenceId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMemberDiagnosis_Diagnoses_DiagnosisId",
                table: "FamilyMemberDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalizationNotifications_Hospitalizations_Hospitalizatio~",
                table: "HospitalizationNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalizationNotifications_UserAccount_UserId",
                table: "HospitalizationNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Diagnoses_DiagnosisId",
                table: "Hospitalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Patients_PatientId",
                table: "Hospitalizations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptionNotifications_MedicationPrescriptions_~",
                table: "MedicationPrescriptionNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptionNotifications_UserAccount_UserId",
                table: "MedicationPrescriptionNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Diagnoses_DiagnosisId",
                table: "MedicationPrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Patients_PatientId",
                table: "MedicationPrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Employees_PrescribedById",
                table: "MedicationPrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationStorageRecords_Medications_MedicationId",
                table: "MedicationStorageRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Cities_CityOfBirthId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Cities_CityOfResidenceId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyResponses_Employees_BestDoctorId",
                table: "PatientSurveyResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyResponses_UserAccount_PatientId",
                table: "PatientSurveyResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Diagnoses_DiagnosisId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Employees_DoctorId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Patients_PatientId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_ProcedureTypes_ProcedureTypeId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Procedure_ReferredFromId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Rooms_RoomId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedure_Diagnoses_Surgery_DiagnosisId",
                table: "Procedure");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureNotifications_Procedure_ProcedureId",
                table: "ProcedureNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureNotifications_UserAccount_UserId",
                table: "ProcedureNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Renovations_Rooms_RoomId",
                table: "Renovations");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Employees_DoctorId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Rooms_RoomId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Medications_MedicationId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_UserAccount_ReviewerId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_UserAccount_SenderId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Patients_PatientId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Patients_ScheduleProcedure_PatientId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Rooms_Preference_PreferredRoomId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Employees_Preference_PreferredDoctorId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Rooms_ProcedureSchedulingPreference_Preference_Prefe~",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestNotifications_Request_RequestId",
                table: "RequestNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestNotifications_UserAccount_UserId",
                table: "RequestNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Rooms_AssignedExamRoomId",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employees_DoctorId",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Employees_EmployeeId",
                table: "UserAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Patients_PatientId",
                table: "UserAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFeedbacks_UserAccount_UserId",
                table: "UserFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_PatientId",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_DoctorId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Request_DoctorId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_PatientId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_ScheduleProcedure_PatientId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_Preference_PreferredDoctorId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_DiagnosisId",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_DoctorId",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_PatientId",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_Procedure_Surgery_DiagnosisId",
                table: "Procedure");

            migrationBuilder.DropIndex(
                name: "IX_PatientSurveyResponses_BestDoctorId",
                table: "PatientSurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPrescriptions_DiagnosisId",
                table: "MedicationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPrescriptions_PatientId",
                table: "MedicationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPrescriptions_PrescribedById",
                table: "MedicationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_DiagnosisId",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_PatientId",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_FamilyMemberDiagnosis_DiagnosisId",
                table: "FamilyMemberDiagnosis");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisDetails_DiagnosisId",
                table: "DiagnosisDetails");

            migrationBuilder.DropIndex(
                name: "IX_BlogAuthors_DoctorId",
                table: "BlogAuthors");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ScheduleProcedure_PatientId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Preference_PreferredDoctorId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "Surgery_DiagnosisId",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "BestDoctorId",
                table: "PatientSurveyResponses");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "MedicationPrescriptions");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "MedicationPrescriptions");

            migrationBuilder.DropColumn(
                name: "PrescribedById",
                table: "MedicationPrescriptions");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "FamilyMemberDiagnosis");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "DiagnosisDetails");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "BlogAuthors");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "UserAccount",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_UserAccount_EmployeeId",
                table: "UserAccount",
                newName: "IX_UserAccount_EmployeeID");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserFeedbacks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PatientMedicalRecordID",
                table: "UserAccount",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssignedExamRoomId",
                table: "Shifts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DoctorEmployeeID",
                table: "Shifts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RequestNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "RequestNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Request",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReviewerId",
                table: "Request",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DoctorEmployeeID",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientMedicalRecordID",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleProcedure_PatientMedicalRecordID",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Preference_PreferredDoctorEmployeeID",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Renovations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProcedureNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureId",
                table: "ProcedureNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Procedure",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReferredFromId",
                table: "Procedure",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureTypeId",
                table: "Procedure",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisIcd",
                table: "Procedure",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorEmployeeID",
                table: "Procedure",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientMedicalRecordID",
                table: "Procedure",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surgery_DiagnosisIcd",
                table: "Procedure",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientSurveyResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BestDoctorEmployeeID",
                table: "PatientSurveyResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityOfResidenceId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CityOfBirthId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MedicationId",
                table: "MedicationStorageRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisIcd",
                table: "MedicationPrescriptions",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientMedicalRecordID",
                table: "MedicationPrescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrescribedByEmployeeID",
                table: "MedicationPrescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MedicationPrescriptionNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "MedicationPrescriptionNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisIcd",
                table: "Hospitalizations",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientMedicalRecordID",
                table: "Hospitalizations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "HospitalizationNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HospitalizationId",
                table: "HospitalizationNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisIcd",
                table: "FamilyMemberDiagnosis",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityOfResidenceId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisIcd",
                table: "DiagnosisDetails",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsumableId",
                table: "ConsumableStorageRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "BlogPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DoctorEmployeeID",
                table: "BlogAuthors",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AllergyId",
                table: "AllergyManifestation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_PatientMedicalRecordID",
                table: "UserAccount",
                column: "PatientMedicalRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DoctorEmployeeID",
                table: "Shifts",
                column: "DoctorEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_DoctorEmployeeID",
                table: "Request",
                column: "DoctorEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_PatientMedicalRecordID",
                table: "Request",
                column: "PatientMedicalRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ScheduleProcedure_PatientMedicalRecordID",
                table: "Request",
                column: "ScheduleProcedure_PatientMedicalRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Preference_PreferredDoctorEmployeeID",
                table: "Request",
                column: "Preference_PreferredDoctorEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_DiagnosisIcd",
                table: "Procedure",
                column: "DiagnosisIcd");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_DoctorEmployeeID",
                table: "Procedure",
                column: "DoctorEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_PatientMedicalRecordID",
                table: "Procedure",
                column: "PatientMedicalRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_Surgery_DiagnosisIcd",
                table: "Procedure",
                column: "Surgery_DiagnosisIcd");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSurveyResponses_BestDoctorEmployeeID",
                table: "PatientSurveyResponses",
                column: "BestDoctorEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_DiagnosisIcd",
                table: "MedicationPrescriptions",
                column: "DiagnosisIcd");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_PatientMedicalRecordID",
                table: "MedicationPrescriptions",
                column: "PatientMedicalRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_PrescribedByEmployeeID",
                table: "MedicationPrescriptions",
                column: "PrescribedByEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_DiagnosisIcd",
                table: "Hospitalizations",
                column: "DiagnosisIcd");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_PatientMedicalRecordID",
                table: "Hospitalizations",
                column: "PatientMedicalRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMemberDiagnosis_DiagnosisIcd",
                table: "FamilyMemberDiagnosis",
                column: "DiagnosisIcd");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisDetails_DiagnosisIcd",
                table: "DiagnosisDetails",
                column: "DiagnosisIcd");

            migrationBuilder.CreateIndex(
                name: "IX_BlogAuthors_DoctorEmployeeID",
                table: "BlogAuthors",
                column: "DoctorEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergyManifestation_Allergies_AllergyId",
                table: "AllergyManifestation",
                column: "AllergyId",
                principalTable: "Allergies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthors_Employees_DoctorEmployeeID",
                table: "BlogAuthors",
                column: "DoctorEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogAuthors_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "BlogAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableStorageRecords_MedicalConsumables_ConsumableId",
                table: "ConsumableStorageRecords",
                column: "ConsumableId",
                principalTable: "MedicalConsumables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisDetails_Diagnoses_DiagnosisIcd",
                table: "DiagnosisDetails",
                column: "DiagnosisIcd",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Cities_CityOfResidenceId",
                table: "Employees",
                column: "CityOfResidenceId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMemberDiagnosis_Diagnoses_DiagnosisIcd",
                table: "FamilyMemberDiagnosis",
                column: "DiagnosisIcd",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalizationNotifications_Hospitalizations_Hospitalizatio~",
                table: "HospitalizationNotifications",
                column: "HospitalizationId",
                principalTable: "Hospitalizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalizationNotifications_UserAccount_UserId",
                table: "HospitalizationNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Diagnoses_DiagnosisIcd",
                table: "Hospitalizations",
                column: "DiagnosisIcd",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Patients_PatientMedicalRecordID",
                table: "Hospitalizations",
                column: "PatientMedicalRecordID",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptionNotifications_MedicationPrescriptions_~",
                table: "MedicationPrescriptionNotifications",
                column: "PrescriptionId",
                principalTable: "MedicationPrescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptionNotifications_UserAccount_UserId",
                table: "MedicationPrescriptionNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Diagnoses_DiagnosisIcd",
                table: "MedicationPrescriptions",
                column: "DiagnosisIcd",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Patients_PatientMedicalRecordID",
                table: "MedicationPrescriptions",
                column: "PatientMedicalRecordID",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Employees_PrescribedByEmployeeID",
                table: "MedicationPrescriptions",
                column: "PrescribedByEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationStorageRecords_Medications_MedicationId",
                table: "MedicationStorageRecords",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Cities_CityOfBirthId",
                table: "Patients",
                column: "CityOfBirthId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Cities_CityOfResidenceId",
                table: "Patients",
                column: "CityOfResidenceId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSurveyResponses_Employees_BestDoctorEmployeeID",
                table: "PatientSurveyResponses",
                column: "BestDoctorEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSurveyResponses_UserAccount_PatientId",
                table: "PatientSurveyResponses",
                column: "PatientId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Diagnoses_DiagnosisIcd",
                table: "Procedure",
                column: "DiagnosisIcd",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Employees_DoctorEmployeeID",
                table: "Procedure",
                column: "DoctorEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Patients_PatientMedicalRecordID",
                table: "Procedure",
                column: "PatientMedicalRecordID",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_ProcedureTypes_ProcedureTypeId",
                table: "Procedure",
                column: "ProcedureTypeId",
                principalTable: "ProcedureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Procedure_ReferredFromId",
                table: "Procedure",
                column: "ReferredFromId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Rooms_RoomId",
                table: "Procedure",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedure_Diagnoses_Surgery_DiagnosisIcd",
                table: "Procedure",
                column: "Surgery_DiagnosisIcd",
                principalTable: "Diagnoses",
                principalColumn: "Icd",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureNotifications_Procedure_ProcedureId",
                table: "ProcedureNotifications",
                column: "ProcedureId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureNotifications_UserAccount_UserId",
                table: "ProcedureNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renovations_Rooms_RoomId",
                table: "Renovations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Employees_DoctorEmployeeID",
                table: "Request",
                column: "DoctorEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Rooms_RoomId",
                table: "Request",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Medications_MedicationId",
                table: "Request",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_UserAccount_ReviewerId",
                table: "Request",
                column: "ReviewerId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_UserAccount_SenderId",
                table: "Request",
                column: "SenderId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Patients_PatientMedicalRecordID",
                table: "Request",
                column: "PatientMedicalRecordID",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Patients_ScheduleProcedure_PatientMedicalRecordID",
                table: "Request",
                column: "ScheduleProcedure_PatientMedicalRecordID",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Rooms_Preference_PreferredRoomId",
                table: "Request",
                column: "Preference_PreferredRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Employees_Preference_PreferredDoctorEmployeeID",
                table: "Request",
                column: "Preference_PreferredDoctorEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Rooms_ProcedureSchedulingPreference_Preference_Prefe~",
                table: "Request",
                column: "ProcedureSchedulingPreference_Preference_PreferredRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestNotifications_Request_RequestId",
                table: "RequestNotifications",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestNotifications_UserAccount_UserId",
                table: "RequestNotifications",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Rooms_AssignedExamRoomId",
                table: "Shifts",
                column: "AssignedExamRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employees_DoctorEmployeeID",
                table: "Shifts",
                column: "DoctorEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Employees_EmployeeID",
                table: "UserAccount",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Patients_PatientMedicalRecordID",
                table: "UserAccount",
                column: "PatientMedicalRecordID",
                principalTable: "Patients",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFeedbacks_UserAccount_UserId",
                table: "UserFeedbacks",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
