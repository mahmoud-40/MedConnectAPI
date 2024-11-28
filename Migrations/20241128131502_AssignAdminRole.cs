using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class AssignAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "032dee93-a51d-4cef-b557-8f90c84cff01", null, "Provider", "PROVIDER" },
                    { "952ff8eb-6fa8-4835-82fa-89125625741b", null, "Patient", "PATIENT" },
                    { "eff5d99a-2a59-4c4f-b2a1-f6160681c21e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5b771323-eaa8-477d-848b-117ff0f19855", 0, "40283979-2c41-4a55-b204-f500acedce4c", new DateTime(2024, 11, 28, 13, 15, 1, 941, DateTimeKind.Utc).AddTicks(7883), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEKDJ4MEpx4XluhAjVBFwbsmooO+G2VFKFzi7USOJBNd4SHhHiGGg4VNyQdopO2964g==", null, false, "f4815c39-a65c-47e0-a480-1ec94aceed87", false, "admin" },
                    { "b610bebc-c3e1-4a92-bfb0-30b32c4fb523", 0, "b6e80357-99da-4617-96cc-796b48f99a46", new DateTime(2024, 11, 28, 13, 15, 1, 986, DateTimeKind.Utc).AddTicks(9209), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEHnBzJC1G+5i3XZ7S15Tbf5jKyc3jes4RrkDdgTRQ/FZUr3OgZmaK67Q+pedwx7dEg==", null, false, "2759945b-2367-4363-a73c-c8b35428398a", false, "Yousef" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "eff5d99a-2a59-4c4f-b2a1-f6160681c21e", "5b771323-eaa8-477d-848b-117ff0f19855" },
                    { "eff5d99a-2a59-4c4f-b2a1-f6160681c21e", "b610bebc-c3e1-4a92-bfb0-30b32c4fb523" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "032dee93-a51d-4cef-b557-8f90c84cff01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "952ff8eb-6fa8-4835-82fa-89125625741b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eff5d99a-2a59-4c4f-b2a1-f6160681c21e", "5b771323-eaa8-477d-848b-117ff0f19855" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eff5d99a-2a59-4c4f-b2a1-f6160681c21e", "b610bebc-c3e1-4a92-bfb0-30b32c4fb523" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eff5d99a-2a59-4c4f-b2a1-f6160681c21e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b771323-eaa8-477d-848b-117ff0f19855");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b610bebc-c3e1-4a92-bfb0-30b32c4fb523");

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
    }
}
