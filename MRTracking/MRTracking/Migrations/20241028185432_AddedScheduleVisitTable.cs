using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddedScheduleVisitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRepresentativeVisit_Doctor_DoctorId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVisitDate",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastVisitId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalStoreId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "VisitUserType",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitedToId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ScheduleVisit",
                schema: "MRTracking",
                columns: table => new
                {
                    ScheduleVisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalRepresentativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MedicalStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MRGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitUserType = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastVisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleVisit", x => x.ScheduleVisitId);
                    table.ForeignKey(
                        name: "FK_ScheduleVisit_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "MRTracking",
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleVisit_MRGroups_MRGroupId",
                        column: x => x.MRGroupId,
                        principalTable: "MRGroups",
                        principalColumn: "MRGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleVisit_MedicalRepresentativeVisit_LastVisitId",
                        column: x => x.LastVisitId,
                        principalSchema: "MRTracking",
                        principalTable: "MedicalRepresentativeVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleVisit_MedicalRepresentativeVisit_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "MRTracking",
                        principalTable: "MedicalRepresentativeVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleVisit_MedicalRepresentative_MedicalRepresentativeId",
                        column: x => x.MedicalRepresentativeId,
                        principalSchema: "MRTracking",
                        principalTable: "MedicalRepresentative",
                        principalColumn: "MedicalRepresentativeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleVisit_MedicalStore_MedicalStoreId",
                        column: x => x.MedicalStoreId,
                        principalSchema: "MRTracking",
                        principalTable: "MedicalStore",
                        principalColumn: "MedicalStoreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRepresentativeVisit_MedicalStoreId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "MedicalStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRepresentativeVisit_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "MRGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleVisit_DoctorId",
                schema: "MRTracking",
                table: "ScheduleVisit",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleVisit_LastVisitId",
                schema: "MRTracking",
                table: "ScheduleVisit",
                column: "LastVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleVisit_MedicalRepresentativeId",
                schema: "MRTracking",
                table: "ScheduleVisit",
                column: "MedicalRepresentativeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleVisit_MedicalStoreId",
                schema: "MRTracking",
                table: "ScheduleVisit",
                column: "MedicalStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleVisit_MRGroupId",
                schema: "MRTracking",
                table: "ScheduleVisit",
                column: "MRGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleVisit_VisitId",
                schema: "MRTracking",
                table: "ScheduleVisit",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRepresentativeVisit_Doctor_DoctorId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "DoctorId",
                principalSchema: "MRTracking",
                principalTable: "Doctor",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRepresentativeVisit_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "MRGroupId",
                principalTable: "MRGroups",
                principalColumn: "MRGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRepresentativeVisit_MedicalStore_MedicalStoreId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "MedicalStoreId",
                principalSchema: "MRTracking",
                principalTable: "MedicalStore",
                principalColumn: "MedicalStoreId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRepresentativeVisit_Doctor_DoctorId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRepresentativeVisit_MRGroups_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRepresentativeVisit_MedicalStore_MedicalStoreId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropTable(
                name: "ScheduleVisit",
                schema: "MRTracking");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRepresentativeVisit_MedicalStoreId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRepresentativeVisit_MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropColumn(
                name: "LastVisitDate",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropColumn(
                name: "LastVisitId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropColumn(
                name: "MRGroupId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropColumn(
                name: "MedicalStoreId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropColumn(
                name: "VisitUserType",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.DropColumn(
                name: "VisitedToId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRepresentativeVisit_Doctor_DoctorId",
                schema: "MRTracking",
                table: "MedicalRepresentativeVisit",
                column: "DoctorId",
                principalSchema: "MRTracking",
                principalTable: "Doctor",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
