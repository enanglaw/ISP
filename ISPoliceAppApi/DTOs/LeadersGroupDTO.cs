using ISPoliceAppApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ISPoliceAppApi.DTOs
{
  public class LeadersGroupDTO
  {
    public int OrganizationId { get; set; }
    public string Name { get; set; }
  }

  public class LeaderGroupCategoryDropdownDTO
  {
    public int LeadersGroupCategoryId { get; set; }
    public string Name { get; set; }
  }
  public partial class LeadersGroupCreationDTO
    {
       
       
        public string Name { get; set; }
        
        public string Designation { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<LeadersCategoryCreationDTO>>))]
        public List<LeadersCategoryCreationDTO> LeadersGroupCategory { get; set; }

    }

    public partial class LeadersSubModelDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }
        public int SubOrganizationId { get; set; }
        public int OrganizationId { get; set; }
        public int LeaderId { get; set; }

    }
    
    
    public partial class LeadersListDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string MobileNumber { get; set; }
        public int LeaderId { get; set; }
        public string Address { get; set; }
        public string GroupName { get; set; }
        public int SubOrganizationId { get; set; }
        public int OrganizationId { get; set; }

    }
    public partial class OrgLeaderDetailDTO
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }

    }
    public partial class SubOrgLeaderDetailDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int SubOrganizationId { get; set; }

    }

    public partial class LeadersMainModelDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }
        public int OrganizationId { get; set; }
        public int SubOrganizationId { get; set; }
        public int LeaderId { get; set; }

    }

    public partial class LeaderModelDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }
        public int LeaderId { get; set; }
        public int OrganizationId { get; set; }
        public int SubOrganizationId { get; set; }

    }

    public partial class LeadersGroupUpdateDTO
    {
        public int LeadersGroupId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }

    }

    public partial class LeadersCategoryCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
   
    public partial class LeadersGroupViewDTO
    {

        public string Name { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
    }

}