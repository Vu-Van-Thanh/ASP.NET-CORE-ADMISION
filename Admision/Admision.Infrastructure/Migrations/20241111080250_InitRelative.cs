using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitRelative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.RenameColumn(
                name: "PathOfAvatar",
                table: "Students",
                newName: "Talent");

            migrationBuilder.AddColumn<string>(
                name: "AcademicPerformance10",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicPerformance11",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicPerformance12",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CandidateType",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Commune",
                table: "Students",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conduct10",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conduct11",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conduct12",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryID",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Students",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ethnic",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthStatus",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseholdType",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndentityCard",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceNumber",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoiningDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Member",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembershipBook",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembershipCard",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutstandingAchievements",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Partisan",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceIssued",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceJoining",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolicySubjectType",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Students",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    RelativeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelativeType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RelativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ethnic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Career = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceOfJob = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityCard = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    District = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Commune = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.RelativeID);
                    table.ForeignKey(
                        name: "FK_Relatives_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relatives_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentMedias",
                columns: table => new
                {
                    StudentMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentMediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMedias", x => new { x.StudentID, x.StudentMediaID });
                    table.ForeignKey(
                        name: "FK_StudentMedias_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[] { new Guid("4ac574fb-85a9-4159-9f2b-053b2bc6fc7e"), "Việt Nam" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CountryID",
                table: "Students",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Relatives_CountryID",
                table: "Relatives",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Relatives_StudentID",
                table: "Relatives",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Countries_CountryID",
                table: "Students",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Countries_CountryID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Relatives");

            migrationBuilder.DropTable(
                name: "StudentMedias");

            migrationBuilder.DropIndex(
                name: "IX_Students_CountryID",
                table: "Students");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("4ac574fb-85a9-4159-9f2b-053b2bc6fc7e"));

            migrationBuilder.DropColumn(
                name: "AcademicPerformance10",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AcademicPerformance11",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AcademicPerformance12",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CandidateType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Commune",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Conduct10",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Conduct11",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Conduct12",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Ethnic",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HealthStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HouseholdType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IndentityCard",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InsuranceNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "JoiningDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Member",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MembershipBook",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MembershipCard",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OutstandingAchievements",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Partisan",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PlaceIssued",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PlaceJoining",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PolicySubjectType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Talent",
                table: "Students",
                newName: "PathOfAvatar");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PersonName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "varchar(8)", nullable: true, defaultValue: "ABC12345")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                    table.CheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8");
                    table.ForeignKey(
                        name: "FK_Persons_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID");
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonID", "Address", "CountryID", "DateOfBirth", "Email", "Gender", "PersonName", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("012107df-862f-4f16-ba94-e5c16886f005"), "413 Sachtjen Way", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1990, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "hmosco8@tripod.com", "Male", "Hansiain", true },
                    { new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"), "2 Warrior Avenue", new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), new DateTime(1990, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "mconachya@va.gov", "Female", "Minta", true },
                    { new Guid("29339209-63f5-492f-8459-754943c74abf"), "57449 Brown Way", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1983, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "mjarrell6@wisc.edu", "Male", "Maddy", true },
                    { new Guid("2a6d3738-9def-43ac-9279-0310edc7ceca"), "97570 Raven Circle", new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), new DateTime(1988, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "mlingfoot5@netvibes.com", "Male", "Mitchael", false },
                    { new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"), "50467 Holy Cross Crossing", new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), new DateTime(1995, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "ttregona4@stumbleupon.com", "Gender", "Tani", false },
                    { new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"), "9334 Fremont Street", new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "vklussb@nationalgeographic.com", "Female", "Verene", true },
                    { new Guid("ac660a73-b0b7-4340-abc1-a914257a6189"), "4 Stuart Drive", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1998, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "pretchford7@virginia.edu", "Female", "Pegeen", true },
                    { new Guid("c03bbe45-9aeb-4d24-99e0-4743016ffce9"), "4 Parkside Point", new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), new DateTime(1989, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "mwebsdale0@people.com.cn", "Female", "Marguerite", false },
                    { new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"), "6 Morningstar Circle", new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ushears1@globo.com", "Female", "Ursa", false },
                    { new Guid("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"), "73 Heath Avenue", new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), new DateTime(1995, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "fbowsher2@howstuffworks.com", "Male", "Franchot", true },
                    { new Guid("cb035f22-e7cf-4907-bd07-91cfee5240f3"), "484 Clarendon Court", new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), new DateTime(1997, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lwoodwing9@wix.com", "Male", "Lombard", false },
                    { new Guid("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"), "83187 Merry Drive", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1987, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "asarvar3@dropbox.com", "Male", "Angie", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryID",
                table: "Persons",
                column: "CountryID");
        }
    }
}
