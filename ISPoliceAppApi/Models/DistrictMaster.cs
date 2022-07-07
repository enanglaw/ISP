using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class DistrictMaster
  {
    public DistrictMaster()
    {
      StationMaster = new HashSet<StationMaster>();
    }

    [Key]
    public int DistrictId { get; set; }
    public int ZoneId { get; set; }
    [Required]
    [StringLength(50)]
    public string District { get; set; }
    [Required]
    public bool? IsActive { get; set; }

    [ForeignKey(nameof(ZoneId))]
    [InverseProperty(nameof(ZoneMaster.DistrictMaster))]
    public virtual ZoneMaster Zone { get; set; }
    [InverseProperty("District")]
    public virtual ICollection<StationMaster> StationMaster { get; set; }
  }
}
