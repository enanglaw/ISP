using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonMedia
  {
    [Key]
    public int MediaId { get; set; }
    public int PersonId { get; set; }
    [Required]
    [StringLength(50)]
    public string MediaLabel { get; set; }
    [Required]
    [StringLength(256)]
    public string MediaActualName { get; set; }
    [Required]
    [StringLength(50)]
    public string MediaPath { get; set; }
    [Required]
    [StringLength(512)]
    public string MediaUrl { get; set; }

    [ForeignKey(nameof(PersonId))]
    [InverseProperty("PersonMedia")]
    public virtual Person Person { get; set; }
  }
}
