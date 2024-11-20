using Admission.Core.DTO;
using Admission.Core.Extension;
using Admission.Core.ServiceContracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Admission.Core.Services
{
    public class RecruitDataService : IRecruitDataService
    {
        private readonly string _connectionString;

        public RecruitDataService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RecruitConnetion");
        }

        public async Task<List<JobDTO>> GetJob()
        {
            var jobList = new List<JobDTO>();
            string query = "SELECT Link, Web, Nganh AS Major, TenCV AS JobName, CongTy AS Company, " +
                   "TinhThanh AS Province, Luong AS Wage, LoaiHinh AS Type, KinhNghiem AS YOE, " +
                   "CapBac AS Level, HanNopCV AS DeadlineSubmit, YeuCau AS Requirement, " +
                   "MoTa AS JobDescription, PhucLoi AS Welfare, SoLuong AS Amount, Image " +
                   "FROM dbo.Stg_Data_Raw";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            jobList.Add(new JobDTO
                            {
                                Link = reader["Link"]?.ToString(),
                                Web = reader["Web"]?.ToString(),
                                Major = reader["Major"]?.ToString(),
                                JobName = reader["JobName"]?.ToString(),
                                Company = reader["Company"]?.ToString(),
                                Province = reader["Province"]?.ToString(),
                                Wage = reader["Wage"]?.ToString(),
                                Type = reader["Type"]?.ToString(),
                                YOE = reader["YOE"]?.ToString() ?? "Không yêu cầu kinh nghiệm",
                                Level = reader["Level"]?.ToString() ?? "Tất cả các cấp bậc",
                                DeadlineSubmit = reader["DeadlineSubmit"]?.ToString(),
                                Requirement = reader["Requirement"]?.ToString(),
                                JobDescription = reader["JobDescription"]?.ToString(),
                                Welfare = reader["Welfare"]?.ToString(),
                                Amount = reader["Amount"]?.ToString(),
                                Image = reader["Image"]?.ToString()
                            });
                        }
                    }
                }
            }


            
            return jobList;
        }

        public async Task<object> GetJobStatic()
        {
            var jobs = await GetJob(); 

            // Biểu đồ cột: Số lượng công việc theo ngành nghề
            var jobByMajor = jobs
                .GroupBy(job => job.Major)
                .Select(group => new
                {
                    Major = group.Key,
                    Count = group.Sum(j => {
                        var cleanedAmount = string.IsNullOrEmpty(j.Amount) ? "0" : System.Text.RegularExpressions.Regex.Replace(j.Amount, @"[^\d]", "");
                        return int.Parse(cleanedAmount);
                    })
                }).ToList();

            // Biểu đồ cột: Số lượng công việc theo khu vực
            var jobByProvince = jobs
                .GroupBy(job => job.Province)
                .Select(group => new
                {
                    Province = group.Key,
                    Count = group.Sum(j => {
                        var cleanedAmount = string.IsNullOrEmpty(j.Amount) ? "0" : System.Text.RegularExpressions.Regex.Replace(j.Amount, @"[^\d]", "");
                        return int.Parse(cleanedAmount);
                    })
                }).ToList();

            // Biểu đồ tròn: Phân bổ công việc theo loại
            var jobByType = jobs
                .GroupBy(job => job.Type)
                .Select(group => new
                {
                    Type = group.Key,
                    Count = group.Count()
                }).ToList();

            // Biểu đồ tròn: Phân bổ công việc theo cấp bậc
            var jobByLevel = jobs
                .GroupBy(job => job.Level)
                .Select(group => new
                {
                    Level = group.Key,
                    Count = group.Count()
                }).ToList();

            // Biểu đồ đường: Hạn nộp hồ sơ theo thời gian
            var jobTrend = jobs
                .Select(job => new
                {
                    Deadline = ParseDeadline(job.DeadlineSubmit)
                })
                .Where(job => job.Deadline != null) // Loại bỏ các giá trị không hợp lệ
                .GroupBy(job => job.Deadline.Value.ToString("yyyy-MM"))
                .Select(group => new
                {
                    Date = group.Key,
                    Count = group.Count()
                })
                .OrderBy(trend => trend.Date)
                .ToList();

            // Biểu đồ thanh ngang: Lương trung bình theo ngành nghề
            SalaryParser salaryParser = new SalaryParser();
            var salaryByMajor = jobs
                .GroupBy(job => job.Major)
                .Select(group => new
                {
                    Major = group.Key,
                    AverageSalary = group.Average(j => salaryParser.Parse(j.Wage).Min)
                }).ToList();

            return new
            {
                jobByMajor = jobByMajor,
                jobByProvince = jobByProvince,
                jobByType = jobByType,
                jobByLevel = jobByLevel,
                jobTrend = jobTrend,
                salaryByMajor = salaryByMajor
            };
        }




        // Hàm xử lý Parse Deadline
        private DateTime? ParseDeadline(string deadline)
        {
            if (string.IsNullOrWhiteSpace(deadline)) return null;

            // Xử lý nếu có tiền tố như "Hạn nộp hồ sơ:"
            if (deadline.ToLower().Contains("hạn nộp hồ sơ"))
            {
                deadline = deadline.Substring(deadline.LastIndexOf(":") + 1).Trim();
            }

            // Thử Parse các định dạng khác nhau
            DateTime parsedDate;
            if (DateTime.TryParseExact(deadline, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            if (DateTime.TryParse(deadline, out parsedDate)) // ISO-8601 và các định dạng thông dụng
            {
                return parsedDate;
            }

            // Nếu không thể Parse, trả về null
            return null;
        }
    }
}
