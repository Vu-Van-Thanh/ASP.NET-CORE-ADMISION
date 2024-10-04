using Admission.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Admission.UI.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = new List<Category>
        {
            new Category { Title = "Tuyển sinh Đại học", SubItems = new List<string>
            {   "Thông tin chung",
                "Đề án tuyển sinh",
                "Xét tuyển tài năng",
                "Kỳ thi đánh giá tư duy",
                "Điểm chuẩn tuyển sinh",
                "Xác thực chứng chỉ ngoại ngữ",
                "Hướng nghiệp"
            }},

            new Category { Title = "Tuyển sinh sau đại học", SubItems = new List<string>
            { "Tuyển sinh cao học",
              "Tuyển sinh NCS",
              "Tuyển sinh Kỹ sư chuyên sâu"
            }},

            new Category { Title = "Tin tức - Sự kiện", SubItems = new List<string> 
            { "Tin tức Đại học",
              "Tin tức Sau đại học",
              "Thành tích",
              "Thông báo",
              "Sự kiện",
              "Người Bách Khoa",
              "Cảm nhận Cựu sinh viên"
            }},

            new Category { Title = "Học phí - Học bổng", SubItems = new List<string> 
            { "Học phí",
              "Học bổng" 
            }},

            new Category { Title = "Hướng dẫn", SubItems = new List<string> 
            { "Câu hỏi thường gặp" 
            }},
        };

            return View(categories);
        }
    }
}
