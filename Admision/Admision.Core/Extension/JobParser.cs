using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Admission.Core.Extension
{
    public class JobParser
    {
        private List<SalaryTemplate> _templates;
        public JobParser()
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
                    Pattern = @"(\d+)\s*-\s*(\d+)\s*triệu", // "A - B triệu"
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value) * 1_000_000,
                        Max = decimal.Parse(match.Groups[2].Value) * 1_000_000
                    }
                },
                new SalaryTemplate
                {
                    Pattern = @"(\d{1,3}(?:,\d{3})*)\s*-\s*(\d{1,3}(?:,\d{3})*)\s*vnd\s*(?:\\n|0vnd)*", // Tổng quát cho nhiều trường hợp
                    Process = match => new SalaryResult
                    {
                        Min = decimal.Parse(match.Groups[1].Value.Replace(",", "")),
                        Max = decimal.Parse(match.Groups[2].Value.Replace(",", ""))
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
            // Chỉ lấy phần chuỗi trước dấu xuống dòng đầu tiên
            int newlineIndex = salary.IndexOf('\n');
            if (newlineIndex >= 0)
            {
                salary = salary.Substring(0, newlineIndex).Trim();
            }
            salary = salary.ToLower()
                           .Replace(",", "")
                           .Replace("vnd", "")
                           .Replace("₫", "")
                           .Replace("/tháng", "")
                           .Trim();

            foreach (var template in _templates)
            {
                var match = Regex.Match(salary, template.Pattern);
                if (match.Success)
                {
                    return template.Process(match);
                }
            }

            return new SalaryResult { Min = 0, Max = 0 };
        }
        public SalaryResult ParseWage(string salary)
        {
            salary = salary.Trim();

            var parts = salary.Split('-');
            if (parts.Length == 2)
            {
                return new SalaryResult
                {
                    Min = long.Parse(parts[0]), 
                    Max = long.Parse(parts[1]) 
                };
            }
            else if (parts.Length == 1)
            {
                string temp = new string(salary.Where(char.IsDigit).ToArray());
                if (long.TryParse(temp, out long numericValue))
                {
                    return new SalaryResult { Min = numericValue, Max = numericValue };
                }
                else
                {
                    return new SalaryResult { Min = 0, Max = 0 };

                }
            }

            return new SalaryResult { Min = 0, Max = 0 };
        }
        public int ParseAmount(string amount)
        {
            string temp = new string(amount.Where(char.IsDigit).ToArray());
            if (!string.IsNullOrEmpty(temp) && int.TryParse(temp, out int numericValue))
            {
                return numericValue;
            }
            else
            {
                return 1;
            }
        }

        public ExperienceResult ParseExperience(string experience)
        {
            if (string.IsNullOrWhiteSpace(experience))
            {
                return new ExperienceResult { Min = 0, Max = 0 }; 
            }

            // Lấy tất cả các chữ số trong chuỗi
            string temp = new string(experience.Where(char.IsDigit).ToArray());
            int number = string.IsNullOrEmpty(temp) ? 0 : int.Parse(temp);

            if (experience.Contains("Trên"))
            {
                return new ExperienceResult { Min = number + 1, Max = 80 }; 
            }
            else if(experience.Contains("Dưới"))
            {
                return new ExperienceResult { Min = 0, Max = number };
            }
            else if (experience.Contains("đến"))
            {
                var numbers = experience.Split(new[] { "đến" }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(part => new string(part.Where(char.IsDigit).ToArray()))
                                         .Where(n => !string.IsNullOrEmpty(n))
                                         .Select(int.Parse)
                                         .ToArray();
                if (numbers.Length == 2)
                {
                    return new ExperienceResult { Min = numbers[0], Max = numbers[1] }; 
                }
            }
            else if(experience.Contains("Không yêu cầu"))
            {
                return new ExperienceResult { Min = 0, Max = 0 };

            }
            else if (!string.IsNullOrEmpty(temp)) 
            {
                return new ExperienceResult { Min = number, Max = number };
            }


            return new ExperienceResult { Min = 0, Max = 0 };
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
    public class ExperienceResult
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
