using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class AgeToBirthday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDay",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32b81f0e-6ef4-4c7f-98ee-04399b3f8486", null, "Provider", "PROVIDER" },
                    { "5d35bc62-7bf0-41a9-a14d-bb46f1198eab", null, "Patient", "PATIENT" },
                    { "f7264f42-b3c8-4d12-a551-480d86a95adf", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32b81f0e-6ef4-4c7f-98ee-04399b3f8486");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d35bc62-7bf0-41a9-a14d-bb46f1198eab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7264f42-b3c8-4d12-a551-480d86a95adf");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

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
    }
}
