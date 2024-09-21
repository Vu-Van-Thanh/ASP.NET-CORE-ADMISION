using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Score = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SchoolDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolID);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MediaUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.MediaID);
                    table.ForeignKey(
                        name: "FK_Medias_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InitialExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Results_ResultID",
                        column: x => x.ResultID,
                        principalTable: "Results",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorId);
                    table.ForeignKey(
                        name: "FK_Majors_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformationOfApplieds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestRoom = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GPA10 = table.Column<double>(type: "float", nullable: false),
                    GPA11 = table.Column<double>(type: "float", nullable: false),
                    GPA12 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationOfApplieds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformationOfApplieds_Majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformationOfApplieds_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformationOfApplieds_MajorID",
                table: "InformationOfApplieds",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_InformationOfApplieds_StudentID",
                table: "InformationOfApplieds",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Majors_SchoolID",
                table: "Majors",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ArticleID",
                table: "Medias",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ResultID",
                table: "Payments",
                column: "ResultID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_StudentID",
                table: "Results",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformationOfApplieds");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
