using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 1, null, "Programming", null });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "Bio", "Email", "FullName", "JoinedDate" },
                values: new object[,]
                {
                    { 1, "Software Developer", "ahmed@example.com", "Ahmed Hassan", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Data Science Instructor", "sara@example.com", "Sara Ali", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "DateOfBirth", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, new DateTime(2004, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar@example.com", "Omar Mohamed" },
                    { 2, new DateTime(2003, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "mona@example.com", "Mona Ali" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 2, null, "Web Development", 1 },
                    { 3, null, "Database", 1 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, 2, null, 1, true, 0, 500m, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Fundamentals" },
                    { 2, 3, null, 2, true, 1, 400m, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SQL Server Basics" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "EnrollmentDate", "FinalGrade", "ProgressPercentage", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 95m, 100m, 1 },
                    { 2, 2, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 40m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);
        }
    }
}
