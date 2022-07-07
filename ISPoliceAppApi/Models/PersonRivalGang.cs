using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonRivalGang
  {
    [Key]
    public int RivalGangId { get; set; }
    public int PersonId { get; set; }
    public int GangId { get; set; }

    [ForeignKey(nameof(PersonId))]
    [InverseProperty("PersonRivalGang")]
    public virtual Person Person { get; set; }
  }
}
