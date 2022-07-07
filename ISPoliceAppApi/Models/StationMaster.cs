using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class StationMaster
  {
    public StationMaster()
    {
    }

    [Key]
    public int StationId { get; set; }
    public int DistrictId { get; set; }
    [Required]
    [StringLength(50)]
    public string StationName { get; set; }
    [Required]
    public bool? IsActive { get; set; }

    [ForeignKey(nameof(DistrictId))]
    [InverseProperty(nameof(DistrictMaster.StationMaster))]
    public virtual DistrictMaster District { get; set; }
  }
}
