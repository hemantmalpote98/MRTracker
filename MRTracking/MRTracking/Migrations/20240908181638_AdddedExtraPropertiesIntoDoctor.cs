using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class AdddedExtraPropertiesIntoDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "MRTracking",
                table: "Doctor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "MRTracking",
                table: "Doctor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "MRTracking",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "MRTracking",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                schema: "MRTracking",
                table: "Doctor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "MRTracking",
                table: "Doctor",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MRTracking",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "MRTracking",
                table: "Doctor");
        }
    }
}
