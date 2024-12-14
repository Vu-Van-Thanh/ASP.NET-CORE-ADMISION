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
            

           return View(jobs);
        }

        public async Task<IActionResult> JobStatistics()
        {
            return View();
        }


        public async Task<IActionResult> GetAllJobByFilter()
        {
            var result = await _recruitDataService.GetJobByFilter();
            
            return Ok(result);
        }
    }
}
