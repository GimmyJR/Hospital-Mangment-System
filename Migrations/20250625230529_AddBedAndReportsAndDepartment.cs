using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Mangment_System.Migrations
{
    /// <inheritdoc />
    public partial class AddBedAndReportsAndDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reports_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "beds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_beds_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_beds_DepartmentId",
                table: "beds",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_PatientId",
                table: "reports",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_departments_DepartmentId",
                table: "Doctors",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_departments_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "beds");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Doctors");
        }
    }
}
