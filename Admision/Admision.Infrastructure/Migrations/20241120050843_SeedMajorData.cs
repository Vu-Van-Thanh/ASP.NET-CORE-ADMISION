using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMajorData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Majors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorId", "Name", "SchoolID" },
                values: new object[,]
                {
                    { new Guid("010e293a-e0cd-47b4-8ebc-bb7b07c3528a"), "( HE1 ) Kỹ thuật Nhiệt", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("08c1cd68-4b4e-467b-b5e7-50d987c3c494"), "( ED3 ) Quản lý giáo dục", new Guid("08d62e13-105c-4544-a0c3-093f055a2411") },
                    { new Guid("0f19b58a-b2c6-4982-b8d6-7ba952263906"), "( BF-E19 ) Kỹ thuật sinh học (Chương trình tiên tiến)", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("13037e5f-c109-48c8-b883-625ab536fcc6"), "( PH2 ) Kỹ thuật hạt nhân", new Guid("65076895-b133-4454-b573-9f2d6a9a0989") },
                    { new Guid("153db93a-80b5-45c2-b460-f56e2e732d23"), "( ME-E1 ) Kỹ thuật Cơ điện tử (Chương trình tiên tiến)", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("168de137-6cd5-4940-9d7b-48c40def31e0"), "( MS2 ) Chương trình Kỹ thuật vi điện tử và công nghệ Nano", new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a") },
                    { new Guid("19975496-ef62-4b01-bc45-30a1df61172e"), "( EM-E14 ) Logistics và Quản lý chuỗi cung ứng (Chương trình tiên tiến)", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("1a5f62be-84a7-4f3a-9bf1-bcd5afa29c82"), "( MI1 ) Toán - Tin", new Guid("24020be3-2931-4802-b6c3-49b95d7528aa") },
                    { new Guid("1bd84ee8-08aa-4644-ba15-119fe56717da"), "( IT1 ) CNTT: Khoa học Máy tính", new Guid("c8a30332-1976-4c48-81f1-e6857032b3db") },
                    { new Guid("1c4f05eb-5deb-4e3e-a093-91b458ca6e3c"), "( IT-E10 ) Khoa học Dữ liệu và Trí tuệ Nhân tạo (Chương trình tiên tiến)", new Guid("c8a30332-1976-4c48-81f1-e6857032b3db") },
                    { new Guid("23646f22-5cc2-418e-83ff-9edb221587fe"), "( IT-E6 ) Công nghệ thông tin (Việt-Nhật) (Chương trình tiên tiến)", new Guid("c8a30332-1976-4c48-81f1-e6857032b3db") },
                    { new Guid("24aa80e7-225c-4f9e-abd1-4bac2f70fbec"), "( ET1 ) Điện tử và Viễn thông", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("2ae51733-bc49-4fe4-bb90-a9860d6e16c5"), "( ET-E16 ) Truyền thông số và Kỹ thuật đa phương tiện (Chương trình tiên tiến", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("2b3e262c-64fa-4d6d-92ca-d7d74e173835"), "( FL2 ) Tiếng Anh chuyên nghiệp quốc tế", new Guid("95697538-47a2-4d6b-a8a3-b99c264240bf") },
                    { new Guid("2bfa586d-4153-4d89-8040-295726253749"), "( EE1 ) Kỹ thuật điện", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("2c388e07-4054-4d7c-b5c6-6658e5ce90c4"), "( BF1 ) Kỹ thuật Sinh học", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("2d5edd91-8fee-4a4d-9658-e18df4c3853d"), "( ME-LUH ) Cơ điện tử - ĐH Leibniz Hannover (Đức)", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("339fa357-2210-4146-abea-560cbf816ba5"), "( TROY-BA ) Quản trị kinh doanh - ĐH Troy (Hoa Kỳ)", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("35f4ad38-af23-4da0-bb67-9f1e87c3eea3"), "( TX1 ) Công nghệ Dệt May", new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a") },
                    { new Guid("372d24bf-3d23-4b30-a4a3-ed39d7301595"), "61 - ( TE3 ) Kỹ thuật Hàng không", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("385bf34c-92c6-4c46-bbc3-e786adedbefd"), "( BF2 ) Kỹ thuật Thực phẩm", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("38e314a4-56fd-41a8-888e-3715a1e23a10"), "( PH1 ) Vật lý kỹ thuật", new Guid("65076895-b133-4454-b573-9f2d6a9a0989") },
                    { new Guid("3c7ad9c4-615f-4167-914b-15f19cb7b38e"), "( EE-E18 ) Hệ thống điện và năng lượng tái tạo (Chương trình tiên tiến)", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("3de603c4-d831-455c-ba4c-2ceb3005ba3c"), "( FL1 ) Tiếng Anh Khoa học Kỹ thuật và Công nghệ", new Guid("95697538-47a2-4d6b-a8a3-b99c264240bf") },
                    { new Guid("3e5244d8-5bf1-4c76-ae88-8e4a353f97f5"), "( ME-NUT ) Cơ điện tử - ĐH Nagaoka (Nhật Bản)", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("45b792f6-1da5-4c7f-8ee4-40b1c2df4271"), "( EM3 ) Quản trị kinh doanh", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("46e9c83a-9360-4642-b7c2-936764d7601b"), "( EV1 ) Kỹ thuật Môi trường", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("4bbef06b-c43d-44b8-b16c-c980ff005fe5"), "( EM2 ) Quản lý công nghiệp", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("50e2553d-c151-42f0-b6b6-67c7c5409060"), "( IT-E7 ) Công nghệ thông tin (Global ICT)", new Guid("c8a30332-1976-4c48-81f1-e6857032b3db") },
                    { new Guid("618f750e-b7b5-4a79-a7a1-55d633717b87"), "( MS5 ) Kỹ thuật in", new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a") },
                    { new Guid("62a22f84-4675-478b-b3d7-12437760b1e6"), "( ET-E5 ) Kỹ thuật Y sinh (Chương trình tiên tiến)", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("62c2ff34-1ad0-4852-81fa-b6751f6ef64d"), "( IT2 ) CNTT: Kỹ thuật máy tính", new Guid("c8a30332-1976-4c48-81f1-e6857032b3db") },
                    { new Guid("69da374a-567a-4f9b-adbd-8d7e55191ec0"), "( PH3 ) Vật lý Y khoa", new Guid("65076895-b133-4454-b573-9f2d6a9a0989") },
                    { new Guid("6c3c5360-b104-4ce3-97a9-4ca989289e66"), "( MS-E3 ) Khoa học và Kỹ thuật Vật liệu (Chương trình tiên tiến)", new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a") },
                    { new Guid("6f1cd35b-3d26-4360-9290-055f5269b033"), "( ME2 ) Kỹ thuật Cơ khí", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("704bd8cc-cfdd-4488-98ff-a66d3df3a91e"), "( IT-E15 ) An toàn không gian số (Chương trình tiên tiến)", new Guid("c8a30332-1976-4c48-81f1-e6857032b3db") },
                    { new Guid("728b5d83-60b2-4f83-a36f-ffa3a7702faa"), "( EM1 ) Quản lý năng lượng", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("7482ec65-86df-4005-aa6f-5cd38aa5b6a7"), "( MS3 ) Công nghệ vật liệu polyme và compozit", new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a") },
                    { new Guid("79aaae30-5cf4-4036-ac80-a30dc0a6c186"), "( TROY-IT ) Khoa học máy tính - ĐH Troy (Hoa Kỳ)", new Guid("24020be3-2931-4802-b6c3-49b95d7528aa") },
                    { new Guid("7cf8d25a-969a-4dd8-977a-c896ef5b48ec"), "( CH1 ) Kỹ thuật Hóa học", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("80e3ddb4-68b9-46e0-88fd-684d47bc1de4"), "( EE2 ) Kỹ thuật Điều khiển - Tự động hóa", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("8b52605b-0f63-4612-a451-2e96eae55b95"), "( ED2 ) Công nghệ giáo dục", new Guid("08d62e13-105c-4544-a0c3-093f055a2411") },
                    { new Guid("8b53abd2-c183-48fe-9a1f-e505f520bb0a"), "( TE-EP ) Cơ khí hàng không (Chương trình Việt - Pháp PFIEV)", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("8d60abe7-bc93-457e-81b4-1d8d5eb4efe9"), "( ET-E9 ) Hệ thống nhúng thông minh và IoT (Chương trình tiên tiến)", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("8f9f59f5-1151-4d4b-8d65-7c2b127fc504"), "( CH-E11 ) Kỹ thuật Hóa dược (Chương trình tiên tiến)", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("976a5443-933c-49bf-a587-e04fbe23f924"), "( EE-E8 ) Kỹ thuật Điều khiển - Tự động hóa (Chương trình tiên tiến)", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("97ee66fb-6593-48d6-b6a5-18d76f5a528f"), "( MI2 ) Hệ thống thông tin quản lý", new Guid("24020be3-2931-4802-b6c3-49b95d7528aa") },
                    { new Guid("9efb59f1-a5de-4191-9d9b-d8870b1a48a7"), "( ET2 ) Kỹ thuật Y sinh", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("9fcd5cf0-2380-47aa-800b-120992504f54"), "( ET-LUH ) Điện tử-Viễn thông - ĐH Leibniz Hannover (Đức)", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("a2e80403-235f-4043-849d-e8b0d5a5090f"), "( TE-E2 ) Kỹ thuật Ô tô (Chương trình tiên tiến)", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("a5eaed7d-dd1b-460c-8777-c0d83c3d363c"), "( ME1 ) Kỹ thuật Cơ điện tử", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("aa3b3433-fea0-4b5f-9c53-20b27516e42b"), "( BF-E12 ) Kỹ thuật thực phẩm (Chương trình tiên tiến)", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("ac54d038-8f72-44ff-ae4d-01e989c0277c"), "( CH2 ) Hóa học", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("bbeb60c6-00cc-4f45-86ed-1d954cbde4b3"), "( MS1 ) Kỹ thuật Vật liệu", new Guid("d0566f46-3751-457e-bb8f-42d32e69e72a") },
                    { new Guid("c2e12946-e05a-42bf-859f-c9021a08581e"), "( IT-EP ) Công nghệ thông tin (Việt-Pháp) (Chương trình tiên tiến)", new Guid("c8a30332-1976-4c48-81f1-e6857032b3db") },
                    { new Guid("c56f1ec4-03ba-4565-8696-7baa4523c6a2"), "( ME-GU ) Cơ khí - Chế tạo máy - ĐH Griffith (Úc)", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("d294f67b-f69a-4ef3-9603-48a0647ce302"), "( EM-E13 ) Phân tích kinh doanh (Chương trình tiên tiến)", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("d7be9946-cbcd-4763-9e16-46a3761c91dd"), "( EM4 ) Kế toán", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("d80ee30c-0446-41db-8f91-501e3deb9364"), "( EV2 ) Quản lý Tài nguyên và Môi trường", new Guid("b47a4f55-2ae0-41fc-8f2c-9f5ee675e807") },
                    { new Guid("d886a8ab-59e5-461f-844d-e3df11e2862e"), "( EM5 ) Tài chính - Ngân hàng", new Guid("9b3a20f7-15b8-4bb1-864a-52b32fe92d1e") },
                    { new Guid("eb5473c1-95ca-4329-87e6-fb4f910070db"), "( TE1 ) Kỹ thuật Ô tô", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") },
                    { new Guid("ec4b6480-38e3-4179-b89d-a9ccf1352737"), "( EE-EP ) Tin học công nghiệp và Tự động hóa (Chương trình Việt-Pháp PFIEV)", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("ed4c3102-6046-42ae-abe5-b24df89a2402"), "( ET-E4 ) Kỹ thuật Điện tử - Viễn thông (Chương trình tiên tiến)", new Guid("1f8cdc05-60e1-445b-bbef-04424cb46bfd") },
                    { new Guid("ee7e1395-68e7-4e19-9b16-9b11f573fe05"), "( TE2 ) Kỹ thuật Cơ khí động lực", new Guid("0e0087ec-1a97-46fb-9b93-ec81a472535c") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("010e293a-e0cd-47b4-8ebc-bb7b07c3528a"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("08c1cd68-4b4e-467b-b5e7-50d987c3c494"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("0f19b58a-b2c6-4982-b8d6-7ba952263906"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("13037e5f-c109-48c8-b883-625ab536fcc6"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("153db93a-80b5-45c2-b460-f56e2e732d23"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("168de137-6cd5-4940-9d7b-48c40def31e0"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("19975496-ef62-4b01-bc45-30a1df61172e"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("1a5f62be-84a7-4f3a-9bf1-bcd5afa29c82"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("1bd84ee8-08aa-4644-ba15-119fe56717da"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("1c4f05eb-5deb-4e3e-a093-91b458ca6e3c"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("23646f22-5cc2-418e-83ff-9edb221587fe"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("24aa80e7-225c-4f9e-abd1-4bac2f70fbec"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("2ae51733-bc49-4fe4-bb90-a9860d6e16c5"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("2b3e262c-64fa-4d6d-92ca-d7d74e173835"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("2bfa586d-4153-4d89-8040-295726253749"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("2c388e07-4054-4d7c-b5c6-6658e5ce90c4"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("2d5edd91-8fee-4a4d-9658-e18df4c3853d"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("339fa357-2210-4146-abea-560cbf816ba5"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("35f4ad38-af23-4da0-bb67-9f1e87c3eea3"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("372d24bf-3d23-4b30-a4a3-ed39d7301595"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("385bf34c-92c6-4c46-bbc3-e786adedbefd"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("38e314a4-56fd-41a8-888e-3715a1e23a10"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("3c7ad9c4-615f-4167-914b-15f19cb7b38e"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("3de603c4-d831-455c-ba4c-2ceb3005ba3c"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("3e5244d8-5bf1-4c76-ae88-8e4a353f97f5"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("45b792f6-1da5-4c7f-8ee4-40b1c2df4271"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("46e9c83a-9360-4642-b7c2-936764d7601b"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("4bbef06b-c43d-44b8-b16c-c980ff005fe5"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("50e2553d-c151-42f0-b6b6-67c7c5409060"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("618f750e-b7b5-4a79-a7a1-55d633717b87"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("62a22f84-4675-478b-b3d7-12437760b1e6"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("62c2ff34-1ad0-4852-81fa-b6751f6ef64d"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("69da374a-567a-4f9b-adbd-8d7e55191ec0"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("6c3c5360-b104-4ce3-97a9-4ca989289e66"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("6f1cd35b-3d26-4360-9290-055f5269b033"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("704bd8cc-cfdd-4488-98ff-a66d3df3a91e"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("728b5d83-60b2-4f83-a36f-ffa3a7702faa"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("7482ec65-86df-4005-aa6f-5cd38aa5b6a7"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("79aaae30-5cf4-4036-ac80-a30dc0a6c186"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("7cf8d25a-969a-4dd8-977a-c896ef5b48ec"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("80e3ddb4-68b9-46e0-88fd-684d47bc1de4"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("8b52605b-0f63-4612-a451-2e96eae55b95"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("8b53abd2-c183-48fe-9a1f-e505f520bb0a"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("8d60abe7-bc93-457e-81b4-1d8d5eb4efe9"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("8f9f59f5-1151-4d4b-8d65-7c2b127fc504"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("976a5443-933c-49bf-a587-e04fbe23f924"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("97ee66fb-6593-48d6-b6a5-18d76f5a528f"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("9efb59f1-a5de-4191-9d9b-d8870b1a48a7"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("9fcd5cf0-2380-47aa-800b-120992504f54"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("a2e80403-235f-4043-849d-e8b0d5a5090f"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("a5eaed7d-dd1b-460c-8777-c0d83c3d363c"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("aa3b3433-fea0-4b5f-9c53-20b27516e42b"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("ac54d038-8f72-44ff-ae4d-01e989c0277c"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("bbeb60c6-00cc-4f45-86ed-1d954cbde4b3"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("c2e12946-e05a-42bf-859f-c9021a08581e"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("c56f1ec4-03ba-4565-8696-7baa4523c6a2"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("d294f67b-f69a-4ef3-9603-48a0647ce302"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("d7be9946-cbcd-4763-9e16-46a3761c91dd"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("d80ee30c-0446-41db-8f91-501e3deb9364"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("d886a8ab-59e5-461f-844d-e3df11e2862e"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("eb5473c1-95ca-4329-87e6-fb4f910070db"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("ec4b6480-38e3-4179-b89d-a9ccf1352737"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("ed4c3102-6046-42ae-abe5-b24df89a2402"));

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: new Guid("ee7e1395-68e7-4e19-9b16-9b11f573fe05"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Majors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
