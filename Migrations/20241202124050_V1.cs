using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "5f45f9a1-089d-42b5-8833-e381265968d9", null, "Provider", "PROVIDER" },
                    { "94ad8268-572c-4e5f-94a2-55b07872e50f", null, "Admin", "ADMIN" },
                    { "e1ea7d7c-8788-40b8-b4a7-85cb31bf192a", null, "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "902a2b66-677b-48b1-b774-6f8f2e09cbc8", 0, "6a0791cc-e14b-491a-bec9-4085c9392041", new DateTime(2024, 12, 2, 12, 40, 49, 97, DateTimeKind.Utc).AddTicks(2154), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEHLp5K6ELE8ypK4QWtiLhHI5SAIEf88CM4kJXEG1FJANlNK3sQOHsJ1xnLIQBoKpHA==", null, false, "086ac6d7-cbdd-437d-8793-3869eae58778", false, "admin" },
                    { "e6d256eb-1f59-48ef-8ca0-11e86eb26a00", 0, "e0f8a492-97e2-4c5b-a591-b948d4346644", new DateTime(2024, 12, 2, 12, 40, 49, 174, DateTimeKind.Utc).AddTicks(7792), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEFZcORqM6ogideu7tVbyv++8x8TBafEw6vu7q5m6tX5XJcEs5gn/0L0EPebVA8380A==", null, false, "fab9cb65-eec3-413a-9813-0deb6e90f0d7", false, "Yousef" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "94ad8268-572c-4e5f-94a2-55b07872e50f", "902a2b66-677b-48b1-b774-6f8f2e09cbc8" },
                    { "94ad8268-572c-4e5f-94a2-55b07872e50f", "e6d256eb-1f59-48ef-8ca0-11e86eb26a00" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f45f9a1-089d-42b5-8833-e381265968d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1ea7d7c-8788-40b8-b4a7-85cb31bf192a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "94ad8268-572c-4e5f-94a2-55b07872e50f", "902a2b66-677b-48b1-b774-6f8f2e09cbc8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "94ad8268-572c-4e5f-94a2-55b07872e50f", "e6d256eb-1f59-48ef-8ca0-11e86eb26a00" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94ad8268-572c-4e5f-94a2-55b07872e50f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "902a2b66-677b-48b1-b774-6f8f2e09cbc8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e6d256eb-1f59-48ef-8ca0-11e86eb26a00");

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
    }
}
