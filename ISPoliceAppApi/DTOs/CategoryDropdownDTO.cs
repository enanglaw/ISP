using System;
using System.Collections.Generic;

namespace ISPoliceAppApi.DTOs
{
  public class CategoryDropdownDTO
  {
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public List<SubCategoryDropdownDTO> SubCategory { get; set; }
  }

  public class SubCategoryDropdownDTO
  {
    public int SubCategoryId { get; set; }
    public string SubCategoryName { get; set; }
  }
}