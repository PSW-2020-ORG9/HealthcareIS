using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareBase.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Allergen = table.Column<string>(nullable: true),
                    Prevention = table.Column<string>(nullable: true),
                    Symptoms = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Icd = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsInjury = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Icd);
                });

            migrationBuilder.CreateTable(
                name: "FamilyHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Overview = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalizationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsualNumberOfDays = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalizationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalConsumableTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConsumableTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    MedicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Overview = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcedureTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    SchedulableByPatient = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMemberDiagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FamilyRelation = table.Column<string>(nullable: true),
                    DiscoveredAtAge = table.Column<int>(nullable: false),
                    Lethal = table.Column<bool>(nullable: false),
                    DiagnosisId = table.Column<string>(nullable: true),
                    FamilyHistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMemberDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMemberDiagnoses_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Icd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyMemberDiagnoses_FamilyHistories_FamilyHistoryId",
                        column: x => x.FamilyHistoryId,
                        principalTable: "FamilyHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HospitalizationTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_HospitalizationType_HospitalizationTypeId",
                        column: x => x.HospitalizationTypeId,
                        principalTable: "HospitalizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalConsumables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Manufacutrer = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ConsumableTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConsumables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalConsumables_MedicalConsumableTypes_ConsumableTypeId",
                        column: x => x.ConsumableTypeId,
                        principalTable: "MedicalConsumableTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Amount",
                columns: table => new
                {
                    MedicationId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    Ingredients_Name = table.Column<string>(nullable: true),
                    Ingredients_IsAllergen = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amount", x => new { x.MedicationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Amount_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    MedicationId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    SideEffects_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => new { x.MedicationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Frequency_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationStorageRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvailableAmount = table.Column<int>(nullable: false),
                    MedicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationStorageRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationStorageRecords_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DiscoveredAtAge = table.Column<int>(nullable: false),
                    Type = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    DiagnosisId = table.Column<string>(nullable: true),
                    PersonalHistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosisDetails_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Icd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosisDetails_PersonalHistories_PersonalHistoryId",
                        column: x => x.PersonalHistoryId,
                        principalTable: "PersonalHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonalHistoryId = table.Column<int>(nullable: false),
                    FamilyHistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_FamilyHistories_FamilyHistoryId",
                        column: x => x.FamilyHistoryId,
                        principalTable: "FamilyHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_PersonalHistories_PersonalHistoryId",
                        column: x => x.PersonalHistoryId,
                        principalTable: "PersonalHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    RequiresRenovationToMove = table.Column<bool>(nullable: false),
                    HospitalizationTypeId = table.Column<int>(nullable: true),
                    ProcedureTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTypes_HospitalizationType_HospitalizationTypeId",
                        column: x => x.HospitalizationTypeId,
                        principalTable: "HospitalizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentTypes_ProcedureTypes_ProcedureTypeId",
                        column: x => x.ProcedureTypeId,
                        principalTable: "ProcedureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Jmbg = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    MartialStatus = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    CityOfResidenceId = table.Column<int>(nullable: false),
                    CityOfBirthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Jmbg);
                    table.ForeignKey(
                        name: "FK_Persons_Cities_CityOfBirthId",
                        column: x => x.CityOfBirthId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Cities_CityOfResidenceId",
                        column: x => x.CityOfResidenceId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumableStorageRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvailableAmount = table.Column<int>(nullable: false),
                    ConsumableId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableStorageRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumableStorageRecords_MedicalConsumables_ConsumableId",
                        column: x => x.ConsumableId,
                        principalTable: "MedicalConsumables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationStorageRecords_SupplyHistory",
                columns: table => new
                {
                    MedicationStorageRecordId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationStorageRecords_SupplyHistory", x => new { x.MedicationStorageRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_MedicationStorageRecords_SupplyHistory_MedicationStorageReco~",
                        column: x => x.MedicationStorageRecordId,
                        principalTable: "MedicationStorageRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationStorageRecords_UsageHistory",
                columns: table => new
                {
                    MedicationStorageRecordId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationStorageRecords_UsageHistory", x => new { x.MedicationStorageRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_MedicationStorageRecords_UsageHistory_MedicationStorageRecor~",
                        column: x => x.MedicationStorageRecordId,
                        principalTable: "MedicationStorageRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllergyManifestations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Intensity = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    AllergyId = table.Column<int>(nullable: false),
                    MedicalHistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyManifestations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllergyManifestations_Allergies_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergyManifestations_MedicalHistories_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citizenships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryID = table.Column<int>(nullable: false),
                    PersonJmbg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizenships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citizenships_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citizenships_Persons_PersonJmbg",
                        column: x => x.PersonJmbg,
                        principalTable: "Persons",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonJmbg = table.Column<string>(nullable: true),
                    Status = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Persons_PersonJmbg",
                        column: x => x.PersonJmbg,
                        principalTable: "Persons",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    MedicalRecordID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InsuranceNumber = table.Column<string>(nullable: true),
                    PersonJmbg = table.Column<string>(nullable: true),
                    Status = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    MedicalHistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.MedicalRecordID);
                    table.ForeignKey(
                        name: "FK_Patients_MedicalHistories_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Persons_PersonJmbg",
                        column: x => x.PersonJmbg,
                        principalTable: "Persons",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Renovations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    TimeInterval_Start = table.Column<DateTime>(nullable: true),
                    TimeInterval_End = table.Column<DateTime>(nullable: true),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renovations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renovations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumableStorageRecords_SupplyHistory",
                columns: table => new
                {
                    ConsumableStorageRecordId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableStorageRecords_SupplyHistory", x => new { x.ConsumableStorageRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_ConsumableStorageRecords_SupplyHistory_ConsumableStorageReco~",
                        column: x => x.ConsumableStorageRecordId,
                        principalTable: "ConsumableStorageRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumableStorageRecords_UsageHistory",
                columns: table => new
                {
                    ConsumableStorageRecordId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableStorageRecords_UsageHistory", x => new { x.ConsumableStorageRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_ConsumableStorageRecords_UsageHistory_ConsumableStorageRecor~",
                        column: x => x.ConsumableStorageRecordId,
                        principalTable: "ConsumableStorageRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAuthors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogAuthors_Employees_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeInterval_Start = table.Column<DateTime>(nullable: true),
                    TimeInterval_End = table.Column<DateTime>(nullable: true),
                    AssignedExamRoomId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Rooms_AssignedExamRoomId",
                        column: x => x.AssignedExamRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shifts_Employees_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hospitalizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DiagnosisId = table.Column<string>(nullable: true),
                    CauseOfAdmission = table.Column<string>(nullable: true),
                    DischargeType = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    RoomId = table.Column<int>(nullable: true),
                    HospitalizationTypeId = table.Column<int>(nullable: true),
                    TimeInterval_Start = table.Column<DateTime>(nullable: true),
                    TimeInterval_End = table.Column<DateTime>(nullable: true),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitalizations_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Icd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hospitalizations_HospitalizationType_HospitalizationTypeId",
                        column: x => x.HospitalizationTypeId,
                        principalTable: "HospitalizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hospitalizations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospitalizations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Procedure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeInterval_Start = table.Column<DateTime>(nullable: true),
                    TimeInterval_End = table.Column<DateTime>(nullable: true),
                    DoctorId = table.Column<int>(nullable: false),
                    ProcedureTypeId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    ReferredFromId = table.Column<int>(nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    AvoidChangingTime = table.Column<bool>(nullable: false),
                    AvoidChangingRoom = table.Column<bool>(nullable: false),
                    AvoidChangingDoctor = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DiagnosisId = table.Column<string>(nullable: true),
                    Anamnesis = table.Column<string>(nullable: true),
                    Surgery_DiagnosisId = table.Column<string>(nullable: true),
                    CauseOfSurgery = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Icd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Procedure_Employees_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_ProcedureTypes_ProcedureTypeId",
                        column: x => x.ProcedureTypeId,
                        principalTable: "ProcedureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_Procedure_ReferredFromId",
                        column: x => x.ReferredFromId,
                        principalTable: "Procedure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_Diagnoses_Surgery_DiagnosisId",
                        column: x => x.Surgery_DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Icd",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    EmployeeType = table.Column<string>(type: "nvarchar(24)", nullable: true),
                    PatientId = table.Column<int>(nullable: true),
                    RespondedToSurvey = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccount_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccount_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_BlogAuthors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "BlogAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AcquisitionDate = table.Column<DateTime>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    CurrentLocationId = table.Column<int>(nullable: true),
                    EquipmentTypeId = table.Column<int>(nullable: true),
                    RoomId = table.Column<int>(nullable: true),
                    HospitalizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentUnits_Rooms_CurrentLocationId",
                        column: x => x.CurrentLocationId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentUnits_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentUnits_Hospitalizations_HospitalizationId",
                        column: x => x.HospitalizationId,
                        principalTable: "Hospitalizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EquipmentUnits_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MedicationPrescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    DiagnosisId = table.Column<string>(nullable: true),
                    MedicationId = table.Column<int>(nullable: true),
                    Instructions_StartDate = table.Column<DateTime>(nullable: true),
                    Instructions_EndDate = table.Column<DateTime>(nullable: true),
                    Instructions_TimesPerDay = table.Column<int>(nullable: true),
                    Instructions_Dosage = table.Column<double>(nullable: true),
                    Instructions_DosageUnit = table.Column<string>(nullable: true),
                    Instructions_Description = table.Column<string>(nullable: true),
                    PrescribedById = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationPrescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationPrescriptions_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Icd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicationPrescriptions_Procedure_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Procedure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MedicationPrescriptions_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicationPrescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationPrescriptions_Employees_PrescribedById",
                        column: x => x.PrescribedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteDoctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteDoctors_Employees_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteDoctors_UserAccount_PatientAccountId",
                        column: x => x.PatientAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HospitalizationNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Read = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    HospitalizationId = table.Column<int>(nullable: false),
                    UpdateType = table.Column<string>(type: "nvarchar(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalizationNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalizationNotifications_Hospitalizations_Hospitalizatio~",
                        column: x => x.HospitalizationId,
                        principalTable: "Hospitalizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalizationNotifications_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientSurveyResponses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExperienceRating = table.Column<int>(nullable: false),
                    BestDoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSurveyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientSurveyResponses_Employees_BestDoctorId",
                        column: x => x.BestDoctorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientSurveyResponses_UserAccount_PatientId",
                        column: x => x.PatientId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcedureNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Read = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ProcedureId = table.Column<int>(nullable: false),
                    UpdateType = table.Column<string>(type: "nvarchar(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureNotifications_Procedure_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureNotifications_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ReviewDate = table.Column<DateTime>(nullable: false),
                    ReviewerComment = table.Column<string>(nullable: true),
                    SenderId = table.Column<int>(nullable: false),
                    ReviewerId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TimeInterval_Start = table.Column<DateTime>(nullable: true),
                    TimeInterval_End = table.Column<DateTime>(nullable: true),
                    DoctorId = table.Column<int>(nullable: true),
                    RoomId = table.Column<int>(nullable: true),
                    MedicationId = table.Column<int>(nullable: true),
                    PatientId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    Preference_PreferredRoomId = table.Column<int>(nullable: true),
                    Preference_PreferredAdmissionDate_Start = table.Column<DateTime>(nullable: true),
                    Preference_PreferredAdmissionDate_End = table.Column<DateTime>(nullable: true),
                    Preference_Duration = table.Column<int>(nullable: true),
                    ScheduleProcedure_PatientId = table.Column<int>(nullable: true),
                    ScheduleProcedure_TypeId = table.Column<int>(nullable: true),
                    Preference_PreferredDoctorId = table.Column<int>(nullable: true),
                    ProcedureSchedulingPreference_Preference_PreferredRoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Employees_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_UserAccount_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_UserAccount_SenderId",
                        column: x => x.SenderId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_HospitalizationType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "HospitalizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_Patients_ScheduleProcedure_PatientId",
                        column: x => x.ScheduleProcedure_PatientId,
                        principalTable: "Patients",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_ProcedureTypes_ScheduleProcedure_TypeId",
                        column: x => x.ScheduleProcedure_TypeId,
                        principalTable: "ProcedureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_Rooms_Preference_PreferredRoomId",
                        column: x => x.Preference_PreferredRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Employees_Preference_PreferredDoctorId",
                        column: x => x.Preference_PreferredDoctorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Rooms_ProcedureSchedulingPreference_Preference_Prefe~",
                        column: x => x.ProcedureSchedulingPreference_Preference_PreferredRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    UserComment = table.Column<string>(nullable: true),
                    isPublic = table.Column<bool>(nullable: false),
                    isAnonymous = table.Column<bool>(nullable: false),
                    isPublished = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFeedbacks_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationPrescriptionNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Read = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    PrescriptionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationPrescriptionNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationPrescriptionNotifications_MedicationPrescriptions_~",
                        column: x => x.PrescriptionId,
                        principalTable: "MedicationPrescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationPrescriptionNotifications_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request_Intervals",
                columns: table => new
                {
                    TimeIntervalCollectionProcedureSchedulingPreferenceScheduleProc = table.Column<int>(name: "TimeIntervalCollectionProcedureSchedulingPreferenceScheduleProc~", nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request_Intervals", x => new { x.TimeIntervalCollectionProcedureSchedulingPreferenceScheduleProc, x.Id });
                    table.ForeignKey(
                        name: "FK_Request_Intervals_Request_TimeIntervalCollectionProcedureSch~",
                        column: x => x.TimeIntervalCollectionProcedureSchedulingPreferenceScheduleProc,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Read = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    RequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestNotifications_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestNotifications_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DoctorEmployeeID = table.Column<int>(nullable: true),
                    MedicationInputRequestId = table.Column<int>(nullable: true),
                    ProcedureTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialties_Employees_DoctorEmployeeID",
                        column: x => x.DoctorEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Specialties_Request_MedicationInputRequestId",
                        column: x => x.MedicationInputRequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Specialties_ProcedureTypes_ProcedureTypeId",
                        column: x => x.ProcedureTypeId,
                        principalTable: "ProcedureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergyManifestations_AllergyId",
                table: "AllergyManifestations",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyManifestations_MedicalHistoryId",
                table: "AllergyManifestations",
                column: "MedicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogAuthors_DoctorId",
                table: "BlogAuthors",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Citizenships_CountryID",
                table: "Citizenships",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Citizenships_PersonJmbg",
                table: "Citizenships",
                column: "PersonJmbg");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableStorageRecords_ConsumableId",
                table: "ConsumableStorageRecords",
                column: "ConsumableId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HospitalizationTypeId",
                table: "Departments",
                column: "HospitalizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisDetails_DiagnosisId",
                table: "DiagnosisDetails",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisDetails_PersonalHistoryId",
                table: "DiagnosisDetails",
                column: "PersonalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonJmbg",
                table: "Employees",
                column: "PersonJmbg");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_HospitalizationTypeId",
                table: "EquipmentTypes",
                column: "HospitalizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_ProcedureTypeId",
                table: "EquipmentTypes",
                column: "ProcedureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentUnits_CurrentLocationId",
                table: "EquipmentUnits",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentUnits_EquipmentTypeId",
                table: "EquipmentUnits",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentUnits_HospitalizationId",
                table: "EquipmentUnits",
                column: "HospitalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentUnits_RoomId",
                table: "EquipmentUnits",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMemberDiagnoses_DiagnosisId",
                table: "FamilyMemberDiagnoses",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMemberDiagnoses_FamilyHistoryId",
                table: "FamilyMemberDiagnoses",
                column: "FamilyHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteDoctors_DoctorId",
                table: "FavoriteDoctors",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteDoctors_PatientAccountId",
                table: "FavoriteDoctors",
                column: "PatientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalizationNotifications_HospitalizationId",
                table: "HospitalizationNotifications",
                column: "HospitalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalizationNotifications_UserId",
                table: "HospitalizationNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_DiagnosisId",
                table: "Hospitalizations",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_HospitalizationTypeId",
                table: "Hospitalizations",
                column: "HospitalizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_PatientId",
                table: "Hospitalizations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_RoomId",
                table: "Hospitalizations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsumables_ConsumableTypeId",
                table: "MedicalConsumables",
                column: "ConsumableTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_FamilyHistoryId",
                table: "MedicalHistories",
                column: "FamilyHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PersonalHistoryId",
                table: "MedicalHistories",
                column: "PersonalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptionNotifications_PrescriptionId",
                table: "MedicationPrescriptionNotifications",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptionNotifications_UserId",
                table: "MedicationPrescriptionNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_DiagnosisId",
                table: "MedicationPrescriptions",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_ExaminationId",
                table: "MedicationPrescriptions",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_MedicationId",
                table: "MedicationPrescriptions",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_PatientId",
                table: "MedicationPrescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescriptions_PrescribedById",
                table: "MedicationPrescriptions",
                column: "PrescribedById");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_MedicationId",
                table: "Medications",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationStorageRecords_MedicationId",
                table: "MedicationStorageRecords",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalHistoryId",
                table: "Patients",
                column: "MedicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonJmbg",
                table: "Patients",
                column: "PersonJmbg");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSurveyResponses_BestDoctorId",
                table: "PatientSurveyResponses",
                column: "BestDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSurveyResponses_PatientId",
                table: "PatientSurveyResponses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityOfBirthId",
                table: "Persons",
                column: "CityOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityOfResidenceId",
                table: "Persons",
                column: "CityOfResidenceId");

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
                name: "IX_Procedure_ProcedureTypeId",
                table: "Procedure",
                column: "ProcedureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_ReferredFromId",
                table: "Procedure",
                column: "ReferredFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_RoomId",
                table: "Procedure",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_Surgery_DiagnosisId",
                table: "Procedure",
                column: "Surgery_DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureNotifications_ProcedureId",
                table: "ProcedureNotifications",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureNotifications_UserId",
                table: "ProcedureNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Renovations_RoomId",
                table: "Renovations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_DoctorId",
                table: "Request",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RoomId",
                table: "Request",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_MedicationId",
                table: "Request",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ReviewerId",
                table: "Request",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_SenderId",
                table: "Request",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_PatientId",
                table: "Request",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_TypeId",
                table: "Request",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ScheduleProcedure_PatientId",
                table: "Request",
                column: "ScheduleProcedure_PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ScheduleProcedure_TypeId",
                table: "Request",
                column: "ScheduleProcedure_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Preference_PreferredRoomId",
                table: "Request",
                column: "Preference_PreferredRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Preference_PreferredDoctorId",
                table: "Request",
                column: "Preference_PreferredDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ProcedureSchedulingPreference_Preference_PreferredRo~",
                table: "Request",
                column: "ProcedureSchedulingPreference_Preference_PreferredRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestNotifications_RequestId",
                table: "RequestNotifications",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestNotifications_UserId",
                table: "RequestNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DepartmentId",
                table: "Rooms",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_AssignedExamRoomId",
                table: "Shifts",
                column: "AssignedExamRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DoctorId",
                table: "Shifts",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_DoctorEmployeeID",
                table: "Specialties",
                column: "DoctorEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_MedicationInputRequestId",
                table: "Specialties",
                column: "MedicationInputRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_ProcedureTypeId",
                table: "Specialties",
                column: "ProcedureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_EmployeeId",
                table: "UserAccount",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_PatientId",
                table: "UserAccount",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_UserId",
                table: "UserFeedbacks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergyManifestations");

            migrationBuilder.DropTable(
                name: "Amount");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Citizenships");

            migrationBuilder.DropTable(
                name: "ConsumableStorageRecords_SupplyHistory");

            migrationBuilder.DropTable(
                name: "ConsumableStorageRecords_UsageHistory");

            migrationBuilder.DropTable(
                name: "DiagnosisDetails");

            migrationBuilder.DropTable(
                name: "EquipmentUnits");

            migrationBuilder.DropTable(
                name: "FamilyMemberDiagnoses");

            migrationBuilder.DropTable(
                name: "FavoriteDoctors");

            migrationBuilder.DropTable(
                name: "Frequency");

            migrationBuilder.DropTable(
                name: "HospitalizationNotifications");

            migrationBuilder.DropTable(
                name: "MedicationPrescriptionNotifications");

            migrationBuilder.DropTable(
                name: "MedicationStorageRecords_SupplyHistory");

            migrationBuilder.DropTable(
                name: "MedicationStorageRecords_UsageHistory");

            migrationBuilder.DropTable(
                name: "PatientSurveyResponses");

            migrationBuilder.DropTable(
                name: "ProcedureNotifications");

            migrationBuilder.DropTable(
                name: "Renovations");

            migrationBuilder.DropTable(
                name: "Request_Intervals");

            migrationBuilder.DropTable(
                name: "RequestNotifications");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "UserFeedbacks");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "BlogAuthors");

            migrationBuilder.DropTable(
                name: "ConsumableStorageRecords");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Hospitalizations");

            migrationBuilder.DropTable(
                name: "MedicationPrescriptions");

            migrationBuilder.DropTable(
                name: "MedicationStorageRecords");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "MedicalConsumables");

            migrationBuilder.DropTable(
                name: "Procedure");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "MedicalConsumableTypes");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "ProcedureTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "HospitalizationType");

            migrationBuilder.DropTable(
                name: "FamilyHistories");

            migrationBuilder.DropTable(
                name: "PersonalHistories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
