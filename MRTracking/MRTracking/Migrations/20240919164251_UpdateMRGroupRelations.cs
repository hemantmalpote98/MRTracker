using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMRGroupRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                schema: "MRTracking",
                table: "MedicalStore",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "MRTracking",
                table: "MedicalStore",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "MedicalStore",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Specialty",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MedicalSchool",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalAffiliation",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "HighestDegree",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "GraduationYear",
                schema: "MRTracking",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FieldOfStudy",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfJoining",
                schema: "MRTracking",
                table: "Doctor",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "AvailabilityForConsultation",
                schema: "MRTracking",
                table: "Doctor",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "Doctor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MRGroups",
                columns: table => new
                {
                    MRGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRGroups", x => x.MRGroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStore_MRGroupId",
                schema: "MRTracking",
                table: "MedicalStore",
                column: "MRGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRepresentative_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                column: "MRGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_MRGroupId",
                schema: "MRTracking",
                table: "Doctor",
                column: "MRGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "Doctor",
                column: "MRGroupId",
                principalTable: "MRGroups",
                principalColumn: "MRGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRepresentative_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                column: "MRGroupId",
                principalTable: "MRGroups",
                principalColumn: "MRGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStore_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "MedicalStore",
                column: "MRGroupId",
                principalTable: "MRGroups",
                principalColumn: "MRGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRepresentative_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentative");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStore_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "MedicalStore");

            migrationBuilder.DropTable(
                name: "MRGroups");

            migrationBuilder.DropIndex(
                name: "IX_MedicalStore_MRGroupId",
                schema: "MRTracking",
                table: "MedicalStore");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRepresentative_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentative");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_MRGroupId",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                schema: "MRTracking",
                table: "MedicalStore");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "MRTracking",
                table: "MedicalStore");

            migrationBuilder.DropColumn(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "MedicalStore");

            migrationBuilder.DropColumn(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentative");

            migrationBuilder.DropColumn(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "Specialty",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicalSchool",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalAffiliation",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HighestDegree",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GraduationYear",
                schema: "MRTracking",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FieldOfStudy",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfJoining",
                schema: "MRTracking",
                table: "Doctor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AvailabilityForConsultation",
                schema: "MRTracking",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
