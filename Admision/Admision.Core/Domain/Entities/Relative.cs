using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Core.Domain.Entities
{
    public class Relative
    {
        [Key]
        public Guid RelativeID { get; set; }
        [StringLength(10)]
        public string RelativeType { get; set; }
        public string RelativeName { get; set; }
		public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; } = "Việt Nam";

        [StringLength(30)]
        public string? Ethnic { get; set; }
        [StringLength(50)]
        public string? Religion { get; set; } = "Không";

        [StringLength(30)]
        public string? Career { get; set; }
        public Guid CountryID { get; set; }


        [StringLength(200)]
        public string? PlaceOfJob { get; set; }

        [StringLength(20)]
        public string? Phone {  get; set; }

        [StringLength(15)]
        public string? IdentityCard { get; set; }

        [StringLength(40)]
        public string? Email { get; set; }

        [StringLength(200)] 
        public string? Address { get; set; }

        [ForeignKey("CountryID")]
        public Country? Country { get; set; }
        [StringLength(40)]
        public string? Province { get; set; }
        [StringLength(40)]
        public string? District { get; set; }
        [StringLength(40)]
        public string? Commune { get; set; }
        public Guid StudentID { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
    }
}
