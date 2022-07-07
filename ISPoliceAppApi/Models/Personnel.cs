using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class Personnel
    {
        public Personnel()
        {
            PersonnelCaseDetails = new List<PersonnelCaseDetail>();
            PersonnelGallantryAwards = new List<PersonnelGallantryAward>();
            PersonnelPostings = new List<PersonnelPosting>();
            PersonnelPreviousAllegations = new List<PersonnelPreviousAllegation>();
            PersonnelWarningOrPunishments = new List<PersonnelWarningOrPunishment>();
            PersonnelEducationalBackgrounds = new List<PersonnelEducationalBackground>();
            PersonnelChildrens = new List<PersonnelChildren>();
            PersonnelSpouses = new List<PersonnelSpouse>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PersonnelPhotoUrl { get; set; }
        public string PersonnelPhotoPath { get; set; }
        [Required]
        public string CurrentRank { get; set; }
        [Required]
        public string PersonalNumber { get; set; }
        [Required]
        public DateTime DateOffBirth { get; set; }
        public DateTime DateOfEnlistment { get; set; }
        [Required]
        public string PresentPosting { get; set; }
        [Required]
        public DateTime DateOfJoiningPresentPosting { get; set; }
        public int GenderId { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        public int MaritalStatusId { get; set; } 
        [ForeignKey("MaritalStatusId")]
        public MaritalStatus MaritalStatus { get; set; }

        public int ReligionId { get; set; }
        [ForeignKey("ReligionId")]
        public Religion Religion { get; set; }

        public string FatherName { get; set; }
        public string MotherName { get; set; }

        [Required]
        public string PermanentAddress { get; set; }
        [Required]
        public string PresentAddress { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }


        public virtual List<PersonnelCaseDetail> PersonnelCaseDetails { get; set; }


        public virtual List<PersonnelSpouse> PersonnelSpouses { get; set; }

        public virtual List<PersonnelChildren> PersonnelChildrens { get; set; }

        public virtual List<PersonnelGallantryAward> PersonnelGallantryAwards { get; set; }


        public virtual List<PersonnelPreviousAllegation> PersonnelPreviousAllegations { get; set; }

        public virtual List<PersonnelPosting>  PersonnelPostings { get; set; }


        public virtual List<PersonnelWarningOrPunishment> PersonnelWarningOrPunishments { get; set; }

        public virtual List<PersonnelEducationalBackground> PersonnelEducationalBackgrounds { get; set; }




    }
}
