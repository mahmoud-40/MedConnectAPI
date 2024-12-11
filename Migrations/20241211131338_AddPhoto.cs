using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_PatientId",
                table: "Records");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "24586763-9356-4dbf-b912-d536e5d34250" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "391d21c6-5c2e-4133-8387-723634eb46e0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "83c1967d-f3a7-49f2-8ba6-19bee5cc1723" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "c67e1c2c-34ec-4a2c-b9bb-41bd9bcfd1fd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "d3baf159-76f6-4845-92e0-fd966b450d15" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24586763-9356-4dbf-b912-d536e5d34250");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "391d21c6-5c2e-4133-8387-723634eb46e0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c1967d-f3a7-49f2-8ba6-19bee5cc1723");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c67e1c2c-34ec-4a2c-b9bb-41bd9bcfd1fd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d3baf159-76f6-4845-92e0-fd966b450d15");

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "094a0b63-1605-4ba5-97e0-d4702ce0926c", 0, "f3206580-5377-4184-8646-c721e59fd515", new DateTime(2024, 12, 11, 13, 13, 37, 627, DateTimeKind.Utc).AddTicks(9715), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEKrfV0N+WDO3B8C6MbDDEDwYYNruAYbXc1n4Iag43gOHq7a4nQDg0Hc4s6xnIAgLFw==", null, false, "8ac3c45f-e4b6-4125-b9c7-640f87959fee", false, "Yousef" },
                    { "3e914e28-b700-4fd1-aebf-36a545c26808", 0, "3e6797a2-5abf-4181-9eeb-c47b33bc8103", new DateTime(2024, 12, 11, 13, 13, 37, 762, DateTimeKind.Utc).AddTicks(5788), "Admin", "reemfadaly.23@gmail.com", true, false, null, "Reem Fadaly", "REEMFADALY.23@GMAIL.COM", "REEM", "AQAAAAIAAYagAAAAEJmexHMJKyHOCnXpKeyPR1ZmBcpinOl7r7AZCDHVdyPVrhxsFWehI+DFc+9VUZjEgg==", null, false, "a2672947-432c-44d4-929e-48c9829795cb", false, "reem" },
                    { "57703d0b-1c7f-48e2-a786-5356c7d6603e", 0, "f4d925ee-b091-485b-bb98-b2982bbeb98b", new DateTime(2024, 12, 11, 13, 13, 37, 583, DateTimeKind.Utc).AddTicks(1892), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEDxLJmIIJXWwtsvQRLNfU6VSoED6CI2vBM6LiWiXSP7v9iUsIs5m1nM/YAhOpAIltg==", null, false, "2de2195f-f6ed-42aa-882f-b9f327d3f4bd", false, "admin" },
                    { "6a6353c6-b8c0-4889-8d6d-edbb0121cf9c", 0, "934b0adc-27f2-485a-b0a3-1875f142e994", new DateTime(2024, 12, 11, 13, 13, 37, 718, DateTimeKind.Utc).AddTicks(1539), "Admin", "m.elbaadishy@gmail.com", true, false, null, "Mahmoud Abdulmawlaa", "M.ELBAADISHY@GMAIL.COM", "B3DSH", "AQAAAAIAAYagAAAAEEWcsQwW+e0BTTSznLEjTzl8b9pJaOO5alJizqxKMkofZbjSAnR86WbBEEqvbNNpIw==", null, false, "f88b3fd5-4151-45f1-9fb2-04f1e08b5d20", false, "b3dsh" },
                    { "c5eadfaf-64b4-4c6c-91c2-eb7b6a7c4d54", 0, "5faaf53f-bf94-4b15-bf69-2632193fd6df", new DateTime(2024, 12, 11, 13, 13, 37, 672, DateTimeKind.Utc).AddTicks(8666), "Admin", "gamalelbatawy@gmail.com", true, false, null, "Gamal Moemen", "GAMALELBATAWY@GMAIL.COM", "GAMAL", "AQAAAAIAAYagAAAAEBL5baEFzlE2qMoUZTltb/gNB6mZey6MXqD3i9vFlHbxni56yImhk2FPFlqc3Fs6jA==", null, false, "42d37d98-3cde-4a03-b305-59dfe5b7b44e", false, "Gamal" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0", "094a0b63-1605-4ba5-97e0-d4702ce0926c" },
                    { "0", "3e914e28-b700-4fd1-aebf-36a545c26808" },
                    { "0", "57703d0b-1c7f-48e2-a786-5356c7d6603e" },
                    { "0", "6a6353c6-b8c0-4889-8d6d-edbb0121cf9c" },
                    { "0", "c5eadfaf-64b4-4c6c-91c2-eb7b6a7c4d54" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_PatientId_ProviderId",
                table: "Records",
                columns: new[] { "PatientId", "ProviderId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_PatientId_ProviderId",
                table: "Records");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "094a0b63-1605-4ba5-97e0-d4702ce0926c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "3e914e28-b700-4fd1-aebf-36a545c26808" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "57703d0b-1c7f-48e2-a786-5356c7d6603e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "6a6353c6-b8c0-4889-8d6d-edbb0121cf9c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "c5eadfaf-64b4-4c6c-91c2-eb7b6a7c4d54" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "094a0b63-1605-4ba5-97e0-d4702ce0926c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e914e28-b700-4fd1-aebf-36a545c26808");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57703d0b-1c7f-48e2-a786-5356c7d6603e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a6353c6-b8c0-4889-8d6d-edbb0121cf9c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5eadfaf-64b4-4c6c-91c2-eb7b6a7c4d54");

            migrationBuilder.DropColumn(
                name: "photo",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "24586763-9356-4dbf-b912-d536e5d34250", 0, "620e2c90-394c-4894-9e70-c57d4f29b65a", new DateTime(2024, 12, 5, 22, 58, 9, 868, DateTimeKind.Utc).AddTicks(3342), "Admin", "gamalelbatawy@gmail.com", true, false, null, "Gamal Moemen", "GAMALELBATAWY@GMAIL.COM", "GAMAL", "AQAAAAIAAYagAAAAEIJ8jBMDDiuLBhqNpgD95Brk/RfpbP0YnhB3xBYdU3DXcD1+KW+racTe1IQoJ/n3FQ==", null, false, "edeb3bde-e4a1-4c50-8403-f4c74009636e", false, "Gamal" },
                    { "391d21c6-5c2e-4133-8387-723634eb46e0", 0, "322a943f-d41c-4a6e-b6d8-427790354bf0", new DateTime(2024, 12, 5, 22, 58, 9, 823, DateTimeKind.Utc).AddTicks(5173), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEBuzONAcPZwET0zeGExRnVsS+JBDLvBpp8QOEJREVCRBBjFrNCtM7rSAPljJqAKW1w==", null, false, "fed395f2-be8a-4dd5-9378-e14cfc41d807", false, "Yousef" },
                    { "83c1967d-f3a7-49f2-8ba6-19bee5cc1723", 0, "f59c8891-5d8a-4070-b2ad-2ca4ffa05b8c", new DateTime(2024, 12, 5, 22, 58, 9, 959, DateTimeKind.Utc).AddTicks(4770), "Admin", "reemfadaly.23@gmail.com", true, false, null, "Reem Fadaly", "REEMFADALY.23@GMAIL.COM", "REEM", "AQAAAAIAAYagAAAAEKy7C1agc+OBcDnpoalsIDUXooEdScPqTntypcxsJC12LgVlFWzSOMXYn1ibCsak8w==", null, false, "b2edf6fc-e887-4f2b-975a-38f203474f23", false, "reem" },
                    { "c67e1c2c-34ec-4a2c-b9bb-41bd9bcfd1fd", 0, "1f22ab2b-a706-4bb1-8f04-1d2484c22406", new DateTime(2024, 12, 5, 22, 58, 9, 914, DateTimeKind.Utc).AddTicks(3362), "Admin", "m.elbaadishy@gmail.com", true, false, null, "Mahmoud Abdulmawlaa", "M.ELBAADISHY@GMAIL.COM", "B3DSH", "AQAAAAIAAYagAAAAEIxXaTj0vL+nFdrnZ6eFMq5ovrod2Np589KSyMPGQXiz2DCY0XZni5SitHNNIJDmsQ==", null, false, "a13e6426-92da-40cd-a19c-fe387ad8c792", false, "b3dsh" },
                    { "d3baf159-76f6-4845-92e0-fd966b450d15", 0, "105bc2e9-4428-44a7-bf14-7c5fe50f7e36", new DateTime(2024, 12, 5, 22, 58, 9, 776, DateTimeKind.Utc).AddTicks(4293), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEAjzR/Kq3He66Z9xJoVvIulYdIhdIgfkwRfEZm7iFhQaLtZc1SIy1wzWJBgRkNGScg==", null, false, "181af9be-180f-42e4-8a15-7fc2611cdc4b", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0", "24586763-9356-4dbf-b912-d536e5d34250" },
                    { "0", "391d21c6-5c2e-4133-8387-723634eb46e0" },
                    { "0", "83c1967d-f3a7-49f2-8ba6-19bee5cc1723" },
                    { "0", "c67e1c2c-34ec-4a2c-b9bb-41bd9bcfd1fd" },
                    { "0", "d3baf159-76f6-4845-92e0-fd966b450d15" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_PatientId",
                table: "Records",
                column: "PatientId");
        }
    }
}
