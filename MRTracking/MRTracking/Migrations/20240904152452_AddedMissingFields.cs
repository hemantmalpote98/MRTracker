using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddedMissingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GraduationYear",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "MRTracking",
                table: "MedicalRepresentative");

            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.AlterColumn<int>(
                name: "GraduationYear",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                schema: "MRTracking",
                table: "MedicalRepresentative",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
