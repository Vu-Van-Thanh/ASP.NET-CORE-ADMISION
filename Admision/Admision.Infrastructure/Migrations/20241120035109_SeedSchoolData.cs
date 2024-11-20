using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSchoolData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Schools",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "SchoolID", "Email", "Location", "PhoneNumber", "SchoolDescription", "SchoolName", "WebSite" },
                values: new object[,]
                {
                    { new Guid("08d62e13-105c-4544-a0c3-093f055a2411"), "sepd@hust.edu.vn", "M321 - C7", "0902282489", null, "Khoa Khoa học và Công nghệ Giáo dục", null },
                    { new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c"), "sme@hust.edu.vn", "614M - C7", "0868040770", null, "Trường Cơ khí", null },
                    { new Guid("11b08db0-f13f-4dfb-8d7b-5b9d481b01be"), null, "D3 - 306", "02438692105", null, "Khoa Lý luận Chính trị", null },
                    { new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd"), "vseee@hust.edu.vn", "C7 - E605", "02438696211", null, "Trường Điện - Điện tử", null },
                    { new Guid("24020be3-2931-4802-b6c3-49b95d7528aa"), "fami@hust.edu.vn", "D3 - 106", "02438692137", null, "Khoa Toán - Tin", null },
                    { new Guid("65076895-b133-4454-b573-9f2d6a9a0989"), "sep@hust.edu.vn", "C10-116", "02438693350", null, "Khoa Vật lý Kỹ thuật", null },
                    { new Guid("878813d2-12c2-462f-b563-a568911fc5b4"), null, "Tầng 2 - Nhà thi đấu Bách Khoa", "02438684922", null, "Khoa Giáo dục Thể chất", null },
                    { new Guid("95697538-47a2-4d6b-a8a3-b99c264240bf"), "sofl@hust.edu.vn", "M310 - C7", null, null, "Khoa Ngoại ngữ", null },
                    { new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e"), "sem@hust.edu.vn", "C9 - 303,304", "02438692304", null, "Trường Kinh tế", null },
                    { new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807"), "scls@hust.edu.vn", null, "02438682470", null, "Trường Hoá và Khoa học sự sống", null },
                    { new Guid("c8a30332-1976-4c48-81f1-e6857032b3db"), "vp@soict.hust.edu.vn", "Nhà B1 - Phòng 505", "02438692463", null, "Trường Công nghệ Thông tin và Truyền thông", null },
                    { new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a"), "smse@hust.edu.vn", "D8 - 706", "02438680409", null, "Trường Vật liệu", null },
                    { new Guid("df205c8b-8194-4585-bd94-1dfca3cdd169"), null, "Nhà B7 - Ký túc xá ĐHBKHN", "02438680473", null, "Khoa Giáo dục Quốc phòng & An ninh", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("08d62e13-105c-4544-a0c3-093f055a2411"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("11b08db0-f13f-4dfb-8d7b-5b9d481b01be"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("24020be3-2931-4802-b6c3-49b95d7528aa"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("65076895-b133-4454-b573-9f2d6a9a0989"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("878813d2-12c2-462f-b563-a568911fc5b4"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("95697538-47a2-4d6b-a8a3-b99c264240bf"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("c8a30332-1976-4c48-81f1-e6857032b3db"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolID",
                keyValue: new Guid("df205c8b-8194-4585-bd94-1dfca3cdd169"));

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
