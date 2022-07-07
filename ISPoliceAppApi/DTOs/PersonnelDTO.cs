using ISPoliceAppApi.Helpers;
using ISPoliceAppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.DTOs
{
   
    public class PersonnelCreationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string CurrentRank { get; set; }
        
        public string PersonalNumber { get; set; }
        
        public DateTime DateOffBirth { get; set; }
        public DateTime DateOfEnlistment { get; set; }
       
        public string PresentPosting { get; set; }
       
        public DateTime DateOfJoiningPresentPosting { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public int MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }

        public int ReligionId { get; set; }
        public string ReligionName { get; set; }

        public string  FatherName { get; set; }
        public string MotherName { get; set; }

        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string ContactNumber { get; set; }
      
        public string Email { get; set; }
        public string PersonnelPhotoUrl { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelCaseDetailCreationDTO>>))]
        public List<PersonnelCaseDetailCreationDTO> PersonnelCaseDetails { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelEducationBackgroundCreationDTO>>))]
        public List<PersonnelEducationBackgroundCreationDTO> PersonnelEducationalBackgrounds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelGallantryAwardCreationDTO>>))]
        public List<PersonnelGallantryAwardCreationDTO> PersonnelGallantryAwards { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelPostingCreationDTO>>))]
        public List<PersonnelPostingCreationDTO> PersonnelPostings { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelPreviousAllegationCreationDTO>>))]
        public List<PersonnelPreviousAllegationCreationDTO> PersonnelPreviousAllegations { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelWarningOrPunishmentCreationDTO>>))]
        public List<PersonnelWarningOrPunishmentCreationDTO> PersonnelWarningOrPunishments { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelSpouseCreateDTO>>))]
        public List<PersonnelSpouseCreateDTO> PersonnelSpouses { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<PersonnelChildren>>))]
        public List<PersonnelChildren> PersonnelChildrens { get; set; }


    }



    public class PersonnelUpdateDTO
    {

        public string Name { get; set; }

        public string CurrentRank { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime DateOffBirth { get; set; }
        public DateTime DateOfEnlistment { get; set; }

        public string PresentPosting { get; set; }

        public DateTime DateOfJoiningPresentPosting { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public int MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }

        public int ReligionId { get; set; }
        public string ReligionName { get; set; }


        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string ContactNumber { get; set; }

        public string Email { get; set; }
        public string PersonnelPhotoUrl { get; set; }
    }
    public  class PersonnelAllegationEnquiryCreationDTO
    {
        public string Title { get; set; }
        public int PersonnelId { get; set; }
    }
    public class PersonnelAllegationEnquiryUpdateDTO
    {
        public int Id { get; set; }
       
        public string Title { get; set; }
        public int PersonnelId { get; set; }
    }
    public class PersonnelSpouseCreateDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonnelId { get; set; }
    }
    public class PersonnelChildrenCreateDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonnelId { get; set; }
    }
    public class PersonnelCaseDetailCreationDTO
    {
        public DateTime CaseDate { get; set; }

        public string CaseNumber { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }

        public string CaseSection { get; set; }

        public string CurrentStatus { get; set; }


        public int PersonnelId { get; set; }
    }
    public class PersonnelCaseDetailUpdateDTO
    {
        public int Id { get; set; }
        
        public string CaseNumber { get; set; }
        public string Title { get; set; }
        public string CaseSection { get; set; }
        

        public int PersonnelId { get; set; }
    }
    public class PersonnelEducationBackgroundCreationDTO
    {

        public string InstitutionName { get; set; }
        public string CourseOfStudy { get; set; }
        public string QualificationName { get; set; }
        public DateTime AdmissionYear { get; set; }
        public DateTime GraduationYear { get; set; }

        public int PersonnelId { get; set; }
    }
    public class PersonnelEducationBackgroundUpdateDTO
    {
        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public string CourseOfStudy { get; set; }
        public string QualificationName { get; set; }
        public DateTime AdmissionYear { get; set; }
        public DateTime GraduationYear { get; set; }

        public int PersonnelId { get; set; }

    }
    public class PersonnelFamilyCreationDTO
    {

        public string FatherName { get; set; }

        public string MotherName { get; set; }
        public string? Spouse { get; set; }
        public string ChildFullName { get; set; }
    }
    public class PersonnelFamilyUpdateDTO
    {
        public int Id { get; set; }
        
        public string FatherName { get; set; }
       
        public string MotherName { get; set; }
        public string? Spouse { get; set; }
        public string ChildFullName { get; set; }
    }
    public class PersonnelGallantryAwardCreationDTO
    {

        public string Title { get; set; }
        public string IssueingAuthority { get; set; }
        public DateTime IssuingDate { get; set; }
        public string DocumentUrl { get; set; }
        public int PersonnelId { get; set; }
    }
    public class PersonnelGallantryAwardUpdateDTO
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        public string IssueingAuthority { get; set; }
        public DateTime IssuingDate { get; set; }
        public string DocumentUrl { get; set; }
        public int PersonnelId { get; set; }

    }
    public class PersonnelPostingCreationDTO
    {
      

        public string Post { get; set; }

        public string Place { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int PersonnelId { get; set; }
    }
    public class PersonnelPostingUpdateDTO
    {
        public int Id { get; set; }
       
        public string Post { get; set; }
       
        public string Place { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int PersonnelId { get; set; }

    }
    public class PersonnelPreviousAllegationCreationDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }

        public int PersonnelId { get; set; }

        public string DocumentUrl { get; set; }
    }
    public class PersonnelPreviousAllegationUpdateDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }

        public int PersonnelId { get; set; }

        public string DocumentUrl { get; set; }
    }
    public class PersonnelWarningOrPunishmentCreationDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime WarningDate { get; set; }
        public string DocumentUrl { get; set; }
        public int PersonnelId { get; set; }

    }
    public class PersonnelWarningOrPunishmentUpdateDTO
    {
        public int Id { get; set; }
      
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime WarningDate { get; set; }
        public string DocumentUrl { get; set; }
        public int PersonnelId { get; set; }
    }
}