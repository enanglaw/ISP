using ISPoliceAppApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.DTOs
{
    public class LeaderGridDTO
    {
        public List<LeaderCreationDTO> leaderDetailedformation { get; set; }
        public LeadersListDTO  leaderBasicInformation { get; set; }

    }

    public class LeaderCreationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }
        public string GroupName { get; set; }

        public int OrganizationLeaderId { get; set; }
        public int SubOrganizationLeaderId { get; set; }
        public string SubOrganizationName { get; set; }
        public string OrganizationName { get; set; }

        public string LeaderName { get; set; }

        public int LeaderId { get; set; }


        public string Alias { get; set; }

        public string PlaceOfBirth { get; set; }

        public string DateOfBirth { get; set; }
        public string Caste { get; set; }

        public string PermanentAddress { get; set; }

        public string PresentAddress { get; set; }

        public string NativeDistrict { get; set; }

        public string Properties { get; set; }

        public string StrinkingPersonalityTrait { get; set; }
        public string PresentPartyAffiliation { get; set; }
        public string PositionInTheParty { get; set; }
        public int ReligionId { get; set; }
        public string ReligionName { get; set; }
        public int GenderId { get; set; }

        public string GenderName { get; set; }
        public int MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderEventCreationDTO>>))]
        public List<LeaderEventCreationDTO> leaderEvents { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderMediaCreationDTO>>))]
        public List<LeaderMediaCreationDTO> leaderMedia { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderPoliticalBackgroundCreationDTO>>))]
        public List<LeaderPoliticalBackgroundCreationDTO> leaderPoliticalBackgrounds { get; set; }
    }
    public class LeaderDetailInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
        public string Alias { get; set; }
     
        public string PlaceOfBirth { get; set; }
        public string DateOfBirth { get; set; }
        public string Caste { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string NativeDistrict { get; set; }

        public string Properties { get; set; }

        public string StrinkingPersonalityTrait { get; set; }
        public string PresentPartyAffiliation { get; set; }
        public string PositionInTheParty { get; set; }
        public int ReligionId { get; set; }
        public int GenderId { get; set; }
        public int LeaderId { get; set; }
        public int OrganizationLeaderId { get; set; }
        public int SubOrganizationLeaderId { get; set; }
        public int MaritalStatusId { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderEventCreationDTO>>))]
        public List<LeaderEventCreationDTO> leaderEvents { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderMediaCreationDTO>>))]
        public List<LeaderMediaCreationDTO> leaderMedia { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderPoliticalBackgroundCreationDTO>>))]
        public List<LeaderPoliticalBackgroundCreationDTO> leaderPoliticalBackgrounds { get; set; }

    }
    public class LeaderUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }
        public string GroupName { get; set; }
        public string SubOrganizationName { get; set; }
     
        public int OrganizationLeaderId { get; set; }
        public int SubOrganizationLeaderId { get; set; }
        public string OrganizationName { get; set; }

        public string LeaderName { get; set; }

        public int LeaderId { get; set; }


        public string Alias { get; set; }

        public string PlaceOfBirth { get; set; }

        public string DateOfBirth { get; set; }
        public string Caste { get; set; }

        public string PermanentAddress { get; set; }

        public string PresentAddress { get; set; }

        public string NativeDistrict { get; set; }

        public string Properties { get; set; }

        public string StrinkingPersonalityTrait { get; set; }
        public string PresentPartyAffiliation { get; set; }
        public string PositionInTheParty { get; set; }
        public int ReligionId { get; set; }
        public string ReligionName { get; set; }
        public int GenderId { get; set; }

        public string GenderName { get; set; }
        public int MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderEventCreationDTO>>))]
        public List<LeaderEventCreationDTO> leaderEvents { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderMediaCreationDTO>>))]
        public List<LeaderMediaCreationDTO> leaderMedia { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<LeaderPoliticalBackgroundCreationDTO>>))]
        public List<LeaderPoliticalBackgroundCreationDTO> leaderPoliticalBackgrounds { get; set; }

    }
    public class LeaderPoliticalBackgroundCreationDTO
    {
        public DateTime PositionYear { get; set; }
        public string Position { get; set; }
        public int LeaderId { get; set; }
    }
    public class LeaderPoliticalBackgroundUpdateDTO
    {

        public int Id { get; set; }
        public DateTime PositionYear { get; set; }
        public string Position { get; set; }
        public int LeaderId { get; set; }
    }
    public class LeaderMediaCreationDTO
    {
        public string Title { get; set; }
        public int LeaderId { get; set; }
        public string LeaderMediaUrl { get; set; }
    }
    public class LeaderMediaUpdateDTO
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        public int LeaderId { get; set; }
        public string LeaderMediaUrl { get; set; }
    }
    public class LeaderEventCreationDTO
    {

        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public int LeaderId { get; set; }
    }
    public class LeaderEventUpdateDTO
    {
        public int Id { get; set; }
     
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public int LeaderId { get; set; }
    }
}
