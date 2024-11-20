using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class FilesService : IFilesService
    {
        private readonly string _rootFolderPath;

        public FilesService()
        {
            _rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        public string GetMediaType(string filePath)
        {
            string extension = Path.GetExtension(filePath)?.ToLower(); // Lấy phần mở rộng và chuyển về chữ thường
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
            {
                return "image";
            }
            else if (extension == ".mp4" || extension == ".avi" || extension == ".mov" || extension == ".mkv")
            {
                return "video";
            }
            return "Unknown"; // Mặc định nếu không phải ảnh hay video
        }

        public async Task<List<string>> SaveMediaFilesAsync(IFormFile[] mediaFiles, string folderName)
        {
            string targetFolderPath = Path.Combine(_rootFolderPath, folderName);
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
            List<string> savedFilePaths = new List<string>();
            if (mediaFiles != null && mediaFiles.Length > 0)
            {
                foreach (var file in mediaFiles)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(targetFolderPath, fileName);
                    var fileExtension = Path.GetExtension(file.FileName);

                    var counter = 1;
                    while (System.IO.File.Exists(filePath))
                    {
                        filePath = Path.Combine(targetFolderPath, $"{Path.GetFileNameWithoutExtension(fileName)}_{counter++}{fileExtension}");
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    string relativeFilePath = Path.Combine(folderName, Path.GetFileName(filePath))
                        .Replace("\\", "/");
                    savedFilePaths.Add("/" + relativeFilePath);
                }

            }
            return savedFilePaths;
        }
    }
}
