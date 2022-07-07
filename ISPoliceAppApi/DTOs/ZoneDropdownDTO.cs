using System;
using System.Collections.Generic;

namespace ISPoliceAppApi.DTOs
{
  public class ZoneDropdownDTO
  {
    public int ZoneId { get; set; }
    public string Zone { get; set; }

    public List<DistrictDropdownDTO> District { get; set; }
  }

  public class DistrictDropdownDTO
  {
    public int DistrictId { get; set; }
    public string District { get; set; }
    public List<StationDropdownDTO> Station { get; set; }
  }

  public class StationDropdownDTO
  {
    public int Stationid { get; set; }
    public string StationName { get; set; }
  }
}