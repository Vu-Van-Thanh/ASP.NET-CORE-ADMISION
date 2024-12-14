using Microsoft.AspNetCore.Http;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class StudentUpdateInfoDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? Ethnic { get; set; }
        public string? Religion { get; set; }
        public string? PlaceOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IndentityCard { get; set; }
        public string? PlaceIssued { get; set; }
        public string? Phone { get; set; }  // user 
        public string? Email { get; set; }  // user
        public string? Country { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Commune { get; set; }
        public string? Address { get; set; }
        public string? RelativePos { get; set; }
        public string? RelativeName { get; set; }
        public string? RelativePhone { get; set; }
        public string? CandidateType { get; set; }
        public string? Member { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? PlaceJoining { get; set; }
        public string? MembershipBook { get; set; }
        public string? MembershipCard { get; set; }
        public string? Partisan { get; set; }
        public string? HealthStatus { get; set; }
        public string? InsuranceNumber { get; set; }
        public IFormFile[]? CCCDImage { get; set; }
        public IFormFile[]? BHYTImage { get; set; }
	}
}
