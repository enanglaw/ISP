using System.Collections.Generic;
using ISPoliceAppApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ISPoliceAppApi.DTOs
{
  public partial class PersonCreationDTO
  {
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public string ParentName { get; set; }
    public string PrimaryAddress { get; set; }
    public string HistorySheetNo { get; set; }
    public string CurrentActivity { get; set; }
    public int? StatusId { get; set; }
    public string ModusOperandi { get; set; }
    public int? GangId { get; set; }
    public int GangMemberType { get; set; }
    public IFormFile PhotoDocument { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<string>>))]
    public List<string> PersonAliasName { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<PersonAddressCreationDTO>>))]
    public List<PersonAddressCreationDTO> PersonAddress { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<PersonCaseHistoryCreationDTO>>))]
    public List<PersonCaseHistoryCreationDTO> PersonCaseHistory { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
    public List<int> PersonPersonType { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<PersonRivalGangCreationDTO>>))]
    public List<PersonRivalGangCreationDTO> PersonRivalGang { get; set; }

    [ModelBinder(BinderType = typeof(TypeBinder<List<PersonMediaCreationDTO>>))]
    public List<PersonMediaCreationDTO> PersonMedia { get; set; }
  }

  public partial class PersonAddressCreationDTO
  {
    // public int PersonId { get; set; }
    public string AddressLabel { get; set; }
    public string AddressText { get; set; }
  }

  public partial class PersonCaseHistoryCreationDTO
  {
    // public int PersonId { get; set; }
    public int CaseId { get; set; }
    public int CaseStatusId { get; set; }
  }

  public partial class PersonMediaCreationDTO
  {
    // public int PersonId { get; set; }
    public string MediaLabel { get; set; }
    public IFormFile Media { get; set; }
  }

  public partial class PersonRivalGangCreationDTO
  {
    // public int PersonId { get; set; }
    public int RivalGangId { get; set; }
  }

  public partial class PersonDropdownDTO
  {
    public int PersonId { get; set; }
    public string PersonName { get; set; }
  }

  public partial class PersonGridViewDTO
  {
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public List<string> PersonAliasNames { get; set; }
    public string PrimaryAddress { get; set; }
    public string ParentName { get; set; }
    public string HistorySheetNo { get; set; }
    public string CurrentActivity { get; set; }
    public string Status { get; set; }
    public string ModusOperandi { get; set; }
    public string Gang { get; set; }
    public string GangMemberType { get; set; }
    public string PhotoUrl { get; set; }
  }
}
