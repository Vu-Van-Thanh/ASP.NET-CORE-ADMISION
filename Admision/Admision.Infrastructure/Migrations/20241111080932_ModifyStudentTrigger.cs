using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStudentTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterInsertUser;");

            // Create the updated trigger with CountryID
            migrationBuilder.Sql(@"
            CREATE TRIGGER trg_AfterInsertUser
            ON AspNetUsers
            AFTER INSERT
            AS
            BEGIN
                SET NOCOUNT ON;

                INSERT INTO Students (StudentID, LastName, HighSchoolID, CountryID, Id)
                SELECT 
                    NEWID(),                  -- Tạo StudentID mới
                    PersonName,               -- LastName từ PersonName của AspNetUsers
                    '514174FD-7B08-4089-9768-A1410F9398E7', -- HighSchool cố định
                    '4ac574fb-85a9-4159-9f2b-053b2bC6fc7e', -- CountryID cố định
                    Id                        -- Id từ bảng AspNetUsers
                FROM 
                    inserted;
            END;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterInsertUser;");

            // Revert to the original trigger without CountryID
            migrationBuilder.Sql(@"
            CREATE TRIGGER trg_AfterInsertUser
            ON AspNetUsers
            AFTER INSERT
            AS
            BEGIN
                SET NOCOUNT ON;

                INSERT INTO Students (StudentID, LastName, HighSchoolID, Id)
                SELECT 
                    NEWID(),                  -- Tạo StudentID mới
                    PersonName,               -- LastName từ PersonName của AspNetUsers
                    '514174FD-7B08-4089-9768-A1410F9398E7', -- HighSchool cố định
                    Id                        -- Id từ bảng AspNetUsers
                FROM 
                    inserted;
            END;
        ");
        }
    }
}
