using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonAliasName
  {
    [Key]
    public int AliasNameId { get; set; }
    public int PersonId { get; set; }
    [Required]
    [StringLength(50)]
    public string AliasName { get; set; }

    [ForeignKey(nameof(PersonId))]
    [InverseProperty("PersonAliasName")]
    public virtual Person Person { get; set; }
  }
}
