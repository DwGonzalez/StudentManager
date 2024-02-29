using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Matematicas" },
                    { 2, "Lengua Española" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Leonel", "Fernandez" },
                    { 2, "Luis", "Abinader" }
                });

            migrationBuilder.InsertData(
                table: "Attendance",
                columns: new[] { "Id", "ClassId", "CreatedDate", "IsPresent", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1 },
                    { 2, 1, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 }
                });

            migrationBuilder.InsertData(
                table: "StudentClass",
                columns: new[] { "Id", "ClassId", "Score", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, 80, 1 },
                    { 2, 2, 70, 1 },
                    { 3, 1, 65, 2 },
                    { 4, 2, 100, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attendance",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attendance",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentClass",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentClass",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentClass",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudentClass",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
