using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("1bd84ee8-08aa-4644-ba15-119fe56717da"),
                column: "Name",
                value: "( IT1 ) CNTT Khoa học Máy tính");

            migrationBuilder.UpdateData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("62c2ff34-1ad0-4852-81fa-b6751f6ef64d"),
                column: "Name",
                value: "( IT2 ) CNTT Kỹ thuật máy tính");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("1bd84ee8-08aa-4644-ba15-119fe56717da"),
                column: "Name",
                value: "( IT1 ) CNTT: Khoa học Máy tính");

            migrationBuilder.UpdateData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("62c2ff34-1ad0-4852-81fa-b6751f6ef64d"),
                column: "Name",
                value: "( IT2 ) CNTT: Kỹ thuật máy tính");
        }
    }
}
