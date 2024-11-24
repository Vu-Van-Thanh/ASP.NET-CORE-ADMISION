using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class FamiliarDTO
    {
        public List<RelativeShort>? Relative { get; set; }
        public int? TotalPerson {  get; set; }
        public int? TotalStudy { get; set; }
        public IFormFile[]? HKImage { get; set; }
        public string? PolicySubjectType {  get; set; }
        public string? HouseholdType {  get; set; }
    }

    public class RelativeShort
    {
        public string? RelativeType { get; set; }
        public string? RelativeName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Career { get; set; }
        public string? PlaceOfJob { get; set; }
    }
}
