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

        public async Task<object> GetJobByFilter()
        {
            var jobList = new List<JobDTO>();
            string query = "SELECT Link, Web, Nganh AS Major, TenCV AS JobName, CongTy AS Company, " +
                   "TinhThanh AS Province, Luong AS Wage, LoaiHinh AS Type, KinhNghiem AS YOE, " +
                   "CapBac AS Level, HanNopCV AS DeadlineSubmit, YeuCau AS Requirement, " +
                   "MoTa AS JobDescription, PhucLoi AS Welfare, SoLuong AS Amount, Image " +
                   "FROM dbo.Stg_Data_Raw";
            JobParser parser = new JobParser();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            try
                            {
                                // Parse and add to the list
                                SalaryResult salary = parser.ParseWage(reader["Wage"]?.ToString() ?? string.Empty);
                                int amount = parser.ParseAmount(reader["Amount"]?.ToString());
                                ExperienceResult experience = parser.ParseExperience(reader["YOE"]?.ToString());
                                jobList.Add(new JobDTO
                                {
                                    Link = reader["Link"]?.ToString(),
                                    Web = reader["Web"]?.ToString(),
                                    Major = reader["Major"]?.ToString(),
                                    JobName = reader["JobName"]?.ToString(),
                                    Company = reader["Company"]?.ToString(),
                                    Province = reader["Province"]?.ToString(),
                                    Wage = reader["Wage"]?.ToString(),
                                    MinWage = salary.Min,
                                    MaxWage = salary.Max,
                                    Type = reader["Type"]?.ToString(),
                                    YOE = reader["YOE"]?.ToString() ?? "Không yêu cầu kinh nghiệm",
                                    MaxYOE = experience.Max,
                                    MinYOE = experience.Min,
                                    Level = reader["Level"]?.ToString() ?? "Tất cả các cấp bậc",
                                    DeadlineSubmit = reader["DeadlineSubmit"]?.ToString(),
                                    Requirement = reader["Requirement"]?.ToString(),
                                    JobDescription = reader["JobDescription"]?.ToString(),
                                    Welfare = reader["Welfare"]?.ToString(),
                                    Amount = amount,
                                    Image = reader["Image"]?.ToString()
                                });
                            }
                            catch (Exception ex)
                            {
                                // Log the error and skip the record
                                Console.WriteLine($"Error processing record: {ex.Message}");
                                // Bạn có thể sử dụng logging framework thay vì Console.WriteLine
                                continue; // Bỏ qua bản ghi lỗi và tiếp tục
                            }
                        }
                    }
                }
            }
            return jobList;
        }

    }
}
