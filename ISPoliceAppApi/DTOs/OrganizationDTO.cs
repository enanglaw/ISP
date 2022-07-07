using ISPoliceAppApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ISPoliceAppApi.DTOs
{
  public class OrganizationDTO
  {
    public int OrganizationId { get; set; }
    public string Name { get; set; }

        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Ideology { get; set; }
        public string SymbolUrl { get; set; }
        public string FlagUrl { get; set; }
    }

  public class SubOrganizationCategoryDropdownDTO
  {
    public int SubOrganizationCategoryId { get; set; }
    public string Name { get; set; }
  }
  public partial class OrganizationCreationDTO
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Ideology { get; set; }
        public string SymbolUrl { get; set; }
        public string FlagUrl { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<SubOrganizationCategoryCreationDTO>>))]
        public List<SubOrganizationCategoryCreationDTO> SubOrganizationCategory { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<OrganizationEventCreationDTO>>))]
        public List<OrganizationEventCreationDTO> OrganizationEvents { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<OrganizationMediaCreationDTO>>))]
        public List<OrganizationMediaCreationDTO> OrganizationMedia  { get; set; }
    }
    public partial class OrganizationUpdateDTO
    {
        public int OrganizationId { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Ideology { get; set; }
        public string SymbolUrl { get; set; }
        public string FlagUrl { get; set; }
        
        [ModelBinder(BinderType = typeof(TypeBinder<List<SubOrganizationCategoryUpdateDTO>>))]
        public List<SubOrganizationCategoryUpdateDTO> subOrganizationCategory { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<OrganizationEventUpdateDTO>>))]
        public List<OrganizationEventUpdateDTO> organizationEvent { get; set; }
    }
  
    public partial class OrganizationMediaCreationDTO
    {
        public string Title { get; set; }
        public string MediaUrl { get; set; }

    }
    public partial class OrganizationMediaUpdateDTO
    {
        public string Title { get; set; }
        public string MediaUrl { get; set; }
    }

    public class OrganizationViewList
    {
        public OrganizationViewList()
        {
            SubOrganizations = new List<SubOrganizationCategoryCreationDTO>();
        }
        public int OrganizationId { get; set; }
        public string FullName { get; set; }
        public List<SubOrganizationCategoryCreationDTO> SubOrganizations { get; set; }
    }
    public class OrganizationList
    {
        public int OrganizationId { get; set; }
        public string FullName { get; set; }
        public List<SubOrganizationCategoryCreationDTO> SubOrganizations { get; set; }
    }

    public class SubOrganizationList
    {
        public int OrganizationId { get; set; }
        public string FullName { get; set; }
        public List<SubOrganizationCategoryUpdateDTO> SubOrganizations { get; set; }
    }
    public partial class SubOrganizationCategoryCreationDTO
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public partial class SubOrganizationCategoryUpdateDTO
    {
      public int SubOrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrganizaionId { get; set; }
    }
    public partial class OrganizationViewDTO
    {
        public int OrganizationId { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Ideology { get; set; }
        public string SymbolUrl { get; set; }
        public string FlagUrl { get; set; }
    }

    public partial class OrganizationLeaderGroupCreationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Designation { get; set; }
        public string MibileNumber { get; set; }
        public string Address { get; set; }
        public int organizationId { get; set; }
        public int subOrganizationId { get; set; }
    }

    public partial class SubOrganizationLeaderCreationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Designation { get; set; }
        public string MibileNumber { get; set; }
        public string Address { get; set; }
        public int subOrganizationId { get; set; }
    }
    public partial class OrganizationLeaderCreationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Designation { get; set; }
        public string MibileNumber { get; set; }
        public string Address { get; set; }
        public int OrganizationId { get; set; }
    }
    public partial class OrganizationEventCreationDTO
    {
        public string title { get; set; }
        public DateTime eventDate { get; set; }
        public string description { get; set; }
        public int organizationId { get; set; }
        public int subOrganizationId { get; set; }
    }
    public partial class OrganizationEventUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public int OrganizationId { get; set; }
        public int SubOrganizationId { get; set; }
    }

    public partial class OrganizationDropdownDTO
    {
        public int OrganizationId { get; set; }
        public string FullName { get; set; }
    }

    public class OrganizationAndSubOrganizationDropdown
    {
        
        public int OrganizationId { get; set; }
        public string FullName { get; set; }
        public string EventGroupName { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<SubOrganizationCategoryCreationDTO>>))]
        public List<SubOrganizationCategoryCreationDTO> SubOrganizationCategory { get; set; }

    }
    public class SubOrganizationListDropdownDTO
    {

        public int OrganizationId { get; set; }
        public string FullName { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<SubOrganizationCategoryCreationDTO>>))]
        public List<SubOrganizationCategoryCreationDTO> SubOrganizations { get; set; }

         [ModelBinder(BinderType = typeof(TypeBinder<List<OrganizationEventCreationDTO>>))]
         public List<OrganizationEventCreationDTO> OrganizationEvents { get; set; }

         [ModelBinder(BinderType = typeof(TypeBinder<List<OrganizationMediaCreationDTO>>))]
          public List<OrganizationMediaCreationDTO> OrganizationMedia { get; set; }
    }


    public class SubOrganizationModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string SubOrganizationName { get; set; }

        public string OrganizationName { get; set; }
        public string Description { get; set; }
    }
    public class SubOrganizationDropdownDTO
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}