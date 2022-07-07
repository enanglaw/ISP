using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class SubCategoryMaster
  {
    public SubCategoryMaster()
    {
    }

    [Key]
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    [Required]
    [StringLength(50)]
    public string SubCategoryName { get; set; }
    [Required]
    public bool? IsActive { get; set; }

    [ForeignKey(nameof(CategoryId))]
    [InverseProperty(nameof(CategoryMaster.SubCategoryMaster))]
    public virtual CategoryMaster Category { get; set; }
  }
}
