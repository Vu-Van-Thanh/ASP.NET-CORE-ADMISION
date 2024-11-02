using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddStudentTrigger : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<int>(
				name: "Gender",
				table: "Students",
				type: "int",
				maxLength: 10,
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int",
				oldMaxLength: 10);

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

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<int>(
				name: "Gender",
				table: "Students",
				type: "int",
				maxLength: 10,
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldMaxLength: 10,
				oldNullable: true);
			migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterInsertUser;");
		}
	}
}
