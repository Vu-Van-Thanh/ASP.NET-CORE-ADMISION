using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Students_StudentID",
                table: "Results");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentID",
                table: "Results",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "InfoAppliedID",
                table: "Results",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Results_InfoAppliedID",
                table: "Results",
                column: "InfoAppliedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_InformationOfApplieds_InfoAppliedID",
                table: "Results",
                column: "InfoAppliedID",
                principalTable: "InformationOfApplieds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Students_StudentID",
                table: "Results",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_InformationOfApplieds_InfoAppliedID",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Students_StudentID",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_InfoAppliedID",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "InfoAppliedID",
                table: "Results");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentID",
                table: "Results",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Students_StudentID",
                table: "Results",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
