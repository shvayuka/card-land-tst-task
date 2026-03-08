using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardLand.Migrations
{
    /// <inheritdoc />
    public partial class Added_indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Offices_AddressCity",
                table: "Offices",
                column: "AddressCity");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CountryCode",
                table: "Offices",
                column: "CountryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offices_AddressCity",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_CountryCode",
                table: "Offices");
        }
    }
}
