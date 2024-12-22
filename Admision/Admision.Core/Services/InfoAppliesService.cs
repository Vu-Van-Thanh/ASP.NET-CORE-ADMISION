using Admission.Core.Domain.Entities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Services
{
    public class InfoAppliesService : IInfoAppliesService
    {
        private readonly IInfoApplyRepository _infoApplyRepository;
        private readonly IStudentsService _studentsService;
        private readonly IMajorsService _majorService;
        private readonly IPaymentService _paymentService;
        private readonly IResultService _resultService;

        public InfoAppliesService(IInfoApplyRepository infoApplyRepository, IStudentsService studentsService, IMajorsService majorService, IPaymentService paymentService, IResultService resultService)
        {
            _infoApplyRepository = infoApplyRepository;
            _studentsService = studentsService;
            _majorService = majorService;   
            _paymentService = paymentService;
            _resultService = resultService;
        }

        public async Task<int> AddInfoApplies(InformationApplyDTO informationApplyDTO)
        {
            InformationOfApplied informationOfApplied = new InformationOfApplied();
            informationOfApplied.Id = Guid.NewGuid();
            informationOfApplied.StudentID = informationApplyDTO.StudentID;
            informationOfApplied.AdmissionMethod = informationApplyDTO.AdmissionMethod;
            informationOfApplied.ExamPeriod = informationApplyDTO.ExamPeriod;
            informationOfApplied.MajorID = informationApplyDTO.MajorID;
            informationOfApplied.GPA11 = informationApplyDTO.GPA11;
            informationOfApplied.GPA10 = informationApplyDTO.GPA10;
            informationOfApplied.GPA12 = informationApplyDTO.GPA12;
            informationOfApplied.TestRoom = null;
            informationOfApplied.TestDate = DateTime.Now.ToString();

             return await _infoApplyRepository.AddInfoApply(informationOfApplied);
        }

        public async Task<List<ExamClassDTO>> GetInfoApplyByStudentId(string studentId)
        {
            List<InformationOfApplied> list = await _infoApplyRepository.GetByStudentId(Guid.Parse(studentId));
            List<ExamClassDTO> result = new List<ExamClassDTO>();
            foreach(InformationOfApplied informationOfApplied in list)
            {
                ExamClassDTO exClass = new ExamClassDTO();
                exClass.Id = informationOfApplied.Id;
                exClass.AdmissionMethod = informationOfApplied.AdmissionMethod;
                exClass.MajorID = informationOfApplied.MajorID;
                exClass.MajorName = (await _majorService.GetMajorByMajorID(informationOfApplied.MajorID)).Name ?? "N/A";
                exClass.TestDate = informationOfApplied.TestDate;
                exClass.TestRoom = informationOfApplied.TestRoom;
                exClass.ExamPeriod = informationOfApplied.ExamPeriod;
                exClass.GPA11 = informationOfApplied.GPA11;
                exClass.GPA10 = informationOfApplied.GPA10;
                exClass.GPA12 = informationOfApplied.GPA12;
                Guid? paymentID = await _paymentService.GetPaymentByAI(informationOfApplied.Id);
                exClass.PaymentStatus = paymentID != null ? "Yes" : "No";
                exClass.Score = (await _resultService.GetScroreExam(informationOfApplied.Id)) ?? 0;
                result.Add(exClass);
            }
            return result;
        }

        public async Task<List<InfoStudentDTO>> GetInfoStudent(string ExamPeriod)
        {
            List<InformationOfApplied> list = await _infoApplyRepository.GetByEP(ExamPeriod);
            List<InfoStudentDTO> result = new List<InfoStudentDTO>();
            foreach (InformationOfApplied informationOfApplied in list)
            {
                Student temp = await _studentsService.GetStudentByStudentID(informationOfApplied.StudentID);
                result.Add(new InfoStudentDTO
                {

                    StudentID = informationOfApplied.StudentID,
                    StudentName = temp.FirstName + " " + temp.LastName,
                    AdmissionMethod = informationOfApplied.AdmissionMethod,
                    MajorID = informationOfApplied.MajorID,
                    TestDate = null,
                    TestRoom = null
                });
            }
            return result;
        }

        public async Task<List<(string, string, int)>> ReadRoomsFromExcel(IFormFile roomsFile)
        {
            var result = new List<(string, string, int)>(); 
            var shifts = new HashSet<string>(); 

           
            using (var stream = new MemoryStream())
            {
                await roomsFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    
                    var worksheet = package.Workbook.Worksheets[0];

                    
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        
                        string shift = worksheet.Cells[row, 3].Text.Trim(); // Kíp thi

                        // Thêm kíp vào set (đảm bảo kíp không bị trùng lặp)
                        if (!string.IsNullOrEmpty(shift))
                        {
                            shifts.Add(shift);
                        }
                    }
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string classRoom = worksheet.Cells[row, 1].Text.Trim();
                        int maxCapacity = int.Parse(worksheet.Cells[row, 2].Text.Trim());
                        foreach (var shift in shifts)
                        {
                            result.Add((classRoom, shift, maxCapacity));
                        }
                    }

                    
                }
            }

            return result;
        }

        public async Task<int> UpdateInfoApplies(List<InfoStudentDTO> informationStudentDTO)
        {
            List<InformationOfApplied> list = informationStudentDTO.Select(p => p.ToIOA()).ToList();
            List<InformationOfApplied> result = await _infoApplyRepository.UpdateInfo(list);
            return result.Count;
        }
    }
}
