using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareBase.Migrations
{
    public partial class OneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentUnits_Hospitalizations_HospitalizationId",
                table: "EquipmentUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Procedure_ExaminationId",
                table: "MedicationPrescriptions");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "EquipmentUnits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentUnits_RoomId",
                table: "EquipmentUnits",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentUnits_Hospitalizations_HospitalizationId",
                table: "EquipmentUnits",
                column: "HospitalizationId",
                principalTable: "Hospitalizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentUnits_Rooms_RoomId",
                table: "EquipmentUnits",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Procedure_ExaminationId",
                table: "MedicationPrescriptions",
                column: "ExaminationId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentUnits_Hospitalizations_HospitalizationId",
                table: "EquipmentUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentUnits_Rooms_RoomId",
                table: "EquipmentUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescriptions_Procedure_ExaminationId",
                table: "MedicationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentUnits_RoomId",
                table: "EquipmentUnits");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "EquipmentUnits");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentUnits_Hospitalizations_HospitalizationId",
                table: "EquipmentUnits",
                column: "HospitalizationId",
                principalTable: "Hospitalizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescriptions_Procedure_ExaminationId",
                table: "MedicationPrescriptions",
                column: "ExaminationId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
