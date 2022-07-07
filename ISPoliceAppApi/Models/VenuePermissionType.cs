using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class VenuePermissionType
  {
    public VenuePermissionType()
    {
    }

    [Key]
    public int VenuePermissionTypeId { get; set; }
    [Required]
    [StringLength(100)]
    public string VenuePermissionTypeName { get; set; }
    public int VenueId { get; set; }
    [Required]
    public bool? IsActive { get; set; }
    public int? CreatedBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime UpdatedOn { get; set; }

    [ForeignKey(nameof(VenueId))]
    [InverseProperty("VenuePermissionType")]
    public virtual Venue Venue { get; set; }
  }
}