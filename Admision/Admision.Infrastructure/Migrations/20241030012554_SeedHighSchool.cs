using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedHighSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathOfAvatar",
                table: "Students",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HighSchoolName",
                table: "HighSchools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.InsertData(
                table: "HighSchools",
                columns: new[] { "HighSchoolID", "Address", "DepartmentOfEducation", "HighSchoolName", "Type" },
                values: new object[,]
                {
                    { new Guid("08ac0d47-4482-4abe-ab30-b549ff8e3bf3"), "Xã Vạn Khánh Huyện Vạn Ninh Tỉnh Khánh Hòa.", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Tô Văn Ơn", "Công lập" },
                    { new Guid("0e753b01-815e-48fe-8fdb-550baa35e7e6"), "75C Nguyễn Thị Minh Khai- Nha Trang - Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Nguyễn Thiện Thuật", "Tư thục" },
                    { new Guid("217772d7-611a-45c9-9346-31a8b0e5857e"), "05 Trường Sơn Phường Vĩnh Nguyên Thành phố Nha Trang", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Phạm Văn Đồng", "Công lập" },
                    { new Guid("2f19a6e1-cf10-43a4-9350-8c44a13a18ea"), "Lê Duẩn - Tô Hạp - Khánh Sơn - Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Khánh Sơn", "Công lập" },
                    { new Guid("39141de0-6e65-4d20-a5cf-cc8ca5ccc3e8"), "284 Nguyễn Công Trứ - Cam Nghĩa - Cam Ranh - Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Ngô Gia Tự", "Công lập" },
                    { new Guid("39bed8c0-b543-4858-a180-3d95e887c490"), "02 Hòn Chồng P.Vĩnh Phước TP.Nha Trang", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường PT DTNT tỉnh", "Công lập" },
                    { new Guid("45bce675-3c48-4b36-958d-9e78c67ffcb9"), "Phước Tuy - Diên Phước - Diên Khánh", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Nguyễn Thái Học", "Công lập" },
                    { new Guid("4b33c784-b8f6-4502-b4c8-e56f2b4fff7b"), "362 Hùng Vương- TT Vạn Giã- Vạn Ninh - Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Huỳnh Thúc Kháng", "Công lập" },
                    { new Guid("514174fd-7b08-4089-9768-a1410f9398e7"), "117 Hùng Vương Thị trấn Diên Khánh", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Hoàng Hoa Thám", "Công lập" },
                    { new Guid("6d85738c-463d-4591-8e5e-57df29320357"), "đường Nguyễn Thái Học", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Phan Bội Châu", "Công lập" },
                    { new Guid("84dd1dbe-0af3-44fe-8dc9-2479dd0881e2"), "183 lý Thường kiệt Tổ dp số 8 Vạn Giã-Vạn Ninh-Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Nguyễn Thị Minh Khai", "Công lập" },
                    { new Guid("95d0969f-1da2-4fd8-a79e-1635dea0274d"), "Quốc lộ 26 xã Ninh Phụng thị xã Ninh Hòa tĩnh Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Nguyễn Chí Thanh", "Công lập" },
                    { new Guid("9a574c91-a796-46c4-b56a-1bc8d0d4e369"), "Đường A1 KĐT Vĩnh Điềm Trung Vĩnh Hiệp Tp.Nha Trang", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường Tiểu học THCS THPT Quốc Tế Việt Nam Singapore", "Tư thục" },
                    { new Guid("9ea71901-ff11-4823-97b1-55323dcab175"), "32 Hàn Thuyên", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Nguyễn Văn Trỗi", "Công lập" },
                    { new Guid("9f0596a4-0556-478a-91c2-4034cf7c41b9"), "10 Phước Long", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT DL Lê Thánh Tôn", "Tư thục" },
                    { new Guid("a7d11b3c-8d8c-4701-ba77-22bc6d164581"), "25 Hai Bà Trưng", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THCS và THPT iSchool Nha Trang", "Tư thục" },
                    { new Guid("a949624c-0843-45cf-9759-f464f53a0b0f"), "386 Đường 3 tháng 4 Phường cam Linh Thành Phố Cam ranhtỉnh Khánh hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Trần Hưng Đạo", "Công lập" },
                    { new Guid("b1c755e4-1102-4403-ae5c-34f6d5558943"), "67 Yersin", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Chuyên Lê Quý Đôn", "Công lập" },
                    { new Guid("b4034916-fd21-42a7-a631-0dbdf2816371"), "Đường Đinh Tiên Hoàng Phường Ninh Hiệp Thị xã Ninh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Trần Cao Vân", "Công lập" },
                    { new Guid("b7eb5f5b-9396-4cf2-b90d-3268a889e869"), "90 Hùng Vương Thị trấn Khánh Vĩnh huyện Khánh Vĩnh", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Lạc Long Quân", "Công lập" },
                    { new Guid("ba2bb140-6287-4243-95fe-3711240161f7"), "số 01 đường 16/7; tổ dân phố 16", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Nguyễn Trãi", "Công lập" },
                    { new Guid("bdff377d-b000-4f30-9464-e35deeb19d6c"), "100 Nguyễn Trãi Cam Đức Cam Lâm Khánh Hoà", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Nguyễn Huệ", "Công lập" },
                    { new Guid("bfb353e8-ac8b-4225-8eab-3c8c714f2a16"), "Tổ 6 Thôn Phú Thạnh xã Vĩnh Thạnh Tp. Nha Trang tỉnh Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Hà Huy Tập", "Công lập" },
                    { new Guid("c8c74838-a0e0-428c-8fff-b48b96e15d76"), "Số 11 Đường Nguyễn Quyền - Phường Vĩnh Hải - TP Nha Trang- Tỉnh Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường PT Hermann Gmeiner", "Tư thục" },
                    { new Guid("d88b8099-d17a-499d-b1e0-c138b76fc97f"), "", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Lê Hồng Phong", "Công lập" },
                    { new Guid("df72fbe5-5039-4b46-8bec-f7fd4fc24614"), "08-Trường Chinh TT. Cam Đức H. Cam Lâm T. Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Trần Bình Trọng", "Công lập" },
                    { new Guid("e15837a2-5c5b-4b52-a48f-5bebbd33293b"), "02 Hòn Chồng", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Hoàng Văn Thụ", "Công lập" },
                    { new Guid("e1a2a3ec-2330-44c7-8a60-291fbac9dc3e"), "Tân Xương2  Suối Cát  Cam Lâm  Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Đoàn Thị Điểm", "Công lập" },
                    { new Guid("e9445ece-9731-4a83-830b-005a0d3f7d96"), "Mỹ Lợi - Ninh Lộc - Ninh Hòa - Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Tôn Đức Thắng", "Công lập" },
                    { new Guid("ed3635cc-0e76-4be5-8f5a-243903feb1d8"), "188 Hòn Khói phường Ninh Diêm thị xã Ninh Hòa tỉnh Khánh Hòa", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Trần Quý Cáp", "Công lập" },
                    { new Guid("ff95ca35-53df-45b5-a025-a70681972850"), "03 Lý Tự Trọng - Phường Lộc Thọ - Thành phố Nha Trang - Tỉnh Khánh Hòa.", "Sở Giáo dục và Đào tạo Khánh Hòa", "Trường THPT Lý Tự Trọng", "Công lập" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("08ac0d47-4482-4abe-ab30-b549ff8e3bf3"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("0e753b01-815e-48fe-8fdb-550baa35e7e6"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("217772d7-611a-45c9-9346-31a8b0e5857e"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("2f19a6e1-cf10-43a4-9350-8c44a13a18ea"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("39141de0-6e65-4d20-a5cf-cc8ca5ccc3e8"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("39bed8c0-b543-4858-a180-3d95e887c490"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("45bce675-3c48-4b36-958d-9e78c67ffcb9"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("4b33c784-b8f6-4502-b4c8-e56f2b4fff7b"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("514174fd-7b08-4089-9768-a1410f9398e7"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("6d85738c-463d-4591-8e5e-57df29320357"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("84dd1dbe-0af3-44fe-8dc9-2479dd0881e2"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("95d0969f-1da2-4fd8-a79e-1635dea0274d"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("9a574c91-a796-46c4-b56a-1bc8d0d4e369"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("9ea71901-ff11-4823-97b1-55323dcab175"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("9f0596a4-0556-478a-91c2-4034cf7c41b9"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("a7d11b3c-8d8c-4701-ba77-22bc6d164581"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("a949624c-0843-45cf-9759-f464f53a0b0f"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("b1c755e4-1102-4403-ae5c-34f6d5558943"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("b4034916-fd21-42a7-a631-0dbdf2816371"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("b7eb5f5b-9396-4cf2-b90d-3268a889e869"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("ba2bb140-6287-4243-95fe-3711240161f7"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("bdff377d-b000-4f30-9464-e35deeb19d6c"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("bfb353e8-ac8b-4225-8eab-3c8c714f2a16"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("c8c74838-a0e0-428c-8fff-b48b96e15d76"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("d88b8099-d17a-499d-b1e0-c138b76fc97f"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("df72fbe5-5039-4b46-8bec-f7fd4fc24614"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("e15837a2-5c5b-4b52-a48f-5bebbd33293b"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("e1a2a3ec-2330-44c7-8a60-291fbac9dc3e"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("e9445ece-9731-4a83-830b-005a0d3f7d96"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("ed3635cc-0e76-4be5-8f5a-243903feb1d8"));

            migrationBuilder.DeleteData(
                table: "HighSchools",
                keyColumn: "HighSchoolID",
                keyValue: new Guid("ff95ca35-53df-45b5-a025-a70681972850"));

            migrationBuilder.DropColumn(
                name: "PathOfAvatar",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "HighSchoolName",
                table: "HighSchools",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
