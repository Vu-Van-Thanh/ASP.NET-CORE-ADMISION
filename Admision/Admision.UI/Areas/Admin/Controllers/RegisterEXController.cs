/*using Entities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Admission.UI.Areas.Admin.Controllers
{
    public class RegisterEXController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterEXController(ApplicationDbContext context)
        {
            _context = context;
        }

        // View chính
        public ActionResult Index()
        {
            return View();
        }

        // API: Lấy danh sách kỳ thi
        [HttpGet]
        public JsonResult GetExamPeriods()
        {
            var periods = _context.InformationOfApplieds
                .Select(x => x.ExamPeriod)
                .Distinct()
                .ToList();

            return Json(periods, JsonRequestBehavior.AllowGet);
        }

        // API: Lấy danh sách sinh viên theo kỳ thi
        [HttpGet]
        public JsonResult GetStudentsByExamPeriod(string examPeriod)
        {
            var students = _context.InformationOfApplieds
                .Where(x => x.ExamPeriod == examPeriod)
                .Select(x => new
                {
                    x.StudentID,
                    x.Student.Name,
                    x.ExamPeriod,
                    x.TestRoom
                })
                .ToList();

            return Json(students, JsonRequestBehavior.AllowGet);
        }

        // API: Upload file Excel
        [HttpPost]
        public JsonResult UploadFile()
        {
            try
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), file.FileName);
                    file.SaveAs(path);
                    return Json(new { success = true, path });
                }

                return Json(new { success = false, message = "Không có file được tải lên." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // API: Random phòng và kíp thi
        [HttpPost]
        public JsonResult RandomRooms(string examPeriod, string filePath)
        {
            try
            {
                // Đọc dữ liệu phòng/kíp thi từ file Excel
                var roomsAndPeriods = new List<(string Room, string Period)>();
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet != null)
                    {
                        int row = 2;
                        while (worksheet.Cells[row, 1].Value != null)
                        {
                            roomsAndPeriods.Add((
                                worksheet.Cells[row, 1].Value.ToString(),
                                worksheet.Cells[row, 2].Value.ToString()
                            ));
                            row++;
                        }
                    }
                }

                // Lấy sinh viên thuộc kỳ thi
                var students = _context.InformationOfApplieds
                    .Where(x => x.ExamPeriod == examPeriod)
                    .ToList();

                var random = new Random();
                foreach (var student in students)
                {
                    var randomRoom = roomsAndPeriods[random.Next(roomsAndPeriods.Count)];
                    student.TestRoom = randomRoom.Room;
                    student.ExamPeriod = randomRoom.Period;
                }

                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
*/