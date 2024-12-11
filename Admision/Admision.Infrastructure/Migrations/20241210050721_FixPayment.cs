using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_InformationOfApplieds_ResultID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ResultID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ResultID",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Id",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_InformationOfApplieds_Id",
                table: "Payments",
                column: "Id",
                principalTable: "InformationOfApplieds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_InformationOfApplieds_Id",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_Id",
                table: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "ResultID",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ResultID",
                table: "Payments",
                column: "ResultID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_InformationOfApplieds_ResultID",
                table: "Payments",
                column: "ResultID",
                principalTable: "InformationOfApplieds",
                principalColumn: "Id");
        }
    }
}
