using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Student_HighSchoolCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_HighSchools_HighSchoolID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HighSchoolIDA",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "HighSchoolID",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HighSchools_HighSchoolID",
                table: "Students",
                column: "HighSchoolID",
                principalTable: "HighSchools",
                principalColumn: "HighSchoolID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_HighSchools_HighSchoolID",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "HighSchoolID",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "HighSchoolIDA",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HighSchools_HighSchoolID",
                table: "Students",
                column: "HighSchoolID",
                principalTable: "HighSchools",
                principalColumn: "HighSchoolID");
        }
    }
}
