using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
  public partial class Person
  {
    public Person()
    {
      PersonAddress = new HashSet<PersonAddress>();
      PersonAliasName = new HashSet<PersonAliasName>();
      PersonCaseHistory = new HashSet<PersonCaseHistory>();
      PersonMedia = new HashSet<PersonMedia>();
      PersonPersonType = new HashSet<PersonPersonType>();
      PersonRivalGang = new HashSet<PersonRivalGang>();
    }

    [Key]
    public int PersonId { get; set; }
    [Required]
    [StringLength(50)]
    public string PersonName { get; set; }
    [StringLength(50)]
    public string ParentName { get; set; }
    [StringLength(500)]
    public string PrimaryAddress { get; set; }
    [StringLength(50)]
    public string HistorySheetNo { get; set; }
    public string CurrentActivity { get; set; }
    public int? StatusId { get; set; }
    public string ModusOperandi { get; set; }
    public int? GangId { get; set; }
    public int? GangMemberType { get; set; }
    [StringLength(256)]
    public string PhotoPath { get; set; }
    [StringLength(512)]
    public string PhotoUrl { get; set; }

    [ForeignKey(nameof(StatusId))]
    [InverseProperty(nameof(PersonStatusMaster.Person))]
    public virtual PersonStatusMaster Status { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<PersonAddress> PersonAddress { get; set; }
    [InverseProperty("Person")]
    public virtual ICollection<PersonAliasName> PersonAliasName { get; set; }
    [InverseProperty("Person")]
    public virtual ICollection<PersonCaseHistory> PersonCaseHistory { get; set; }
    [InverseProperty("Person")]
    public virtual ICollection<PersonMedia> PersonMedia { get; set; }
    [InverseProperty("Person")]
    public virtual ICollection<PersonPersonType> PersonPersonType { get; set; }
    [InverseProperty("Person")]
    public virtual ICollection<PersonRivalGang> PersonRivalGang { get; set; }
  }
}
