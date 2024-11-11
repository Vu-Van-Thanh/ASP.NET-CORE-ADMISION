using Admission.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  /// <summary>
  /// Domain Model for Country
  /// </summary>
  public class Country
  {
    [Key]
    public Guid CountryID { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<Student>? Students { get; set; }
    public virtual ICollection<Relative>? Relatives { get; set; }
    }
}
