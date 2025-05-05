using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Mangment_System.Migrations
{
    /// <inheritdoc />
    public partial class EditConsultationForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationForms_Patients_PatientId1",
                table: "ConsultationForms");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ConsultationForms",
                newName: "Symptoms");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId1",
                table: "ConsultationForms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "ConsultationForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "ConsultationForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationForms_Patients_PatientId1",
                table: "ConsultationForms",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationForms_Patients_PatientId1",
                table: "ConsultationForms");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "ConsultationForms");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "ConsultationForms");

            migrationBuilder.RenameColumn(
                name: "Symptoms",
                table: "ConsultationForms",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId1",
                table: "ConsultationForms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationForms_Patients_PatientId1",
                table: "ConsultationForms",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
