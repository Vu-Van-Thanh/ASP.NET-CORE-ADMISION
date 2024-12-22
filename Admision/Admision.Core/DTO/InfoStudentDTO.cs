using Admission.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.DTO
{
    public class InfoStudentDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        [StringLength(50)]
         
        public string? AdmissionMethod { get; set; }

        public Guid MajorID { get; set; }

        public string? TestDate { get; set; }

        [StringLength(10)]
        public string? TestRoom { get; set; }
    }
    public static class InfoAppliedExtension
    {
        public static InformationOfApplied ToIOA(this InfoStudentDTO info)
        {
            return new InformationOfApplied { StudentID = info.StudentID, AdmissionMethod = info.AdmissionMethod, MajorID = info.MajorID, TestDate = info.TestDate, TestRoom = info.TestRoom };
        }
    }

}
