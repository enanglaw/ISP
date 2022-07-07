using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonPersonType
  {
    [Key]
    public int PersonPersonTypeId { get; set; }
    public int PersonId { get; set; }
    public int PersonTypeId { get; set; }

    [ForeignKey(nameof(PersonId))]
    [InverseProperty("PersonPersonType")]
    public virtual Person Person { get; set; }
    [ForeignKey(nameof(PersonTypeId))]
    [InverseProperty(nameof(PersonTypeMaster.PersonPersonType))]
    public virtual PersonTypeMaster PersonType { get; set; }
  }
}
