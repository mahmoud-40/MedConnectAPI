using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class InitRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "590fc0b2-ecda-40ff-8537-f790d227209f", null, "Provider", "PROVIDER" },
                    { "6dbe495f-26b5-486e-8142-90cdc17f9a82", null, "Admin", "ADMIN" },
                    { "ed4dea11-f969-4524-a207-c1a14a488c8f", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "590fc0b2-ecda-40ff-8537-f790d227209f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dbe495f-26b5-486e-8142-90cdc17f9a82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed4dea11-f969-4524-a207-c1a14a488c8f");
        }
    }
}
