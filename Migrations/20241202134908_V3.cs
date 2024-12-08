using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "0", null, "Admin", "ADMIN" },
                    { "1", null, "Provider", "PROVIDER" },
                    { "2", null, "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1d98450b-1542-4e98-9060-a19dd9f00262", 0, "29ed36fd-1963-4ac0-9604-1e2453d1d156", new DateTime(2024, 12, 2, 13, 49, 8, 512, DateTimeKind.Utc).AddTicks(8305), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAELN4LlPDL6c82/x0Whi86+/SJgaLsyV6UkkpBZdCkQj7bHxU7btCFyGel6P3e8lYbg==", null, false, "b30d38e1-4d27-48c9-a9ee-b622bd6bea20", false, "Yousef" },
                    { "3785fb8c-863a-4e0f-8740-35c04ad79909", 0, "986f0df1-2048-440c-ac56-cc85ddeadbc1", new DateTime(2024, 12, 2, 13, 49, 8, 462, DateTimeKind.Utc).AddTicks(8447), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEIgEjoF6uW6bK1VZvALyr+EH/FdLTzHQRWs5U5C2PE+6I48000CBEC+fg2FRxFuLDA==", null, false, "42158d89-9ad9-4f1c-bec2-c2ff27bcfaf3", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0", "1d98450b-1542-4e98-9060-a19dd9f00262" },
                    { "0", "3785fb8c-863a-4e0f-8740-35c04ad79909" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "1d98450b-1542-4e98-9060-a19dd9f00262" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "3785fb8c-863a-4e0f-8740-35c04ad79909" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d98450b-1542-4e98-9060-a19dd9f00262");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3785fb8c-863a-4e0f-8740-35c04ad79909");

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
    }
}
