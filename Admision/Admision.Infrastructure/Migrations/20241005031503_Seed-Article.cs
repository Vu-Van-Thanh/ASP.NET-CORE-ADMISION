using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "Medias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "Content", "DateCreated", "Title" },
                values: new object[,]
                {
                    { new Guid("18fc6183-8a57-4deb-9d26-f28034395fc1"), "Điểm chuẩn vào các chương trình đào tạo của Đại học Bách khoa Hà Nội trong năm 2023 đã được công bố. Các thí sinh có thể tham khảo để chuẩn bị cho năm sau.", new DateTime(2024, 9, 30, 11, 30, 0, 0, DateTimeKind.Unspecified), "Điểm chuẩn năm 2023" },
                    { new Guid("24e5b629-cd94-4364-bb64-63e5a7e72dda"), "Đánh giá chất lượng chương trình đào tạo tại Đại học Bách khoa Hà Nội được thực hiện hàng năm để cải tiến và nâng cao chất lượng giảng dạy.", new DateTime(2024, 10, 4, 15, 45, 0, 0, DateTimeKind.Unspecified), "Đánh giá chương trình đào tạo" },
                    { new Guid("32460e72-2ba1-420f-b32c-53f4e2e01be3"), "Để đăng ký dự thi, các thí sinh cần chuẩn bị hồ sơ đầy đủ theo hướng dẫn của trường. Hãy đọc kỹ để tránh thiếu sót.", new DateTime(2024, 10, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), "Hướng dẫn chuẩn bị hồ sơ đăng ký" },
                    { new Guid("53aa8629-51a7-4810-8e69-0cc8a4c4ebd3"), "Trường Đại học Bách khoa Hà Nội thông báo lịch thi tuyển sinh cho năm 2024. Các thí sinh cần chú ý theo dõi để không bỏ lỡ.", new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Thông báo lịch thi tuyển sinh 2024" },
                    { new Guid("9aa5e0da-57d8-4393-b9c9-5a30374f0703"), "Đại học Bách khoa Hà Nội hiện có nhiều chương trình đào tạo nổi bật, đáp ứng nhu cầu thị trường lao động.", new DateTime(2024, 10, 2, 12, 15, 0, 0, DateTimeKind.Unspecified), "Các chương trình đào tạo nổi bật" },
                    { new Guid("a14fb4b4-9886-437b-add1-e4429424f6a2"), "Để hỗ trợ các thí sinh đưa ra quyết định về việc lựa chọn nguyện vọng một cách hợp lý nhất, Đại học Bách khoa Hà Nội đưa ra dự báo điểm chuẩn cho 64 chương trình đào tạo tuyển sinh năm 2024.", new DateTime(2024, 10, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "Đề án tuyển sinh 2024-2025" }
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "MediaID", "ArticleID", "MediaContent", "MediaType", "MediaUrl" },
                values: new object[,]
                {
                    { new Guid("266cc1fd-8d23-461a-bbed-17d8d77ad97b"), new Guid("18fc6183-8a57-4deb-9d26-f28034395fc1"), null, "video/mp4", "https://www.youtube.com/watch?v=8yVew7OrJdI" },
                    { new Guid("3f64434c-0c5e-4b28-9e87-ef0562c9536c"), new Guid("a14fb4b4-9886-437b-add1-e4429424f6a2"), null, "image", "https://firebasestorage.googleapis.com/v0/b/tsdhbk-632bb.appspot.com/o/slide2_1.png?alt=media&token=bc2ae7c2-f47e-4883-a600-7cc5c34e9c5e" },
                    { new Guid("9f2b04f0-4a38-4abc-823d-b83e618ff4ad"), new Guid("32460e72-2ba1-420f-b32c-53f4e2e01be3"), null, "image", "https://firebasestorage.googleapis.com/v0/b/tsdhbk-632bb.appspot.com/o/HY%2Ftd6.jpg?alt=media&token=a3878a22-1a97-40ae-bbbe-2cd3eac03cf9" },
                    { new Guid("bd669f86-a9c3-476e-8975-1efccf6ad3c5"), new Guid("9aa5e0da-57d8-4393-b9c9-5a30374f0703"), null, "image", "https://firebasestorage.googleapis.com/v0/b/tsdhbk-632bb.appspot.com/o/xttn%2FPOST-NG-a-NG-Ia-M-XTTN-2024-XT-6188-2429-1718331174.png?alt=media&token=6aba61bc-23b6-4f31-8d8d-5f81c1586951" },
                    { new Guid("c87767bd-dbfa-4416-a500-ee8bf14227f2"), new Guid("53aa8629-51a7-4810-8e69-0cc8a4c4ebd3"), null, "video/mp4", "https://www.youtube.com/watch?v=xLrHnIiAack" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("24e5b629-cd94-4364-bb64-63e5a7e72dda"));

            migrationBuilder.DeleteData(
                table: "Medias",
                keyColumn: "MediaID",
                keyValue: new Guid("266cc1fd-8d23-461a-bbed-17d8d77ad97b"));

            migrationBuilder.DeleteData(
                table: "Medias",
                keyColumn: "MediaID",
                keyValue: new Guid("3f64434c-0c5e-4b28-9e87-ef0562c9536c"));

            migrationBuilder.DeleteData(
                table: "Medias",
                keyColumn: "MediaID",
                keyValue: new Guid("9f2b04f0-4a38-4abc-823d-b83e618ff4ad"));

            migrationBuilder.DeleteData(
                table: "Medias",
                keyColumn: "MediaID",
                keyValue: new Guid("bd669f86-a9c3-476e-8975-1efccf6ad3c5"));

            migrationBuilder.DeleteData(
                table: "Medias",
                keyColumn: "MediaID",
                keyValue: new Guid("c87767bd-dbfa-4416-a500-ee8bf14227f2"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("18fc6183-8a57-4deb-9d26-f28034395fc1"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("32460e72-2ba1-420f-b32c-53f4e2e01be3"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("53aa8629-51a7-4810-8e69-0cc8a4c4ebd3"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("9aa5e0da-57d8-4393-b9c9-5a30374f0703"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: new Guid("a14fb4b4-9886-437b-add1-e4429424f6a2"));

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "Medias",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
