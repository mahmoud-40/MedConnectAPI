using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "2454825d-aee0-4761-ad75-e3aae86f8a5b", null, "Patient", "PATIENT" },
                    { "8f04038f-b5a6-4a9d-91a4-e0c0612656b4", null, "Admin", "ADMIN" },
                    { "c1aaa439-7b17-4d07-bbf3-28ae3f45412c", null, "Provider", "PROVIDER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "34b900ff-fba2-40cb-aab6-c0de934d246d", 0, "34cbdf1d-5d02-468a-a17f-f89dbf2233a9", new DateTime(2024, 12, 2, 13, 16, 53, 155, DateTimeKind.Utc).AddTicks(5701), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEIUakITRg+OVSutEkozr5Bk4Ti//knbrwBvyjr2lsTV1LSFsWxafiXks6CgTnB/vXA==", null, false, "5b1ed5d2-6c76-40da-8ffc-f95fb26d8338", false, "admin" },
                    { "ecfc0538-bfb3-48d6-983c-a96581fd94df", 0, "510d8540-39d5-4aa3-9ef7-369ba5411ace", new DateTime(2024, 12, 2, 13, 16, 53, 292, DateTimeKind.Utc).AddTicks(8444), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAECq1YRu+1ipbLTYC4jKuatO/B3D3Rieek58zxCGKrjrsOEeq5RO1lfWOUpYzMb99Cg==", null, false, "6712ae7e-e431-4171-a619-ab367048901a", false, "Yousef" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8f04038f-b5a6-4a9d-91a4-e0c0612656b4", "34b900ff-fba2-40cb-aab6-c0de934d246d" },
                    { "8f04038f-b5a6-4a9d-91a4-e0c0612656b4", "ecfc0538-bfb3-48d6-983c-a96581fd94df" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2454825d-aee0-4761-ad75-e3aae86f8a5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1aaa439-7b17-4d07-bbf3-28ae3f45412c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8f04038f-b5a6-4a9d-91a4-e0c0612656b4", "34b900ff-fba2-40cb-aab6-c0de934d246d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8f04038f-b5a6-4a9d-91a4-e0c0612656b4", "ecfc0538-bfb3-48d6-983c-a96581fd94df" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f04038f-b5a6-4a9d-91a4-e0c0612656b4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34b900ff-fba2-40cb-aab6-c0de934d246d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ecfc0538-bfb3-48d6-983c-a96581fd94df");

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
    }
}
