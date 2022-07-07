using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class CategoryMaster
  {
    public CategoryMaster()
    {
      SubCategoryMaster = new HashSet<SubCategoryMaster>();
    }

    [Key]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(50)]
    public string CategoryName { get; set; }
    [Required]
    public bool? IsActive { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<SubCategoryMaster> SubCategoryMaster { get; set; }
  }
}
