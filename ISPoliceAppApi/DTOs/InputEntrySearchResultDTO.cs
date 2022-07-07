using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.DTOs
{
  public partial class CategoryDTO
  {
    public string Name { get; set; }
  }

  public partial class SubCategoryDTO
  {
    public string Name { get; set; }
  }

  public partial class ZoneDTO
  {
    public string Name { get; set; }
  }

  public partial class DistrictDTO
  {
    public string Name { get; set; }
  }

  public partial class StationDTO
  {
    public string Name { get; set; }
  }
}
