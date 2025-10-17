using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddTimePropertyForAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability1_EndTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability1_StartTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability2_EndTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability2_StartTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability3_EndTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability3_StartTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability4_EndTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Availability4_StartTime",
                schema: "MRTracking",
                table: "Doctor",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability1_EndTime",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability1_StartTime",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability2_EndTime",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability2_StartTime",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability3_EndTime",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability3_StartTime",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability4_EndTime",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Availability4_StartTime",
                schema: "MRTracking",
                table: "Doctor");
        }
    }
}
