using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISPoliceAppApi.DSR;
using ISPoliceAppApi.DTOs;
using ISPoliceAppApi.Models;

namespace ISPoliceAppApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        private readonly IFileStorageService _fileStorageService;
        public AutoMapperProfiles(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            CreateMap<VenueCreationDTO, Venue>();
            CreateMap<Venue, VenueDropdownDTO>()
              .ForMember(x => x.VenuePermissionType, options => options.MapFrom(MapVenuePermissionType));
            CreateMap<Venue, VenueGridDTO>()
              .ForMember(x => x.VenuePermissionType, options => options.MapFrom(MapVenuePermissionType))
              .ForMember(x => x.VenuePermissionTypes, options => options.MapFrom(MapVenuePermissionTypes));

            CreateMap<VenuePermissionType, VenuePermissionTypeGridDTO>()
              .ForMember(x => x.VenueName, options => options.MapFrom(MapVenuePermissionTypeVenue));

            CreateMap<VenuePermissionTypeCreationDTO, VenuePermissionType>();

            CreateMap<CategoryMaster, CategoryDropdownDTO>()
              .ForMember(x => x.SubCategory, options => options.MapFrom(MapSubCategoryType));

            CreateMap<ZoneMaster, ZoneDropdownDTO>()
              .ForMember(x => x.District, options => options.MapFrom(MapDistrictType));

            CreateMap<ControlRoomDSR, ControlRoomDSRModel>();

            CreateMap<PersonCreationDTO, Person>()
                .ForMember(x => x.PhotoPath, options => options.Ignore())
                .ForMember(x => x.PhotoUrl, options => options.Ignore())
                .ForMember(x => x.PersonAddress, options => options.MapFrom(MapPersonAddress))
                .ForMember(x => x.PersonAliasName, options => options.MapFrom(MapPersonAliasName))
                .ForMember(x => x.PersonCaseHistory, options => options.MapFrom(MapPersonCaseHistory))
                .ForMember(x => x.PersonMedia, options => options.Ignore())
                .ForMember(x => x.PersonPersonType, options => options.MapFrom(MapPersonPersonType))
                .ForMember(x => x.PersonRivalGang, options => options.MapFrom(MapPersonRivalGang));

            CreateMap<Person, PersonDropdownDTO>();
            CreateMap<Person, PersonGridViewDTO>()
              .ForMember(x => x.PersonAliasNames, options => options.MapFrom(MapPersonAliasName))
              .ForMember(x => x.Gang, options => options.MapFrom(MapPersonGang))
              .ForMember(x => x.GangMemberType, options => options.MapFrom(src => (src.GangMemberType == 1 ? "Leader" : (src.GangMemberType == 2 ? "Prominant" : "Others"))))
              .ForMember(x => x.Status, options => options.MapFrom(src => src.Status != null ? src.Status.StatusName : ""));
            CreateMap<PersonTypeMaster, PersonTypeDTO>();
            CreateMap<PersonStatusMaster, PersonStatusDTO>();
            CreateMap<ProfileAssociates, ProfileAssociatesModel>().ForMember(x=>x.Name,options=> options.MapFrom(src => src.PrimaryProfileDetail.Name));
            CreateMap<ProfileMaster, ProfileMasterModel>();
            CreateMap<ProfileMasterModel, ProfileMaster>();
            CreateMap<ProfileMasterModel, ProfileMaster>();
            CreateMap<LeadersGroupCreationDTO, LeadersGroup>();
            CreateMap<LeadersGroup, LeadersGroupCreationDTO>();
            CreateMap<LeadersGroupUpdateDTO, LeadersGroup>();

            CreateMap<OrganizationLeaderCreationDTO, LeadersGroupModel>();
            CreateMap<LeadersGroupModel, OrganizationLeaderCreationDTO>();

            CreateMap<OrganizationLeaderCreationDTO, LeadersGroupModel>();
            CreateMap<LeadersGroupModel, OrganizationLeaderCreationDTO>();


            CreateMap<OrganizationLeaderCreationDTO, SubLeadersGroup>();
            CreateMap<SubLeadersGroup, OrganizationLeaderCreationDTO>();

            CreateMap<OrganizationLeaderCreationDTO, LeadersGroup>();
            CreateMap<LeadersGroup, OrganizationLeaderCreationDTO>();


            CreateMap<OrganizationLeaderCreationDTO, SubOrganizationLeaders>();
            CreateMap<SubOrganizationLeaders, OrganizationLeaderCreationDTO>();



            CreateMap<LeaderModelDTO, SubOrganizationLeaders>();
            CreateMap<SubOrganizationLeaders, LeaderModelDTO>();


            CreateMap<LeaderModelDTO, LeadersGroup>();
            CreateMap<LeadersGroup, LeaderModelDTO>();


            CreateMap<LeadersGroup, LeadersGroupUpdateDTO>();
            CreateMap<LeadersGroupViewDTO, LeadersGroup>();
            CreateMap<LeadersGroup, LeadersGroupViewDTO>();
            CreateMap<AllegationCreationDTO, Allegation>();
            CreateMap<Allegation, AllegationCreationDTO>();
            CreateMap<AllegationUpdateDTO, Allegation>();
            CreateMap<Allegation, AllegationUpdateDTO>();
            CreateMap<PlanCreationDTO, Plan>().ForMember(m=>m.PlanUrl,m=>m.Ignore());
            CreateMap<Plan, PlanCreationDTO>();
            CreateMap<PlanDownLoadDTO, Plan>().ForMember(m => m.PlanUrl, m => m.Ignore());
            CreateMap<Plan, PlanDownLoadDTO>();
            CreateMap<Religion, GlobalCreationDTO>();
            CreateMap<GlobalCreationDTO, Religion>();
            CreateMap<MaritalStatus, GlobalCreationDTO>();
            CreateMap<GlobalCreationDTO, MaritalStatus>();
            CreateMap<Gender, GlobalCreationDTO>();
            CreateMap<GlobalCreationDTO, Gender>();

            CreateMap<OrganizationViewList, Organization>()
                .ForMember(m => m.SubOrganizationCategory, m => m.MapFrom(MapSubOrganization));
            CreateMap<Organization, OrganizationViewList>();
            CreateMap<OrganizationCreationDTO, Organization>()
                .ForMember(m => m.SymbolPath, m => m.Ignore())
                .ForMember(m => m.SymbolUrl, m => m.Ignore())
                .ForMember(m => m.FlagPath, m => m.Ignore())
                .ForMember(m => m.FlagUrl, m => m.Ignore())
                .ForMember(m => m.SubOrganizationCategory, m => m.MapFrom(MapSubOrganizationCategory))
                 .ForMember(m => m.OrganizationEvent, m => m.MapFrom(MapOrganizationEvent))
                  .ForMember(m => m.OrganizationMedia, m => m.MapFrom(MapOrganizationMedia));
            CreateMap<OrganizationUpdateDTO, Organization>();
            CreateMap<Organization, OrganizationUpdateDTO>();


            CreateMap<OrganizationEventCreationDTO, Events>();
            CreateMap<Events, OrganizationEventCreationDTO>();


            CreateMap<LeaderUpdateDTO, Leader>()
                 .ForMember(m => m.LeaderPoliticalBackgrounds, m => m.MapFrom(MapLeaderPoliticalBackgroundUpdate))
                 .ForMember(m => m.LeaderEvents, m => m.MapFrom(MapLeaderEventUpdate))
                  .ForMember(m => m.LeaderMedia, m => m.MapFrom(MapLeaderMediaUpdate));

            CreateMap<Leader, LeaderUpdateDTO>();


            CreateMap<LeaderDetailInfo, LeaderUpdateDTO>()
               .ForMember(m => m.leaderPoliticalBackgrounds, m => m.MapFrom(MapLeaderDetailPoliticalBackgroundUpdate))
               .ForMember(m => m.leaderEvents, m => m.MapFrom(MapLeaderDetailEventUpdate))
               .ForMember(m => m.leaderMedia, m => m.MapFrom(MapLeaderDetailMediaUpdate));
            CreateMap<LeaderUpdateDTO, LeaderDetailInfo>();



            CreateMap<LeaderDetailInfo, Leader>()
               .ForMember(m => m.LeaderPoliticalBackgrounds, m => m.MapFrom(MapLeaderDetailInfoPoliticalBackgroundUpdate))
               .ForMember(m => m.LeaderEvents, m => m.MapFrom(MapLeaderDetailInfoEventUpdate))
               .ForMember(m => m.LeaderMedia, m => m.MapFrom(MapLeaderDetailInfoMediaUpdate));
            CreateMap<Leader, LeaderDetailInfo>();

           

            CreateMap<Organization, OrganizationCreationDTO>();
            CreateMap<OrganizationEventCreationDTO, OrganizationEvent>();
            CreateMap<OrganizationEventModel, OrganizationCreationDTO>();
            CreateMap<OrganizationEventCreationDTO, OrganizationEventModel>();

            CreateMap<SubOrganizationEvent, OrganizationCreationDTO>();
            CreateMap<OrganizationEventCreationDTO, SubOrganizationEvent>();

            
           CreateMap<SubOrganizationCategoryCreationDTO, SubOrganizationCategory>();
            CreateMap<SubOrganizationCategory, SubOrganizationCategoryCreationDTO>();
            CreateMap<SubOrganizationCategoryUpdateDTO, SubOrganizationCategory>();
            CreateMap<SubOrganizationCategory, SubOrganizationCategoryUpdateDTO>();


            CreateMap<SubOrgLeaderDetailDTO, Leader>();
            CreateMap<Leader, SubOrgLeaderDetailDTO>();

            CreateMap<SubOrgLeaderDetailDTO, Leader>();
            CreateMap<Leader, SubOrgLeaderDetailDTO>();

            
   

            CreateMap<OrganizationEvent, OrganizationEventCreationDTO>();
            CreateMap<OrganizationMediaCreationDTO, OrganizationMedia>();
            CreateMap<OrganizationMedia, OrganizationMediaCreationDTO>();
            CreateMap<OrganizationDTO, Organization>();
            CreateMap<Organization, OrganizationDTO>();

            CreateMap<OrganizationMediaUpdateDTO, OrganizationMedia>();
            CreateMap<OrganizationMedia, OrganizationMediaUpdateDTO>();

            CreateMap<OrganizationAndSubOrganizationDropdown, Organization>()
               .ForMember(m => m.SymbolPath, m => m.Ignore())
               .ForMember(m => m.SymbolUrl, m => m.Ignore())
               .ForMember(m => m.FlagPath, m => m.Ignore())
               .ForMember(m => m.FlagUrl, m => m.Ignore())
               .ForMember(m => m.SubOrganizationCategory, m => m.MapFrom(MapOrganizationAndSubOrganizationDropDown));
            CreateMap<Organization, OrganizationAndSubOrganizationDropdown>();

            CreateMap<SubOrganizationListDropdownDTO, Organization>()
               .ForMember(m => m.SymbolPath, m => m.Ignore())
               .ForMember(m => m.SymbolUrl, m => m.Ignore())
               .ForMember(m => m.FlagPath, m => m.Ignore())
               .ForMember(m => m.FlagUrl, m => m.Ignore())
               .ForMember(m => m.SubOrganizationCategory, m => m.MapFrom(MapSubOrganizationCategoryEvents));
            CreateMap<Organization, SubOrganizationListDropdownDTO>();



            CreateMap<SubOrganizationListDropdownDTO, Organization>()
                .ForMember(m => m.SymbolPath, m => m.Ignore())
                .ForMember(m => m.SymbolUrl, m => m.Ignore())
                .ForMember(m => m.FlagPath, m => m.Ignore())
                .ForMember(m => m.FlagUrl, m => m.Ignore())
                .ForMember(m => m.SubOrganizationCategory, m => m.MapFrom(MapSubOrganizationCategoryEvents));
            CreateMap<Organization, SubOrganizationListDropdownDTO>();




            CreateMap<AllegationDropdown, Allegation>();
            CreateMap<Allegation, AllegationDropdown>();

            CreateMap<Organization, OrganizationDropdownDTO>();
            CreateMap<Organization, OrganizationViewDTO>();
            CreateMap<PersonnelCreationDTO, Personnel>()
                .ForMember(m => m.PersonnelPostings, m => m.MapFrom(MapPersonnelPosting))
                .ForMember(m => m.PersonnelPreviousAllegations, m => m.MapFrom(MapPersonnelPreviousAllegation))
                .ForMember(m => m.PersonnelWarningOrPunishments, m => m.MapFrom(MapPersonnelWarningOrPunishment))
                .ForMember(m => m.PersonnelGallantryAwards, m => m.MapFrom(MapPersonnelGallantryAward))
                .ForMember(m => m.PersonnelCaseDetails, m => m.MapFrom(MapCaseDetails))
                .ForMember(m => m.PersonnelEducationalBackgrounds, m => m.MapFrom(MapEducationalBackgrounds));
             
            CreateMap<Personnel, PersonnelCreationDTO>();
            CreateMap<PersonnelUpdateDTO, Personnel>();
            CreateMap<Personnel, PersonnelUpdateDTO>();
            CreateMap<PersonnelWarningOrPunishmentCreationDTO,PersonnelWarningOrPunishment>();
            CreateMap<PersonnelWarningOrPunishment, PersonnelWarningOrPunishmentCreationDTO>();
            CreateMap<PersonnelAllegationEnquiryUpdateDTO, PersonnelWarningOrPunishment>();
            CreateMap<PersonnelWarningOrPunishment, PersonnelAllegationEnquiryUpdateDTO>();

            CreateMap<PersonnelPreviousAllegationCreationDTO, PersonnelPreviousAllegation>();
            CreateMap<PersonnelPreviousAllegation, PersonnelPreviousAllegationCreationDTO>();
            CreateMap<PersonnelPreviousAllegationUpdateDTO, PersonnelPreviousAllegation>();
            CreateMap<PersonnelPreviousAllegation, PersonnelPreviousAllegationUpdateDTO>();


            CreateMap<PersonnelPostingCreationDTO, PersonnelPosting>();
            CreateMap<PersonnelPosting, PersonnelPostingCreationDTO>();
            CreateMap<PersonnelPostingUpdateDTO, PersonnelPosting>();
            CreateMap<PersonnelPosting, PersonnelPostingUpdateDTO>();


            CreateMap<PersonnelFamilyCreationDTO, PersonnelFamily>();
            CreateMap<PersonnelFamily, PersonnelFamilyCreationDTO>();
            CreateMap<PersonnelFamilyUpdateDTO, PersonnelFamily>();
            CreateMap<PersonnelFamily, PersonnelFamilyUpdateDTO>();


            CreateMap<PersonnelGallantryAwardCreationDTO, PersonnelGallantryAward>();
            CreateMap<PersonnelGallantryAward, PersonnelGallantryAwardCreationDTO>();
            CreateMap<PersonnelGallantryAwardUpdateDTO, PersonnelGallantryAward>();
            CreateMap<PersonnelGallantryAward, PersonnelGallantryAwardUpdateDTO>();

            CreateMap<PersonnelEducationBackgroundCreationDTO,PersonnelEducationalBackground>();
            CreateMap<PersonnelEducationalBackground, PersonnelEducationBackgroundCreationDTO>();
            CreateMap<PersonnelEducationBackgroundUpdateDTO, PersonnelEducationalBackground>();
            CreateMap<PersonnelEducationalBackground, PersonnelEducationBackgroundUpdateDTO>();

            CreateMap<PersonnelCaseDetailCreationDTO, PersonnelCaseDetail>();
            CreateMap<PersonnelCaseDetail, PersonnelCaseDetailCreationDTO>();
            CreateMap<PersonnelCaseDetailUpdateDTO, PersonnelCaseDetail>();
            CreateMap<PersonnelCaseDetail, PersonnelCaseDetailUpdateDTO>();

            CreateMap<PersonnelAllegationEnquiryCreationDTO, PersonnelAllegationEnquiry>();
            CreateMap<PersonnelAllegationEnquiry, PersonnelAllegationEnquiryCreationDTO>();
            CreateMap<PersonnelAllegationEnquiryUpdateDTO, PersonnelAllegationEnquiry>();
            CreateMap<PersonnelAllegationEnquiry, PersonnelAllegationEnquiryUpdateDTO>();

            CreateMap<LeaderCreationDTO, Leader>()
                .ForMember(m => m.LeaderEvents, m => m.MapFrom(MapLeaderEvent))
                .ForMember(m => m.LeaderMedia, m => m.MapFrom(MapLeaderMedia))
                .ForMember(m => m.LeaderPoliticalBackgrounds, m => m.MapFrom(MapLeaderPoliticalBackground));

            CreateMap<Leader, LeaderCreationDTO>();

          /*  CreateMap<LeaderUpdateDTO, Leader>();
            CreateMap<Leader, LeaderUpdateDTO>();*/

            CreateMap<LeaderMediaCreationDTO, LeaderMedia>();
            CreateMap<LeaderMedia, LeaderMediaCreationDTO>();
            CreateMap<LeaderMediaUpdateDTO, LeaderMedia>();
            CreateMap<LeaderMedia, LeaderMediaUpdateDTO>();

            CreateMap<LeaderEventCreationDTO, LeaderEvent>();
            CreateMap<LeaderEvent, LeaderEventCreationDTO>();
            CreateMap<LeaderEventUpdateDTO, LeaderEvent>();
            CreateMap<LeaderEvent, LeaderEventUpdateDTO>();

            CreateMap<LeaderPoliticalBackgroundCreationDTO, LeaderPoliticalBackground>();
            CreateMap<LeaderPoliticalBackground, LeaderPoliticalBackgroundCreationDTO>();
            CreateMap<LeaderPoliticalBackgroundUpdateDTO, LeaderPoliticalBackground>();
            CreateMap<LeaderPoliticalBackground, LeaderPoliticalBackgroundUpdateDTO>();
        }

      

        private List<SubCategoryDropdownDTO> MapSubCategoryType(CategoryMaster category, CategoryDropdownDTO categoryDropdownDTO)
        {
            var result = new List<SubCategoryDropdownDTO>();
            if (category == null) { return result; }

            foreach (var subCat in category.SubCategoryMaster)
            {
                result.Add(new SubCategoryDropdownDTO() { SubCategoryId = subCat.SubCategoryId, SubCategoryName = subCat.SubCategoryName });
            }

            return result;
        }

        private List<DistrictDropdownDTO> MapDistrictType(ZoneMaster zone, ZoneDropdownDTO zoneDropdownDTO)
        {
            var result = new List<DistrictDropdownDTO>();
            if (zone == null) { return result; }

            foreach (var distType in zone.DistrictMaster)
            {
                var stationsList = new List<StationDropdownDTO>();
                if (distType.StationMaster.Count > 0)
                {
                    foreach (var st in distType.StationMaster)
                    {
                        stationsList.Add(new StationDropdownDTO() { Stationid = st.StationId, StationName = st.StationName });
                    }
                }
                var dist = new DistrictDropdownDTO() { DistrictId = distType.DistrictId, District = distType.District, Station = stationsList };
                result.Add(dist);
            }

            return result;
        }

        private List<VenuePermissionTypeDropdownDTO> MapVenuePermissionType(Venue venue, VenueDropdownDTO venueDropdownDTO)
        {
            var result = new List<VenuePermissionTypeDropdownDTO>();
            if (venue == null) { return result; }

            foreach (var vpType in venue.VenuePermissionType)
            {
                result.Add(new VenuePermissionTypeDropdownDTO() { VenuePermissionTypeId = vpType.VenuePermissionTypeId, VenuePermissionTypeName = vpType.VenuePermissionTypeName });
            }

            return result;
        }

        private List<VenuePermissionTypeDropdownDTO> MapVenuePermissionType(Venue venue, VenueGridDTO venueGridDTO)
        {
            var result = new List<VenuePermissionTypeDropdownDTO>();
            if (venue == null) { return result; }

            foreach (var vpType in venue.VenuePermissionType)
            {
                result.Add(new VenuePermissionTypeDropdownDTO() { VenuePermissionTypeId = vpType.VenuePermissionTypeId, VenuePermissionTypeName = vpType.VenuePermissionTypeName });
            }
            return result;
        }

        private List<string> MapVenuePermissionTypes(Venue venue, VenueGridDTO venueGridDTO)
        {
            var result = new List<string>();
            if (venue == null) { return result; }

            foreach (var vpType in venue.VenuePermissionType)
            {
                result.Add(vpType.VenuePermissionTypeName);
            }
            return result;
        }

        private string MapVenuePermissionTypeVenue(VenuePermissionType vpType, VenuePermissionTypeGridDTO venuePermissionTypeGridDTO)
        {
            if (vpType.Venue == null) { return ""; }
            return vpType.Venue.VenueName;
        }

        private List<string> MapPersonAliasName(Person person, PersonGridViewDTO personGrid)
        {
            var result = new List<string>();
            if (person.PersonAliasName == null) return result;
            foreach (var name in person.PersonAliasName)
            {
                result.Add(name.AliasName);
            }
            return result;
        }

        private string MapPersonGang(Person person, PersonGridViewDTO personGrid)
        {
            var result = "";
            // if (person.Gang == null) return result;
            // result = person.Gang.GangName;
            return result;
        }

        #region Create Person
        private List<PersonAddress> MapPersonAddress(PersonCreationDTO personDTO, Person person)
        {
            try
            {
                var result = new List<PersonAddress>();
                if (personDTO.PersonAddress == null) { return result; }

                foreach (var address in personDTO.PersonAddress)
                {
                    result.Add(new PersonAddress() { AddressLabel = address.AddressLabel, AddressText = address.AddressText });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting Address" + ex.Message);
                throw ex;
            }
        }

        private List<PersonAliasName> MapPersonAliasName(PersonCreationDTO personDTO, Person person)
        {
            try
            {
                var result = new List<PersonAliasName>();
                if (personDTO.PersonAliasName == null) { return result; }

                foreach (var aliasName in personDTO.PersonAliasName)
                {
                    result.Add(new PersonAliasName() { AliasName = aliasName });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting AliasName" + ex.Message);
                throw ex;
            }
        }

        private List<PersonCaseHistory> MapPersonCaseHistory(PersonCreationDTO personDTO, Person person)
        {
            try
            {
                var result = new List<PersonCaseHistory>();
                if (personDTO.PersonCaseHistory == null) { return result; }

                foreach (var caseHistory in personDTO.PersonCaseHistory)
                {
                    result.Add(new PersonCaseHistory() { CaseId = caseHistory.CaseId, CaseStatusId = caseHistory.CaseStatusId });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting Case History" + ex.Message);
                throw ex;
            }
        }

        private List<PersonPersonType> MapPersonPersonType(PersonCreationDTO personDTO, Person person)
        {
            try
            {
                var result = new List<PersonPersonType>();
                if (personDTO.PersonPersonType == null) { return result; }

                foreach (var typeId in personDTO.PersonPersonType)
                {
                    result.Add(new PersonPersonType() { PersonTypeId = typeId });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting Offender Type" + ex.Message);
                throw ex;
            }
        }

        private List<PersonRivalGang> MapPersonRivalGang(PersonCreationDTO personDTO, Person person)
        {
            var result = new List<PersonRivalGang>();
            try
            {
                if (personDTO.PersonRivalGang == null) { return result; }

                foreach (var address in personDTO.PersonRivalGang)
                {
                    result.Add(new PersonRivalGang() { GangId = address.RivalGangId });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting Rival Gang" + ex.Message);
                throw ex;
            }
        }
        #endregion

        #region leader

        private List<LeaderEvent> MapLeaderDetailEventUpdate(LeaderDetailInfo leaderCreationDTO, LeaderUpdateDTO leader)
        {
            var result = new List<LeaderEvent>();
            try
            {
                if (leaderCreationDTO.leaderEvents == null) return result;
                foreach (var leaderEvent in leaderCreationDTO.leaderEvents)
                {
                    result.Add(new LeaderEvent()
                    {
                        Title = leaderEvent.Title,
                        Description = leaderEvent.Description,
                        EventDate = leaderEvent.EventDate,
                        LeaderId = leaderEvent.LeaderId
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting leader Event " + ex.Message);
                throw ex;

            }
        }
        private List<LeaderMedia> MapLeaderDetailMediaUpdate(LeaderDetailInfo leaderCreationDTO, LeaderUpdateDTO leader)
        {
            var result = new List<LeaderMedia>();
            try
            {
                if (leaderCreationDTO.leaderMedia == null) return result;
                foreach (var leaderMedia in leaderCreationDTO.leaderMedia)
                {
                    result.Add(new LeaderMedia()
                    {
                        Title = leaderMedia.Title,
                        LeaderMediaUrl = leaderMedia.LeaderMediaUrl,
                        LeaderId = leaderMedia.LeaderId
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Leader Media " + ex.Message);
                throw ex;

            }
        }
        private List<LeaderPoliticalBackground> MapLeaderDetailInfoPoliticalBackgroundUpdate(LeaderDetailInfo leaderCreationDTO, Leader leader)
        {
            var result = new List<LeaderPoliticalBackground>();
            try
            {
                if (leaderCreationDTO.leaderPoliticalBackgrounds == null) return result;
                foreach (var leaderPoliticalBackground in leaderCreationDTO.leaderPoliticalBackgrounds)
                {
                    result.Add(new LeaderPoliticalBackground()
                    {
                        Position = leaderPoliticalBackground.Position,
                        PositionYear = leaderPoliticalBackground.PositionYear,
                        LeaderId = leaderPoliticalBackground.LeaderId,

                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Leader Media " + ex.Message);
                throw ex;

            }
        }



        private List<LeaderEvent> MapLeaderDetailInfoEventUpdate(LeaderDetailInfo leaderCreationDTO, Leader leader)
        {
            var result = new List<LeaderEvent>();
            try
            {
                if (leaderCreationDTO.leaderEvents == null) return result;
                foreach (var leaderEvent in leaderCreationDTO.leaderEvents)
                {
                    result.Add(new LeaderEvent()
                    {
                        Title = leaderEvent.Title,
                        Description = leaderEvent.Description,
                        EventDate = leaderEvent.EventDate,
                        LeaderId=leaderEvent.LeaderId
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting leader Event " + ex.Message);
                throw ex;

            }
        }
        private List<LeaderMedia> MapLeaderDetailInfoMediaUpdate(LeaderDetailInfo leaderCreationDTO, Leader leader)
        {
            var result = new List<LeaderMedia>();
            try
            {
                if (leaderCreationDTO.leaderMedia == null) return result;
                foreach (var leaderMedia in leaderCreationDTO.leaderMedia)
                {
                    result.Add(new LeaderMedia()
                    {
                        Title = leaderMedia.Title,
                        LeaderMediaUrl = leaderMedia.LeaderMediaUrl,
                        LeaderId=leaderMedia.LeaderId
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Leader Media " + ex.Message);
                throw ex;

            }
        }
        private List<LeaderPoliticalBackground> MapLeaderDetailPoliticalBackgroundUpdate(LeaderDetailInfo leaderCreationDTO, LeaderUpdateDTO leader)
        {
            var result = new List<LeaderPoliticalBackground>();
            try
            {
                if (leaderCreationDTO.leaderPoliticalBackgrounds == null) return result;
                foreach (var leaderPoliticalBackground in leaderCreationDTO.leaderPoliticalBackgrounds)
                {
                    result.Add(new LeaderPoliticalBackground()
                    {
                        Position = leaderPoliticalBackground.Position,
                        PositionYear = leaderPoliticalBackground.PositionYear,
                        LeaderId = leaderPoliticalBackground.LeaderId,
                        
                    }); 
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Leader Media " + ex.Message);
                throw ex;

            }
        }

        private List<LeaderEvent> MapLeaderEventUpdate(LeaderUpdateDTO leaderCreationDTO, Leader leader)
        {
            var result = new List<LeaderEvent>();
            try
            {
                if (leaderCreationDTO.leaderEvents == null) return result;
                foreach (var leaderEvent in leaderCreationDTO.leaderEvents)
                {
                    result.Add(new LeaderEvent()
                    {
                        Title = leaderEvent.Title,
                        Description = leaderEvent.Description,
                        EventDate = leaderEvent.EventDate,
                        LeaderId=leaderEvent.LeaderId
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting leader Event " + ex.Message);
                throw ex;

            }
        }
        private List<LeaderMedia> MapLeaderMediaUpdate(LeaderUpdateDTO leaderCreationDTO, Leader leader)
        {
            var result = new List<LeaderMedia>();
            try
            {
                if (leaderCreationDTO.leaderMedia == null) return result;
                foreach (var leaderMedia in leaderCreationDTO.leaderMedia)
                {
                    result.Add(new LeaderMedia()
                    {
                        Title = leaderMedia.Title,
                        LeaderMediaUrl = leaderMedia.LeaderMediaUrl,
                        LeaderId = leaderMedia.LeaderId
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Leader Media " + ex.Message);
                throw ex;

            }
        }
        private List<LeaderPoliticalBackground> MapLeaderPoliticalBackgroundUpdate(LeaderUpdateDTO leaderCreationDTO, Leader leader)
        {
            var result = new List<LeaderPoliticalBackground>();
            try
            {
                if (leaderCreationDTO.leaderPoliticalBackgrounds == null) return result;
                foreach (var leaderPoliticalBackground in leaderCreationDTO.leaderPoliticalBackgrounds)
                {
                    result.Add(new LeaderPoliticalBackground()
                    {
                        Position = leaderPoliticalBackground.Position,
                        PositionYear = leaderPoliticalBackground.PositionYear,
                        LeaderId = leaderPoliticalBackground.LeaderId,
                        
                    }); 
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Leader Media " + ex.Message);
                throw ex;

            }
        }


        #endregion
        #region organization
        private List<OrganizationMedia> MapOrganizationMedia(OrganizationCreationDTO organizationCreationDTO, Organization organization)
        {
            var result = new List<OrganizationMedia>();
            try {
                if (organizationCreationDTO.OrganizationMedia == null) { return result; }
                foreach(var media in organizationCreationDTO.OrganizationMedia)
                {
                    result.Add(new OrganizationMedia(){ Name=media.Title});
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Organization Media "+ ex.Message);
                throw ex;

            }
        }
        private List<OrganizationEvent> MapOrganizationEvent(OrganizationCreationDTO organizationCreationDTO, Organization organization)
        {
            var result = new List<OrganizationEvent>();
            try
            {
                if (organizationCreationDTO.OrganizationEvents == null) return result;
                foreach(var organizationEvent in organizationCreationDTO.OrganizationEvents)
                {
                    result.Add(new OrganizationEvent() { Title= organizationEvent.title,
                        Description=organizationEvent.description, 
                         EventDate=organizationEvent.eventDate });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Organization Event " + ex.Message);
                throw ex;

            }
        }
        private List<SubOrganizationCategory> MapSubOrganizationCategory(OrganizationCreationDTO organizationCreationDTO, Organization organization)
        {
            var result = new List<SubOrganizationCategory>();
            try
            {
                if (organizationCreationDTO.SubOrganizationCategory == null) return result;
                foreach (var subCategory in organizationCreationDTO.SubOrganizationCategory)
                {
                    result.Add(new SubOrganizationCategory() { Name = subCategory.Name});
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Organization sub category " + ex.Message);
                throw ex;

            }
        }
        //private List<SubOrganizationCategory> MapSubOrganizationCategories(SubOrganizationListDropdownDTO organizationCreationDTO)
        //{
        //    var result = new List<SubOrganizationCategory>();
        //    try
        //    {
        //        if (organizationCreationDTO.SubOrganizationCategory == null) return result;
        //        foreach (var subCategory in organizationCreationDTO.SubOrganizationCategory)
        //        {
        //            result.Add(new SubOrganizationCategory() { Name = subCategory.SubOrganizationName, Id=subCategory.SubOrganizationId });
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error while coverting Organization sub category " + ex.Message);
        //        throw ex;

        //    }
        //}
        private List<SubOrganizationCategory> MapSubOrganization(OrganizationViewList organizationCreationDTO, Organization organization)
        {
            var result = new List<SubOrganizationCategory>();
            try
            {
                if (organizationCreationDTO.SubOrganizations == null) return result;
                foreach (var subCategory in organizationCreationDTO.SubOrganizations)
                {
                    result.Add(new SubOrganizationCategory() { Name = subCategory.Name });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Organization sub category " + ex.Message);
                throw ex;

            }
           
        
      
        }


        private List<SubOrganizationCategory> MapOrganizationAndSubOrganizationDropDown(OrganizationAndSubOrganizationDropdown organizationCreationDTO, Organization organization)
        {
            var result = new List<SubOrganizationCategory>();
            try
            {
                if (organizationCreationDTO.SubOrganizationCategory == null) return result;
                foreach (var subCategory in organizationCreationDTO.SubOrganizationCategory)
                {
                    result.Add(new SubOrganizationCategory()
                    {
                        Name = subCategory.Name,
                        Id = subCategory.Id,
                        OrganizationId = subCategory.OrganizationId
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Organization sub category " + ex.Message);
                throw ex;

            }

        }

        private List<SubOrganizationCategory> MapSubOrganizationCategoryEvents(SubOrganizationListDropdownDTO organizationCreationDTO, Organization organization)
        {
            var result = new List<SubOrganizationCategory>();
            try
            {
                if (organizationCreationDTO.SubOrganizations == null) return result;
                foreach (var subCategory in organizationCreationDTO.SubOrganizations)
                {
                    result.Add(new SubOrganizationCategory() { Name = subCategory.Name, Id=subCategory.Id,
                        OrganizationId=subCategory.OrganizationId   });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while coverting Organization sub category " + ex.Message);
                throw ex;

            }



        }
        #endregion
        #region Leaders Group
     

        #endregion

        #region Personnel
        private List<PersonnelCaseDetail> MapCaseDetails(PersonnelCreationDTO personnelCreationDTO, Personnel personnel)
        {
            try
            {
                var result = new List<PersonnelCaseDetail>();
                if (personnelCreationDTO.PersonnelCaseDetails == null) { return result; }

                foreach (var enquiry in personnelCreationDTO.PersonnelCaseDetails)
                {
                    result.Add(new PersonnelCaseDetail()
                    {
                        PersonnelId = enquiry.PersonnelId, 
                        Title=enquiry.Title, 
                        CaseCreatedDate=enquiry.CaseDate, 
                        CaseNumber=enquiry.CaseNumber,
                         CaseSection=enquiry.CaseSection,
                          CurrentStatus=enquiry.CurrentStatus,
                           CreatedBy=enquiry.CreatedBy
                        
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting personel case details" + ex.Message);
                throw ex;
            }
        }

        private List<PersonnelEducationalBackground> MapEducationalBackgrounds(PersonnelCreationDTO personnelCreationDTO, Personnel personnel)
        {
            try
            {
                var result = new List<PersonnelEducationalBackground>();
                if (personnelCreationDTO.PersonnelEducationalBackgrounds == null) { return result; }

                foreach (var personnelEducationBackground in personnelCreationDTO.PersonnelEducationalBackgrounds)
                {
                    result.Add(new PersonnelEducationalBackground()
                    {
                        PersonnelId = personnelEducationBackground.PersonnelId,
                         AdmissionYear= personnelEducationBackground.AdmissionYear,
                         CourseOfStudy= personnelEducationBackground.CourseOfStudy,
                         InstitutionName= personnelEducationBackground.InstitutionName,
                         GraduationYear= personnelEducationBackground.GraduationYear,
                         QualificationName= personnelEducationBackground.QualificationName
                     

                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting personel Educational details" + ex.Message);
                throw ex;
            }
        }
        private List<PersonnelGallantryAward> MapPersonnelGallantryAward(PersonnelCreationDTO personnelCreationDTO, Personnel personnel)
        {
            try
            {
                var result = new List<PersonnelGallantryAward>();
                if (personnelCreationDTO.PersonnelGallantryAwards == null) { return result; }

                foreach (var personnelGallantryAward in personnelCreationDTO.PersonnelGallantryAwards)
                {
                    result.Add(new PersonnelGallantryAward()
                    {
                          
                         PersonnelId= personnelGallantryAward.PersonnelId,
                          Title= personnelGallantryAward.Title,
                           IssuingDate= personnelGallantryAward.IssuingDate,
                            IssueingAuthority= personnelGallantryAward.IssueingAuthority 
                             
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting personel Awards" + ex.Message);
                throw ex;
            }
        }
        private List<PersonnelPosting> MapPersonnelPosting(PersonnelCreationDTO personnelCreationDTO, Personnel personnel)
        {
            try
            {
                var result = new List<PersonnelPosting>();
                if (personnelCreationDTO.PersonnelPostings == null) { return result; }

                foreach (var personnelPosting in personnelCreationDTO.PersonnelPostings)
                {
                    result.Add(new PersonnelPosting()
                    {

                        PersonnelId = personnelPosting.PersonnelId,
                         From= personnelPosting.From,
                         To= personnelPosting.To,
                          Place= personnelPosting.Place,
                           Post= personnelPosting.Post

                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting personel Posting" + ex.Message);
                throw ex;
            }
        }
        private List<PersonnelPreviousAllegation> MapPersonnelPreviousAllegation(PersonnelCreationDTO personnelCreationDTO, Personnel personnel)
        {
            try
            {
                var result = new List<PersonnelPreviousAllegation>();
                if (personnelCreationDTO.PersonnelPreviousAllegations == null) { return result; }

                foreach (var personnelPreviousAllegation in personnelCreationDTO.PersonnelPreviousAllegations)
                {
                    result.Add(new PersonnelPreviousAllegation()
                    {

                        PersonnelId = personnelPreviousAllegation.PersonnelId,
                         Description= personnelPreviousAllegation.Description,
                         Result= personnelPreviousAllegation.Result,
                         

                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting personel previous allegation" + ex.Message);
                throw ex;
            }
        }
        private List<PersonnelWarningOrPunishment> MapPersonnelWarningOrPunishment(PersonnelCreationDTO personnelCreationDTO, Personnel personnel)
        {
            try
            {
                var result = new List<PersonnelWarningOrPunishment>();
                if (personnelCreationDTO.PersonnelWarningOrPunishments == null) { return result; }

                foreach (var personnelWarning in personnelCreationDTO.PersonnelWarningOrPunishments)
                {
                    result.Add(new PersonnelWarningOrPunishment()
                    {

                        PersonnelId = personnelWarning.PersonnelId,
                         Title= personnelWarning.Title


                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting personel previous allegation" + ex.Message);
                throw ex;
            }
        }
       
        #endregion


        #region Leader
        private List<LeaderMedia> MapLeaderMedia(LeaderCreationDTO leaderCreationDTO, Leader leader)
        {
            try
            {
                var result = new List<LeaderMedia>();
                if (leaderCreationDTO.leaderMedia == null) { return result; }

                foreach (var leaderMedia in leaderCreationDTO.leaderMedia)
                {
                    result.Add(new LeaderMedia() {  LeaderId=leaderMedia.LeaderId, Title=leaderMedia.Title });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting leader media" + ex.Message);
                throw ex;
            }
        }
        private List<LeaderEvent> MapLeaderEvent(LeaderCreationDTO leaderCreationDTO, Leader leader)
        {
            try
            {
                var result = new List<LeaderEvent>();
                if (leaderCreationDTO.leaderEvents == null) { return result; }

                foreach (var leaderEvent in leaderCreationDTO.leaderEvents)
                {
                    result.Add(new LeaderEvent() { LeaderId=leaderEvent.LeaderId, Title=leaderEvent.Title,
                        Description=leaderEvent.Description, EventDate=leaderEvent.EventDate});
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting leader event" + ex.Message);
                throw ex;
            }
        }
        private List<LeaderPoliticalBackground> MapLeaderPoliticalBackground(LeaderCreationDTO leaderCreationDTO,Leader leader)
        {
            try
            {
                var result = new List<LeaderPoliticalBackground>();
                if (leaderCreationDTO.leaderPoliticalBackgrounds == null) { return result; }

                foreach (var leaderPoliticalBackground in leaderCreationDTO.leaderPoliticalBackgrounds)
                {
                    result.Add(new LeaderPoliticalBackground()
                    {
                        LeaderId = leaderPoliticalBackground.LeaderId,
                         Position= leaderPoliticalBackground.Position,
                          PositionYear= leaderPoliticalBackground.PositionYear
                      
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while converting leader event" + ex.Message);
                throw ex;
            }
        }
        #endregion Personnel
    }
}
