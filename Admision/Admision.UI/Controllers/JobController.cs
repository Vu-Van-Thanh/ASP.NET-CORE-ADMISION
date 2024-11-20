using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Admission.UI.Controllers
{
    public class JobController : Controller
    {
        private readonly IRecruitDataService _recruitDataService;

        public JobController(IRecruitDataService recruitDataService)
        {
            _recruitDataService = recruitDataService;
        }
        public async Task<IActionResult> JobView(FilterDTO filterDTO)
        {
            List<JobDTO> jobs = new List<JobDTO>();
            if (filterDTO == null)
            {
                jobs = await _recruitDataService.GetJob();
            }

           return View(jobs);
        }

        public async Task<IActionResult> JobStatistics()
        {
            // Gọi service để lấy dữ liệu thống kê
            var statistics = await _recruitDataService.GetJobStatic();

            // Truyền dữ liệu xuống View
            return View(statistics);
        }
    }
}
