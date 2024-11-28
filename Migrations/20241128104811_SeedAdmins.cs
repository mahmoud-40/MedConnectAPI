using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ffa8ef1-d979-4274-8269-4417dc463b74", null, "Admin", "ADMIN" },
                    { "5f8226d9-8846-41ac-9f09-ea7a352d9950", null, "Patient", "PATIENT" },
                    { "dee8cf8d-b4f2-4530-8088-c527698322ac", null, "Provider", "PROVIDER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "79b170b6-7ab2-4e9d-ba31-42ff04a0f99a", 0, "8861865e-b715-4858-ae2e-1d94d39c6cf0", new DateTime(2024, 11, 28, 10, 48, 10, 600, DateTimeKind.Utc).AddTicks(6242), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEPkceoy1KRZKiqGAHSdvuAooKaOH2vuulp/ka1kzf521X217lNM/N9PUspQJIDw3WQ==", null, false, "59f0bd1a-90dc-4fbb-9ddd-2c0f4c54f183", false, "Yousef" },
                    { "81f9940f-b9ea-42ce-9954-bfa74b1840e9", 0, "fc919954-826a-4c12-982f-0d81366b41a6", new DateTime(2024, 11, 28, 10, 48, 10, 556, DateTimeKind.Utc).AddTicks(4129), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEG6710iKI38N1N1iyCjgMBQuM7wyo5QkWQ54TJj6yXwRcebs9PqKqU3pQMKelt4JaA==", null, false, "fddf7d89-d0b1-421c-aed3-a7e607925504", false, "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ffa8ef1-d979-4274-8269-4417dc463b74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f8226d9-8846-41ac-9f09-ea7a352d9950");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dee8cf8d-b4f2-4530-8088-c527698322ac");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79b170b6-7ab2-4e9d-ba31-42ff04a0f99a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81f9940f-b9ea-42ce-9954-bfa74b1840e9");

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
    }
}
