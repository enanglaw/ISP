using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class Venue
  {
    public Venue()
    {
      VenuePermissionType = new HashSet<VenuePermissionType>();
    }

    [Key]
    public int VenueId { get; set; }
    [Required]
    [StringLength(100)]
    public string VenueName { get; set; }
    [Required]
    public bool? IsActive { get; set; }
    public int? CreatedBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime UpdatedOn { get; set; }

    [InverseProperty("Venue")]
    public virtual ICollection<VenuePermissionType> VenuePermissionType { get; set; }
  }
}
