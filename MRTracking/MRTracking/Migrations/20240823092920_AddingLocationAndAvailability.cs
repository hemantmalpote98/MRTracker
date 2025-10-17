using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddingLocationAndAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Availability1",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Availability2",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Availability3",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Availability4",
                schema: "MRTracking",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropColumn(
                name: "Availability1",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability2",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability3",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability4",
                schema: "MRTracking",
                table: "Doctor");
        }
    }
}
