using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonCaseHistory
  {
    [Key]
    public int CaseHistoryId { get; set; }
    public int PersonId { get; set; }
    public int CaseId { get; set; }
    public int CaseStatusId { get; set; }

    [ForeignKey(nameof(PersonId))]
    [InverseProperty("PersonCaseHistory")]
    public virtual Person Person { get; set; }
  }
}
