using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical.Migrations
{
    /// <inheritdoc />
    public partial class HandleReminders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Appointments_AppointmentId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AppointmentId",
                table: "Notifications");

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

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notifications",
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
                name: "IX_Notifications_ReleaseDate",
                table: "Notifications",
                column: "ReleaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReleaseDate_IsSeen",
                table: "Notifications",
                columns: new[] { "ReleaseDate", "IsSeen" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ReleaseDate",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ReleaseDate_IsSeen",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

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
                name: "ReleaseDate",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AppointmentId",
                table: "Notifications",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Appointments_AppointmentId",
                table: "Notifications",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
