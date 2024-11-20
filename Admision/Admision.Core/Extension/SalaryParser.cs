using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Admission.Core.Extension
{
    public class SalaryParser
    {
        private List<SalaryTemplate> _templates;
        public SalaryParser()
        {
            _templates = new List<SalaryTemplate>
            {
                new SalaryTemplate
                {
                    Pattern = @"(\d+)\s*tr\s*-\s*(\d+)\s*tr\s*vnd", // "A Tr - B Tr VND"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 1_000_000,
                        Max = decimal.Parse(match.Groups[2].Value) * 1_000_000
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"(\d+),000\s*-\s*(\d+),000\s*usd", // "A,000 - B,000 USD"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 1_000 * 24_000,
                        Max = decimal.Parse(match.Groups[2].Value) * 1_000 * 24_000
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"(\d+,\d{3},\d{3})\s*-\s*(\d+,\d{3},\d{3})\s*vnd", // "A,000,000 - B,000,000 VNĐ"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value.Replace(",", "")),
                        Max = decimal.Parse(match.Groups[2].Value.Replace(",", ""))
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"trên\s*(\d+)\s*triệu", // "Trên A triệu"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 1_000_000,
                        Max = 0
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"(\d+)\s*triệu\s*-\s*(\d+)\s*triệu", // "A triệu - B triệu"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 1_000_000,
                        Max = decimal.Parse(match.Groups[2].Value) * 1_000_000
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"(\d+)tr\s*-\s*(\d+)tr\s*₫/tháng", // "Atr-Btr ₫/tháng"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 1_000_000,
                        Max = decimal.Parse(match.Groups[2].Value) * 1_000_000
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"(\d{1,3}(?:,\d{3})*)\s*-\s*(\d{1,3}(?:,\d{3})*)\s*vnd\s*0vnd", // "8,000,000-10,000,000VNĐ0VNĐ"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value.Replace(",", "")),
                        Max = decimal.Parse(match.Groups[2].Value.Replace(",", ""))
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"từ\s*\$\s*(\d+),000\s*/tháng", // "Từ $ A,000 /tháng"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 1_000 * 24_000,
                        Max = 0
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"\$\s*(\d+)\s*-\s*(\d+)\s*/tháng", // "$ 400-1,200 /tháng"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 24_000,
                        Max = decimal.Parse(match.Groups[2].Value) * 24_000
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"\$\s*(\d+),(\d+)\s*/tháng", // "$ 1,200 /tháng"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value + match.Groups[2].Value) * 24_000,
                        Max = decimal.Parse(match.Groups[1].Value + match.Groups[2].Value) * 24_000
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"tới\s*(\d+)tr\s*₫/tháng", // "Tới Atr ₫/tháng"
                    Process = match => new SalaryResult
                    {
                        Min = 0,
                        Max = decimal.Parse(match.Groups[1].Value) * 1_000_000
                    }
                }
            };
        }
        public SalaryResult Parse(string salary)
        {
            // Chuẩn hóa chuỗi đầu vào
            salary = salary.ToLower()
                           .Replace(",", "")
                           .Replace("vnd", "")
                           .Replace("₫", "")
                           .Replace("/tháng", "")
                           .Trim();

            // Tìm template phù hợp và xử lý
            foreach (var template in _templates)
            {
                var match = Regex.Match(salary, template.Pattern);
                if (match.Success)
                {
                    return template.Process(match);
                }
            }

            // Không khớp template nào
            return new SalaryResult { Min = 0, Max = 0 };
        }
    }
    public class SalaryTemplate
    {
        public string Pattern { get; set; }
        public Func<Match, SalaryResult> Process { get; set; }
    }

    public class SalaryResult
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public override string ToString()
        {
            return $"Min: {Min}, Max: {Max}";
        }
    }
}
