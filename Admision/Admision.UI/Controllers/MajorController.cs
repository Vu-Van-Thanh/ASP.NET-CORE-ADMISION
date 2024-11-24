using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class MajorController : Controller
    {
        private readonly IMajorsService _majorsService;
        private readonly IArticlesService _articlesService;


        public MajorController(IMajorsService majorsService, IArticlesService articlesService )
		{
			_majorsService = majorsService;
            _articlesService = articlesService;
		}
		public async Task<IActionResult> AcademicProgram()
        {
            IEnumerable<Major> list = await _majorsService.GetAllMajors();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> ProgramDetail(string ProgramID, string year)
        {
            try
            {
                // Lấy thông tin ngành học
                Major? major = await _majorsService.GetMajorByMajorID(Guid.Parse(ProgramID));

                if (major == null)
                {
                    return NotFound(new { message = "Ngành học không tồn tại." });
                }

                // Tạo mã ngành từ tên ngành
                string majorCode = _majorsService.ExtractMajorCode(major.Name);

                // Lấy bài viết liên quan đến ngành học và năm
                Article? exist = await _articlesService.GetArticleByType(year + " " + majorCode);
                exist.Content = await _articlesService.RenderArticleContent(exist);
                if (exist == null)
                {
                    return NotFound(new { message = "Không tìm thấy thông tin ngành học." });
                }

                // Trả dữ liệu về dạng JSON
                var result = new
                {
                    content = exist.Content,
                    title = exist.Title,
                    dateCreated = exist.DateCreated
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, new { message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }

    }
}
