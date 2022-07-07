using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class PersonStatusMaster
  {
    public PersonStatusMaster()
    {
      Person = new HashSet<Person>();
    }

    [Key]
    public int StatusId { get; set; }
    [Required]
    [StringLength(50)]
    public string StatusName { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Person> Person { get; set; }
  }
}
