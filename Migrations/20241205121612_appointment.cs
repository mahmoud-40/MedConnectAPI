using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class appointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "40bb21bd-4fa5-45de-aac6-a421a4d1bf16", 0, "a0d65557-915e-4a8e-94b4-09e5b23d1e4a", new DateTime(2024, 12, 5, 12, 16, 11, 415, DateTimeKind.Utc).AddTicks(4598), "Admin", "gamalelbatawy@gmail.com", true, false, null, "Gamal Moemen", "GAMALELBATAWY@GMAIL.COM", "GAMAL", "AQAAAAIAAYagAAAAEDRttc20NKCNCHNZS4km6VHT2vPNy9CPbW15gG7MkkrfSrNnMSKj9tmKdmT5CCqafw==", null, false, "6ff465c7-2231-435a-bf63-fce0bdc3ac63", false, "Gamal" },
                    { "4f3bde11-81cb-438d-b3b1-b790869811c8", 0, "b3a7ed4d-cf10-44ca-a3b8-1a3e7c4e24f4", new DateTime(2024, 12, 5, 12, 16, 11, 316, DateTimeKind.Utc).AddTicks(4399), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEN37OgyfISbA5K87KEFX4fb8rUjlcXc3ryNm9DgKCgZZ7zO+vvexfJWAIrA5XhFONQ==", null, false, "6e24e87a-afe9-42ba-86df-ae9b3b40e954", false, "Yousef" },
                    { "abc139bb-12a0-46da-b673-934572b79172", 0, "f7923c59-8ec0-4365-ace3-49d5eabbafb3", new DateTime(2024, 12, 5, 12, 16, 11, 505, DateTimeKind.Utc).AddTicks(9499), "Admin", "m.elbaadishy@gmail.com", true, false, null, "Mahmoud Abdulmawlaa", "M.ELBAADISHY@GMAIL.COM", "B3DSH", "AQAAAAIAAYagAAAAEI1OXLpx/hxVcPFy90xNOTzxpcYjVOHOfMx/DwHrelzSrKrGbCkHqesWEUopoy+iRg==", null, false, "b181d750-2b3f-4d01-9129-17d751294979", false, "b3dsh" },
                    { "ec43cf0e-42ab-41c1-b33a-1723cdae4cb4", 0, "3368f50c-f041-4bb5-a4dd-7122f2680968", new DateTime(2024, 12, 5, 12, 16, 11, 226, DateTimeKind.Utc).AddTicks(3303), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEGHvRUTrJZayzXmEQOpwDS2ggz2YYg4eK6/Du7k8MJn9U03x9qcFJD4WMGULThrpQw==", null, false, "805f9f31-0ac2-4d45-96a7-42fa33720e6c", false, "admin" },
                    { "f8760cd4-d447-45de-9025-acb5f573f481", 0, "3ea1b6bb-3ed5-4940-875b-86279a72c25f", new DateTime(2024, 12, 5, 12, 16, 11, 599, DateTimeKind.Utc).AddTicks(7211), "Admin", "reemfadaly.23@gmail.com", true, false, null, "Reem Fadaly", "REEMFADALY.23@GMAIL.COM", "REEM", "AQAAAAIAAYagAAAAED1L1TP143mS4TNu4cPe7WVjiXQnYAvQvTZsjedR2VeL5Q6anVfsvzxpPDSZcnKnTA==", null, false, "8486a11e-eb37-4f90-b757-1c8c78d6a410", false, "reem" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0", "40bb21bd-4fa5-45de-aac6-a421a4d1bf16" },
                    { "0", "4f3bde11-81cb-438d-b3b1-b790869811c8" },
                    { "0", "abc139bb-12a0-46da-b673-934572b79172" },
                    { "0", "ec43cf0e-42ab-41c1-b33a-1723cdae4cb4" },
                    { "0", "f8760cd4-d447-45de-9025-acb5f573f481" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "40bb21bd-4fa5-45de-aac6-a421a4d1bf16" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "4f3bde11-81cb-438d-b3b1-b790869811c8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "abc139bb-12a0-46da-b673-934572b79172" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "ec43cf0e-42ab-41c1-b33a-1723cdae4cb4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "f8760cd4-d447-45de-9025-acb5f573f481" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40bb21bd-4fa5-45de-aac6-a421a4d1bf16");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f3bde11-81cb-438d-b3b1-b790869811c8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abc139bb-12a0-46da-b673-934572b79172");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ec43cf0e-42ab-41c1-b33a-1723cdae4cb4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8760cd4-d447-45de-9025-acb5f573f481");

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
    }
}
