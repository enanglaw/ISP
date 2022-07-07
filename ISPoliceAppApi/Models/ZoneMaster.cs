using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class ZoneMaster
  {
    public ZoneMaster()
    {
      DistrictMaster = new HashSet<DistrictMaster>();
    }

    [Key]
    public int ZoneId { get; set; }
    [Required]
    [StringLength(50)]
    public string Zone { get; set; }
    [Required]
    public bool? IsActive { get; set; }

    [InverseProperty("Zone")]
    public virtual ICollection<DistrictMaster> DistrictMaster { get; set; }
  }
}
