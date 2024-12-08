using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "1d98450b-1542-4e98-9060-a19dd9f00262" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "3785fb8c-863a-4e0f-8740-35c04ad79909" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d98450b-1542-4e98-9060-a19dd9f00262");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3785fb8c-863a-4e0f-8740-35c04ad79909");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4b260c87-4581-4261-9202-dbf9a8582642", 0, "993adf5c-d081-43fe-abdf-fde93606ed97", new DateTime(2024, 12, 2, 14, 0, 56, 229, DateTimeKind.Utc).AddTicks(3736), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAECy247uQgANGgrg8d1h9smPx/twI4KYzM1yDfJw3Cfv+c3jvNfHXj6eHWjrKOMy1Qg==", null, false, "b12e9468-83af-429b-9cdb-3a33bf8ef52f", false, "Yousef" },
                    { "d3902317-d61c-49b5-88bd-61f869b8a031", 0, "dd22b2a6-fd97-47d9-8cb5-04b48a87b037", new DateTime(2024, 12, 2, 14, 0, 56, 302, DateTimeKind.Utc).AddTicks(5030), "Admin", "gamalelbatawy@gmail.com", true, false, null, "Gamal Moemen", "GAMALELBATAWY@GMAIL.COM", "GAMAL", "AQAAAAIAAYagAAAAEJiFiFu0tQ/czdBAPkLHB1NibE94MyauN+Pbj5BbCXfpPQUh4+0U4/yw66qfTdCuHQ==", null, false, "dbbcdfb2-ec23-47a2-99d3-dac5760cba1c", false, "Gamal" },
                    { "d4b9e94f-43b4-463d-a47a-6f8a223b7aea", 0, "6b2504bf-e20a-4492-adc1-585d08a4341c", new DateTime(2024, 12, 2, 14, 0, 56, 158, DateTimeKind.Utc).AddTicks(7350), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEJ15II/9gykM15hYLpRYDGO+yvAiZhOxdx80zreeunSwB3Yh9BaPUOfl/gpgyImprQ==", null, false, "a4c20e05-1dc9-4a9d-98a6-f10e827bf95b", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0", "4b260c87-4581-4261-9202-dbf9a8582642" },
                    { "0", "d3902317-d61c-49b5-88bd-61f869b8a031" },
                    { "0", "d4b9e94f-43b4-463d-a47a-6f8a223b7aea" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "4b260c87-4581-4261-9202-dbf9a8582642" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "d3902317-d61c-49b5-88bd-61f869b8a031" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "d4b9e94f-43b4-463d-a47a-6f8a223b7aea" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b260c87-4581-4261-9202-dbf9a8582642");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d3902317-d61c-49b5-88bd-61f869b8a031");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4b9e94f-43b4-463d-a47a-6f8a223b7aea");

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
    }
}
