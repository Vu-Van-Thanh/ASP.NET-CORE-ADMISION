using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Admission.UI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RegisterEXController : Controller
    {
        private  List<InfoStudentDTO> students;
        private readonly IInfoAppliesService _info;
        

        public RegisterEXController(IInfoAppliesService info)
        {
            students = new List<InfoStudentDTO>();   
            _info = info;
        }

        [HttpGet]
        public IActionResult ExamRegister()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExamRegister(IFormFile rooms)
        {
            if (TempData["students"] != null)
            {
                // Lấy lại students từ TempData
                students = JsonConvert.DeserializeObject<List<InfoStudentDTO>>(TempData["students"].ToString());
            }
            // Đọc thông tin phòng thi từ tệp Excel
            List<(string room, string shift, int maxCapacity)> roomsList = await _info.ReadRoomsFromExcel(rooms);

            Random random = new Random();
            int roomIndex = 0;

            // Tạo danh sách để theo dõi số lượng tối đa còn lại cho mỗi phòng và kíp
            // Chúng ta sẽ lưu số lượng tối đa còn lại cho mỗi phòng/kíp trong một danh sách mới
            var roomCapacity = roomsList.ToDictionary(
                r => (r.room, r.shift),
                r => r.maxCapacity
            );

            foreach (var student in students)
            {
                // Lấy phòng thi và kíp thi cho sinh viên
                var room = roomsList[roomIndex].room;
                var shift = roomsList[roomIndex].shift;

                // Kiểm tra xem có còn chỗ không cho phòng và kíp này
                if (roomCapacity[(room, shift)] > 0)
                {
                    // Cập nhật thông tin phòng thi và kíp thi cho sinh viên
                    student.TestRoom = room;
                    student.TestDate = shift; // Có thể thay đổi theo yêu cầu

                    // Giảm số lượng sinh viên còn lại cho phòng/kíp thi này
                    roomCapacity[(room, shift)]--;

                    // Nếu phòng thi đã hết chỗ, chuyển sang phòng tiếp theo
                    if (roomCapacity[(room, shift)] == 0)
                    {
                        roomIndex++;
                        if (roomIndex >= roomsList.Count)
                        {
                            // Nếu hết phòng thi, dừng lại
                            break;
                        }
                    }
                }
                else
                {
                   
                    Console.WriteLine($"Không còn chỗ trong phòng {room} cho kíp {shift}");
                }
            }

            int result = await _info.UpdateInfoApplies(students);
            if(result == students.Count)
            {
                return PartialView("_InfoAppliedStudent", students);
            }
            else
            {
                return BadRequest();
            }
            
        }



        public async Task<IActionResult> GetStudentData(string ExamPeriod)
        {
            List<InfoStudentDTO> list = await _info.GetInfoStudent(ExamPeriod);
            TempData["students"] = JsonConvert.SerializeObject(list);
            /*students.AddRange(list);*/
            return PartialView("_InfoAppliedStudent",list);
        }
    }
}
