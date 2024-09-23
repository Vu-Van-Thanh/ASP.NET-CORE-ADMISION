using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Students_StudentID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_StudentID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentID",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StudentID",
                table: "Comments",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Students_StudentID",
                table: "Comments",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID");
        }
    }
}
