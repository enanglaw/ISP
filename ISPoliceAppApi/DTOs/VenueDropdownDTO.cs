using System;
using System.Collections.Generic;

namespace ISPoliceAppApi.DTOs
{
  public class VenueDropdownDTO
  {
    public int VenueId { get; set; }
    public string VenueName { get; set; }

    public List<VenuePermissionTypeDropdownDTO> VenuePermissionType { get; set; }
  }

  public class VenueGridDTO
  {
    public int VenueId { get; set; }
    public string VenueName { get; set; }
    public bool IsActive { get; set; }
    public List<string> VenuePermissionTypes { get; set; }
    public List<VenuePermissionTypeDropdownDTO> VenuePermissionType { get; set; }
  }

  public class VenuePermissionTypeGridDTO
  {
    public int VenuePermissionTypeId { get; set; }
    public string VenuePermissionTypeName { get; set; }
    public string VenueName { get; set; }
    public bool IsActive { get; set; }
  }

  public class VenuePermissionTypeCreationDTO
  {
    public int VenuePermissionTypeId { get; set; }
    public string VenuePermissionTypeName { get; set; }
    public int VenueId { get; set; }
    public bool IsActive { get; set; }
  }

  public class VenuePermissionTypeDropdownDTO
  {
    public int VenuePermissionTypeId { get; set; }
    public string VenuePermissionTypeName { get; set; }
  }

  public class VenueCreationDTO
  {
    public int VenueId { get; set; }
    public string VenueName { get; set; }
    public bool IsActive { get; set; }
  }
}