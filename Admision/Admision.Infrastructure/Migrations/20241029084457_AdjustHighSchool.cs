using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustHighSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Province",
                table: "HighSchools");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "HighSchools",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentOfEducation",
                table: "HighSchools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "HighSchools",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "HighSchools");

            migrationBuilder.DropColumn(
                name: "DepartmentOfEducation",
                table: "HighSchools");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "HighSchools");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "HighSchools",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }
    }
}
