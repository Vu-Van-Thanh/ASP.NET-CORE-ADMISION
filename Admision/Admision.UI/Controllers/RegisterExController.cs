using Microsoft.AspNetCore.Mvc;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Admission.Core.Domain.IdentityEntities;
using ServiceContracts.Enums;
using Admission.Core.Services;
using System.Net.Mail;

namespace Admission.UI.Controllers
{
    public class RegisterExController : Controller
    {
        private readonly IStudentMediasService _studentMediasService;
        private readonly IStudentsService _studentsService;
        private readonly IHighSchoolsService _highSchoolsService;
        private readonly IMajorsService _majorsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFilesService _filesService;
        private readonly IInfoAppliesService _inforAppliesService;
        private readonly IEmailService _emailService;
        

        public RegisterExController(IStudentMediasService studentMediasService, IStudentsService studentsService, IMajorsService majorsService, IHighSchoolsService highSchoolsService, UserManager<ApplicationUser> userManager,IFilesService filesService, IInfoAppliesService infoAppliesService, IEmailService emailService)
        {
            _studentMediasService = studentMediasService;
            _studentsService = studentsService;
            _majorsService = majorsService;
            _highSchoolsService = highSchoolsService;
            _userManager = userManager;
            _filesService = filesService;
            _inforAppliesService = infoAppliesService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> registerExam()
        {
            string? accountId = User.FindFirst("AccountID")?.Value;
            if (accountId == null)
            {
                return BadRequest();

            }
            Student? student = await _studentsService.GetStudentByAccountID(Guid.Parse(accountId));
            if (student == null)
            {
                return BadRequest();
            }
            ViewBag.HighSchools = await _highSchoolsService.GetAllHighSchools();
            List<StudentMediaDTO> list = await _studentMediasService.GetStudentMediasAsync(student.StudentID);
            RegisterExDTO registerExDTO = new RegisterExDTO();
            registerExDTO.FullName = student.FirstName + " " + student.LastName;
            registerExDTO.IdentityCardNumber = student.IndentityCard;
            registerExDTO.DateOfBirth = student.DateOfBirth;
            registerExDTO.Address = student.Address;
            string highSchool = (await _highSchoolsService.GetHighSchoolById(student.HighSchoolID)).HighSchoolName;
            registerExDTO.HighSchool = highSchool != null ? highSchool : "";
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            registerExDTO.Email = user.Email;
            registerExDTO.Phone = user.PhoneNumber;
            registerExDTO.TranscriptPath = list
                .Where(p => p.StudentMediaType == "HB")
                .Select(p => p.MediaUrl) // Lấy chỉ đường dẫn
                .ToList();
            registerExDTO.IdentityCard = list
                .Where(p => p.StudentMediaType == "CCCD")
                .Select(p => p.MediaUrl) // Lấy chỉ đường dẫn
                .ToList();

            List<Major>? majors = await _majorsService.GetAllMajors();
            ViewBag.Majors = majors;
            return View(registerExDTO);
        }
        [HttpPost]

        public async Task<IActionResult> registerExam(RegisterExDTO registerExDTO)
        {
            string? accountId = User.FindFirst("AccountID")?.Value;
            if (accountId == null)
            {
                return BadRequest();

            }
            Student? student = await _studentsService.GetStudentByAccountID(Guid.Parse(accountId));
            if (student == null)
            {
                return BadRequest();
            }
            List<StudentMediaDTO> list = await _studentMediasService.GetStudentMediasAsync(student.StudentID);
            Student updateStudent = new Student();
            updateStudent.StudentID = student.StudentID;
            var (first, last) = _studentsService.SplitStringAtFirstSpace(registerExDTO.FullName);
            updateStudent.FirstName = first;
            updateStudent.LastName = last;
            updateStudent.Gender = registerExDTO.Gender == "0" ? GenderOptions.Male : (registerExDTO.Gender == "1" ? GenderOptions.Female : GenderOptions.Other);
            updateStudent.IndentityCard = registerExDTO.IdentityCardNumber;
            updateStudent.DateOfBirth = registerExDTO.DateOfBirth;
            updateStudent.Address = registerExDTO.Address;
            updateStudent.HighSchoolID = (await _highSchoolsService.GetHighSchoolById(Guid.Parse(registerExDTO.HighSchool))).HighSchoolID;
            bool flag1 = await _studentsService.UpdateRegisEx(updateStudent);
            bool flag2, flag3, flag4;
            //lưu các media
            if (registerExDTO.NewPortraitPath != null)
            {
                List<string> hbs = await _filesService.SaveMediaFilesAsync(registerExDTO.NewTranscriptPath, $"HBImage/{student.StudentID}");
                List<StudentMedia> hb = hbs.Select(media => new StudentMedia
                {
                    StudentMediaID = Guid.NewGuid(),
                    StudentID = student.StudentID,
                    MediaUrl = media,
                    StudentMediaType = "HB"
                }).ToList();
                flag2 = await _studentsService.UpdateStudentMedia(hb);
            }
            if(registerExDTO.NewIdentityCard !=null)
            {
                List<string> cccds = await _filesService.SaveMediaFilesAsync(registerExDTO.NewIdentityCard, $"CCCDImage/{student.StudentID}");
                List<StudentMedia> cccd = cccds.Select(media => new StudentMedia
                {
                    StudentMediaID = Guid.NewGuid(),
                    StudentID = student.StudentID,
                    MediaUrl = media,
                    StudentMediaType = "CCCD"
                }).ToList();
                flag3 = await _studentsService.UpdateStudentMedia(cccd);

            }
            if (registerExDTO.NewPortraitPath != null)
            {
                IFormFile[] portraits = new IFormFile[] { registerExDTO.NewPortraitPath };
                List<string> cd = await _filesService.SaveMediaFilesAsync(portraits, $"CDImage/{student.StudentID}");
                List<StudentMedia> cdMedia = cd.Select(media => new StudentMedia
                {
                    StudentMediaID = Guid.NewGuid(),
                    StudentID = student.StudentID,
                    MediaUrl = media,
                    StudentMediaType = "CD"
                }).ToList();
                flag4 = await _studentsService.UpdateStudentMedia(cdMedia);
            }
            
            
            InformationApplyDTO infor= new InformationApplyDTO();
            infor.AdmissionMethod = registerExDTO.AdmissionMethod;
            infor.StudentID = student.StudentID;
            infor.MajorID = Guid.Parse(registerExDTO.MajorID);
            infor.ExamPeriod = registerExDTO.ExamPeriod;
            infor.GPA10 = registerExDTO.GPA10;
            infor.GPA11 = registerExDTO.GPA11;
            infor.GPA12 = registerExDTO.GPA12;
            int result = await _inforAppliesService.AddInfoApplies(infor);
            if (result > 0)
            {
                // Ví dụ sử dụng EmailService
                var emailBody = $"<p>Chào {student.FirstName} {student.LastName},</p>" +
                                $"<p>Bạn đã đăng ký thành công kỳ thi {registerExDTO.ExamPeriod}. Chúng tôi sẽ sớm gửi thông tin chi tiết.</p>" +
                                $"<p>Trân trọng,<br>Ban tuyển sinh</p>";

                
                await _emailService.SendEmailAsync(registerExDTO.Email, "Xác nhận đăng ký kỳ thi thành công", emailBody);

            }

            return Redirect("/");
        }
    }
}
