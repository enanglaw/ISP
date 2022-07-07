using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonAddress
  {
    [Key]
    public int AddressId { get; set; }
    public int PersonId { get; set; }
    [Required]
    [StringLength(50)]
    public string AddressLabel { get; set; }
    [Required]
    [StringLength(500)]
    public string AddressText { get; set; }

    [ForeignKey(nameof(PersonId))]
    [InverseProperty("PersonAddress")]
    public virtual Person Person { get; set; }
  }
}
