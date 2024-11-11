using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitPostMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "PostMedias",
                columns: table => new
                {
                    PostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMedias", x => new { x.PostID, x.MediaID });
                    table.ForeignKey(
                        name: "FK_PostMedias_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupID", "Name" },
                values: new object[,]
                {
                    { new Guid("1bf4c6b1-b102-4063-9396-8823a70ff7b5"), "Viện Kỹ thuật Điện và Điện tử" },
                    { new Guid("2084a35b-6ceb-4974-8c1d-823e95bf02a7"), "Trường Kỹ thuật Công nghệ Thông tin" },
                    { new Guid("4b1cf118-6612-4d47-97fe-8d6055f3bf67"), "Viện Quản trị Kinh doanh" },
                    { new Guid("5b97f946-5446-404a-8ea7-f72de1f1bf08"), "Trường Kỹ thuật Xây dựng" },
                    { new Guid("63690708-42e3-4887-89b5-e3e7c5cfa8ef"), "Viện Điện tử Viễn thông" },
                    { new Guid("6a7a706e-55fb-4675-a721-7d5ab0ee583d"), "Viện Kỹ thuật Hóa học" },
                    { new Guid("6e041819-6114-4781-94b9-6892fe124f93"), "Trường Kinh tế và Quản lý" },
                    { new Guid("6f20eaf9-0c3a-41ef-ab74-4e9de7add63a"), "Trường Kỹ thuật Điện" },
                    { new Guid("6fbc24f2-713a-42c9-980c-77c46de81761"), "Viện Cơ học" },
                    { new Guid("7ffe994d-da38-4136-9f79-4e6695fd9696"), "Trường Kỹ thuật Cơ khí" },
                    { new Guid("8bbab4e8-2cc2-4489-a8bd-8baf31894cec"), "Trường Kỹ thuật Hóa học" },
                    { new Guid("986c9e54-9ef0-4ce9-b714-b3f8f67234f0"), "Viện Khoa học và Công nghệ Vật liệu" },
                    { new Guid("996e2db4-7341-408f-a57c-bc39f74207bd"), "Viện Cơ khí" },
                    { new Guid("ae0c4e13-922f-4ff5-aa8d-b2b650ced61e"), "Viện Công nghệ Sinh học" },
                    { new Guid("b11ba393-c8bc-4496-a02b-fce57326100e"), "Trường Kỹ thuật Cơ khí Động lực" },
                    { new Guid("caab93de-f077-4f0f-af70-431ffb630caf"), "Viện Khoa học Máy tính" },
                    { new Guid("d9bce23c-ac7f-4491-8820-9702eb6ecf11"), "Viện Xây dựng" },
                    { new Guid("e51c655a-25b5-4596-8ba8-1f711436febf"), "Viện Toán học và Tin học Ứng dụng" },
                    { new Guid("e726d97a-0cc2-4e98-ac73-e91b3eb1b645"), "Viện Công nghệ Thông tin và Truyền thông" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostMedias");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("1bf4c6b1-b102-4063-9396-8823a70ff7b5"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("2084a35b-6ceb-4974-8c1d-823e95bf02a7"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("4b1cf118-6612-4d47-97fe-8d6055f3bf67"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("5b97f946-5446-404a-8ea7-f72de1f1bf08"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("63690708-42e3-4887-89b5-e3e7c5cfa8ef"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("6a7a706e-55fb-4675-a721-7d5ab0ee583d"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("6e041819-6114-4781-94b9-6892fe124f93"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("6f20eaf9-0c3a-41ef-ab74-4e9de7add63a"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("6fbc24f2-713a-42c9-980c-77c46de81761"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("7ffe994d-da38-4136-9f79-4e6695fd9696"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("8bbab4e8-2cc2-4489-a8bd-8baf31894cec"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("986c9e54-9ef0-4ce9-b714-b3f8f67234f0"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("996e2db4-7341-408f-a57c-bc39f74207bd"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("ae0c4e13-922f-4ff5-aa8d-b2b650ced61e"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("b11ba393-c8bc-4496-a02b-fce57326100e"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("caab93de-f077-4f0f-af70-431ffb630caf"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("d9bce23c-ac7f-4491-8820-9702eb6ecf11"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("e51c655a-25b5-4596-8ba8-1f711436febf"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupID",
                keyValue: new Guid("e726d97a-0cc2-4e98-ac73-e91b3eb1b645"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
