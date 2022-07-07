using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonTypeMaster
  {
    public PersonTypeMaster()
    {
      PersonPersonType = new HashSet<PersonPersonType>();
    }

    [Key]
    public int PersonTypeId { get; set; }
    [Required]
    [StringLength(50)]
    public string PersonTypeName { get; set; }
    [Required]
    public bool? IsActive { get; set; }

    [InverseProperty("PersonType")]
    public virtual ICollection<PersonPersonType> PersonPersonType { get; set; }
  }
}
