using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("18fc6183-8a57-4deb-9d26-f28034395fc1"),
                column: "Type",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("24e5b629-cd94-4364-bb64-63e5a7e72dda"),
                column: "Type",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("32460e72-2ba1-420f-b32c-53f4e2e01be3"),
                column: "Type",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("53aa8629-51a7-4810-8e69-0cc8a4c4ebd3"),
                column: "Type",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("9aa5e0da-57d8-4393-b9c9-5a30374f0703"),
                column: "Type",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("a14fb4b4-9886-437b-add1-e4429424f6a2"),
                column: "Type",
                value: "Normal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Articles");
        }
    }
}
