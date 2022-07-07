using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
    public partial class ProfileMaster
    {
        public ProfileMaster()
        {
            ProfileAlias = new HashSet<ProfileAlias>();
            ProfileAssociates = new HashSet<ProfileAssociates>();
            ProfileSpouses    = new HashSet<ProfileSpouse>();
            ProfileChildrens = new HashSet<ProfileChildren>();
            ProfileSiblings  = new HashSet<ProfileSibling>();
            ProfileAbstracts = new HashSet<ProfileAbstract>();
            CaseDetails = new HashSet<CaseDetail>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hs { get; set; }
        public string Image { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? EntryDate { get; set; }
        public virtual ProfileTransaction ProfileTransaction { get; set; }
        [InverseProperty("ProfileDetail")]
        public virtual ICollection<ProfileAlias> ProfileAlias { get; set; }
        [InverseProperty("PrimaryProfileDetail")]
        public virtual ICollection<ProfileAssociates> ProfileAssociates { get; set; }
        [InverseProperty("AssociatesProfileDetail")]
        public virtual ICollection<ProfileAssociates> AssociatesProfiles { get; set; }
        public virtual ICollection<ProfileSpouse> ProfileSpouses { get; set; }
        public virtual ICollection<ProfileChildren> ProfileChildrens { get; set; }
        public virtual ICollection<ProfileSibling> ProfileSiblings { get; set; }
        public virtual ICollection<ProfileAbstract> ProfileAbstracts { get; set; }
        public virtual ICollection<CaseDetail> CaseDetails { get; set; }
    }
    public partial class ProfileMasterModel
    {
        public ProfileMasterModel()
        {
            ProfileAlias = new HashSet<ProfileAlias>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hs { get; set; }
        public Boolean IsActive { get; set; }
        public virtual ICollection<ProfileAlias> ProfileAlias { get; set; }
    }
    public partial class ProfileTransaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProfileMaster")]
        public int ProfileId { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Category { get; set; }
        public string PermanentAddres { get; set; }
        public string PresentAddress { get; set; }
        public string Photo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public Nullable<int> MartialStatus { get; set; }
        public string SpouseName { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public string NoOfGoondas { get; set; }
        public string SecurityProceeding { get; set; }
        public DateTime? dateOfInitiation { get; set; }
        public string LastAction { get; set; }
        public DateTime? LastActionDate { get; set; }
        public string Bail { get; set; }
        public DateTime? BailDate { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? EntryDate { get; set; }

        public virtual ProfileMaster ProfileMaster { get; set; }



       
    }
    public partial class BaseModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? EntryDate { get; set; }

    }
    public partial class ProfileAlias : BaseModel
    {
        public string Name { get; set; }
        [ForeignKey(nameof(ProfileId))]
        [InverseProperty(nameof(ProfileMaster.ProfileAlias))]
        public virtual ProfileMaster ProfileDetail { get; set; }
    }
    public partial class ProfileSpouse : BaseModel
    {
        public string Name { get; set; }
        [ForeignKey(nameof(ProfileId))]
        [InverseProperty(nameof(ProfileMaster.ProfileSpouses))]
        public virtual ProfileMaster ProfileDetail { get; set; }
    }
    public partial class ProfileChildren : BaseModel
    {
        public string Name { get; set; }
        public Nullable<int> Gender { get; set; }
        [ForeignKey(nameof(ProfileId))]
        [InverseProperty(nameof(ProfileMaster.ProfileChildrens))]
        public virtual ProfileMaster ProfileDetail { get; set; }
    }
    public partial class ProfileSibling : BaseModel
    {
        public string Name { get; set; }
        public string relation { get; set; }
        [ForeignKey(nameof(ProfileId))]
        [InverseProperty(nameof(ProfileMaster.ProfileSiblings))]
        public virtual ProfileMaster ProfileDetail { get; set; }
    }
    public partial class ProfileAssociates : BaseModel
    {
        public int AssociatesId { get; set; }
        
        [ForeignKey(nameof(ProfileId))]
        [InverseProperty(nameof(ProfileMaster.ProfileAssociates))]
        public virtual ProfileMaster PrimaryProfileDetail { get; set; }

        [ForeignKey(nameof(AssociatesId))]
        [InverseProperty(nameof(ProfileMaster.AssociatesProfiles))]
        public virtual ProfileMaster AssociatesProfileDetail { get; set; }
    }
    public partial class ProfileAssociatesModel : BaseModel
    {
        public int AssociatesId { get; set; }
        public string Name { get; set; }

    }
    public partial class ProfileAbstract : BaseModel
    {
        public Nullable<int> DistCity { get; set; }
        public Nullable<int> DistCityId { get; set; }
        
        public string Jurisdiction { get; set; }
        public Nullable<int> Murder { get; set; }
        public Nullable<int> AttmptMurder { get; set; }
        public Nullable<int> Ndps { get; set; }
        public Nullable<int> Robbery { get; set; }
        public Nullable<int> ChainSnatch { get; set; }
        public Nullable<int> MobileSnatch { get; set; }
        public Nullable<int> HbDay { get; set; }
        public Nullable<int> HbNight { get; set; }
        public Nullable<int> OtherCase { get; set; }
        public Nullable<int> TechCase { get; set; }
        public Nullable<int> TotalCase { get; set; }
        [ForeignKey(nameof(ProfileId))]
        [InverseProperty(nameof(ProfileMaster.ProfileAbstracts))]
        public virtual ProfileMaster ProfileDetail { get; set; }

    }
    public partial class CaseDetail : BaseModel
    {
        public int ps { get; set; }
        public string cr { get; set; }
        public string Section { get; set; }
        public string Head { get; set; }
        public string io { get; set; }
        public string Court { get; set; }
        public string Goondas { get; set; }
        public string Stage { get; set; }
        public string Reason { get; set; }
        public string Dsr { get; set; }
        [ForeignKey(nameof(ProfileId))]
        [InverseProperty(nameof(ProfileMaster.CaseDetails))]
        public virtual ProfileMaster ProfileDetail { get; set; }
    }

}

 











