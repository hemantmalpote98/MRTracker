using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MRTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "120bcbde-ff6e-49d5-913a-b488e2a8c860", "120bcbde-ff6e-49d5-913a-b488e2a8c860", "Admin", "Admin" },
                    { "2ab83b9d-b84f-4ec3-818e-e99e066ee000", "2ab83b9d-b84f-4ec3-818e-e99e066ee000", "Doctor", "Doctor" },
                    { "8b7fc89b-645a-44da-81d8-e2a7faf6af6a", "8b7fc89b-645a-44da-81d8-e2a7faf6af6a", "MedicalStore", "MedicalStore" },
                    { "91e08da7-5a91-4dd8-b809-1fa19007d587", "91e08da7-5a91-4dd8-b809-1fa19007d587", "MedicalRepresentative", "MedicalRepresentative" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");
        }
    }
}
