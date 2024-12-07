using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_ProviderId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Records_RecordId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProviderId",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "1460faa9-5c49-4b5b-aeff-121a7cba9cab" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "8dfa9bf5-7ebc-4b25-8874-be84240c95d2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "a4cfc333-3a36-4bf8-947e-e0c244050f5b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0", "fda76e43-0b14-4752-898e-eadd43761bbc" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1460faa9-5c49-4b5b-aeff-121a7cba9cab");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8dfa9bf5-7ebc-4b25-8874-be84240c95d2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4cfc333-3a36-4bf8-947e-e0c244050f5b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fda76e43-0b14-4752-898e-eadd43761bbc");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Appointments",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_RecordId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorId");

            migrationBuilder.AddColumn<string>(
                name: "ProviderId",
                table: "Records",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Records_ProviderId",
                table: "Records",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_ProviderId",
                table: "Records",
                column: "ProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_ProviderId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ProviderId",
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

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointments",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                newName: "IX_Appointments_RecordId");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "Appointments",
                type: "time",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ProviderId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1460faa9-5c49-4b5b-aeff-121a7cba9cab", 0, "6c84ffb0-3bd7-44fc-bbab-73534e2b9d80", new DateTime(2024, 12, 4, 16, 38, 5, 31, DateTimeKind.Utc).AddTicks(2623), "Admin", "admin@joo.com", true, false, null, "Admin", "ADMIN@JOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEK0sB8KR3nUgBZYiYaDzSg16aEsOWZ6daR2DXXpncB0JMM+LxyDDTYALd4AMaAp7jw==", null, false, "318ef986-d987-4746-b813-d8f2cb3c0240", false, "admin" },
                    { "8dfa9bf5-7ebc-4b25-8874-be84240c95d2", 0, "90b86fc6-c05e-4cab-8c43-37027dfd2194", new DateTime(2024, 12, 4, 16, 38, 5, 165, DateTimeKind.Utc).AddTicks(8180), "Admin", "m.elbaadishy@gmail.com", true, false, null, "Mahmoud Abdulmawlaa", "M.ELBAADISHY@GMAIL.COM", "B3DSH", "AQAAAAIAAYagAAAAEOLc5/837CiNl0aJXy18ImY4lfgM/vJSnWxq2Lqy1tOwJ6ok9reV9aFRaawmwZ1OzQ==", null, false, "00ef3489-e5fe-4aee-bd54-e076f5c53896", false, "b3dsh" },
                    { "a4cfc333-3a36-4bf8-947e-e0c244050f5b", 0, "d7db92e8-7574-4d01-a0ba-5501eee5b9d5", new DateTime(2024, 12, 4, 16, 38, 5, 120, DateTimeKind.Utc).AddTicks(1552), "Admin", "gamalelbatawy@gmail.com", true, false, null, "Gamal Moemen", "GAMALELBATAWY@GMAIL.COM", "GAMAL", "AQAAAAIAAYagAAAAEPUVPeYPqI5qBVJXVvGGCU/fyw6ftsmB0p9YOY7Et1Rv44NatYhLNOhMOITVwL6rzQ==", null, false, "ea27b604-3db8-4998-bded-c2e08c3abe9d", false, "Gamal" },
                    { "fda76e43-0b14-4752-898e-eadd43761bbc", 0, "05a10492-6962-41e5-a4bb-0055222811a8", new DateTime(2024, 12, 4, 16, 38, 5, 75, DateTimeKind.Utc).AddTicks(4863), "Admin", "yuossefbakier@gmail.com", true, false, null, "Yousef Ahmed", "YUOSSEFBAKIER@GMAIL.COM", "YOUSEF", "AQAAAAIAAYagAAAAEMpc+6xYKitTLPAC9zfiXWUYXjBOrbQ3o63wXniYfSzKn/ODZcF1XGJ3sxL48o7cCA==", null, false, "0dfbd771-2084-4a36-9041-4e9da5d17676", false, "Yousef" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0", "1460faa9-5c49-4b5b-aeff-121a7cba9cab" },
                    { "0", "8dfa9bf5-7ebc-4b25-8874-be84240c95d2" },
                    { "0", "a4cfc333-3a36-4bf8-947e-e0c244050f5b" },
                    { "0", "fda76e43-0b14-4752-898e-eadd43761bbc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProviderId",
                table: "Appointments",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_ProviderId",
                table: "Appointments",
                column: "ProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Records_RecordId",
                table: "Appointments",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
