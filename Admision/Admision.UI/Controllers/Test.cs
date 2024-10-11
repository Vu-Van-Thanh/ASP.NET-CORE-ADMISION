using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;


namespace Admission.UI.Controllers
{

    public class Test : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "File not selected";
                return View("Index");
            }

            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                // Nén ảnh và thay đổi kích thước
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 800), // Kích thước tối đa
                    Mode = ResizeMode.Max
                }));

                // Nén chất lượng ảnh
                using (var memoryStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(memoryStream, new JpegEncoder { Quality = 100 }); // Chất lượng 75%
                    var fileBytes = memoryStream.ToArray();

                    // Chuyển đổi thành chuỗi Base64
                    var base64String = Convert.ToBase64String(fileBytes);
                    ViewBag.Base64Image = $"data:image/jpeg;base64,{base64String}";
                    ViewBag.Message = "Image uploaded and compressed successfully";
                }
            }

            return View("Index");
        }

    }
}
