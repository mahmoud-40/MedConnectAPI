using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "4d51a9eb-fc6f-413a-b802-f55e25ca9c38", 0, "999361ec-951a-4602-bc33-842cc3eb2e37", new DateTime(2024, 12, 2, 14, 19, 14, 286, DateTimeKind.Utc).AddTicks(622), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEMbN+qBoDAwsvgh3eK2sxytFu5aWbjE+q+oISuHaaksceYEr3gMJD+wb4+qJVuo90A==", null, false, "f5a01737-2685-4e4c-bc4f-58a42e9d8a9e", false, "Yousef" },
                    { "5afa67c0-043e-4276-a7bd-d0cdc560b6fa", 0, "81f0e0ea-29ce-4a44-b238-cbd33a8b1f8d", new DateTime(2024, 12, 2, 14, 19, 14, 220, DateTimeKind.Utc).AddTicks(226), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEL6iEp5aoyk3tKoxDAfohFfny7oBdgU0mF8y7pwLQVOakAjWlrldfcs/3PM4a2cm4w==", null, false, "88bff881-0ed1-4361-a6e7-6e4415d199c6", false, "admin" },
                    { "b0662161-3209-49a2-a9e8-19ccc5eccd8d", 0, "abea342e-0669-4da1-9108-6bcdcae03616", new DateTime(2024, 12, 2, 14, 19, 14, 420, DateTimeKind.Utc).AddTicks(6301), "Admin", "m.elbaadishy@gmail.com", true, false, null, "Mahmoud Abdulmawlaa", "M.ELBAADISHY@GMAIL.COM", "B3DSH", "AQAAAAIAAYagAAAAEO9MGD93YYIhvDAM0XJdeLUcvrG/h2S0jzIADWiFelGL6S9VwuLavFewMTA8IH+Z3g==", null, false, "b03cff07-0871-4d1b-b95a-1f7f0634d7e1", false, "b3dsh" },
                    { "c3d14f98-f86f-4bfc-b9f5-630d39314000", 0, "00085640-1e58-40f2-a67c-4623a9d8b182", new DateTime(2024, 12, 2, 14, 19, 14, 355, DateTimeKind.Utc).AddTicks(395), "Admin", "gamalelbatawy@gmail.com", true, false, null, "Gamal Moemen", "GAMALELBATAWY@GMAIL.COM", "GAMAL", "AQAAAAIAAYagAAAAELOrgqFxghR1UKAXjTTmCeKUYlQGsjnlCIPJda+6Fl03xMeiSV3NOxFgDhipZD4uEw==", null, false, "2b06f4a6-7a99-497b-adc1-43105b600f90", false, "Gamal" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0", "4d51a9eb-fc6f-413a-b802-f55e25ca9c38" },
                    { "0", "5afa67c0-043e-4276-a7bd-d0cdc560b6fa" },
                    { "0", "b0662161-3209-49a2-a9e8-19ccc5eccd8d" },
                    { "0", "c3d14f98-f86f-4bfc-b9f5-630d39314000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "4d51a9eb-fc6f-413a-b802-f55e25ca9c38" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "5afa67c0-043e-4276-a7bd-d0cdc560b6fa" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "b0662161-3209-49a2-a9e8-19ccc5eccd8d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "c3d14f98-f86f-4bfc-b9f5-630d39314000" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d51a9eb-fc6f-413a-b802-f55e25ca9c38");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5afa67c0-043e-4276-a7bd-d0cdc560b6fa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b0662161-3209-49a2-a9e8-19ccc5eccd8d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d14f98-f86f-4bfc-b9f5-630d39314000");

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
    }
}
