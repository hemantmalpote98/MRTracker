using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MRTracking");

            migrationBuilder.CreateTable(
                name: "Doctor",
                schema: "MRTracking",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MedicalLicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HospitalAffiliation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HighestDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicalSchool = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GraduationYear = table.Column<int>(type: "int", nullable: false),
                    AvailabilityForConsultation = table.Column<bool>(type: "bit", nullable: false),
                    OfficeHours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRepresentative",
                schema: "MRTracking",
                columns: table => new
                {
                    MedicalRepresentativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationAssigned = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReportingManager = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HighestDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GraduationYear = table.Column<int>(type: "int", nullable: false),
                    AvailabilityForTravel = table.Column<bool>(type: "bit", nullable: false),
                    DriversLicense = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRepresentative", x => x.MedicalRepresentativeId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRepresentativeVisit",
                schema: "MRTracking",
                columns: table => new
                {
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalRepresentativeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FollowUpRequired = table.Column<bool>(type: "bit", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRepresentativeVisit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_MedicalRepresentativeVisit_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "MRTracking",
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalRepresentativeVisit_MedicalRepresentative_MedicalRepresentativeId",
                        column: x => x.MedicalRepresentativeId,
                        principalSchema: "MRTracking",
                        principalTable: "MedicalRepresentative",
                        principalColumn: "MedicalRepresentativeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRepresentativeVisit_DoctorId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRepresentativeVisit_MedicalRepresentativeId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "MedicalRepresentativeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalRepresentativeVisit",
                schema: "MRTracking");

            migrationBuilder.DropTable(
                name: "Doctor",
                schema: "MRTracking");

            migrationBuilder.DropTable(
                name: "MedicalRepresentative",
                schema: "MRTracking");
        }
    }
}
