using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class Leader
    {
        public Leader()
        {
            LeaderEvents = new List<LeaderEvent>();
            LeaderMedia = new List<LeaderMedia>();
            LeaderPoliticalBackgrounds = new List<LeaderPoliticalBackground>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        //[Required]
        public string Alias { get; set; }
        //[Required]
        public string PlaceOfBirth { get; set; }
        //[Required]
        public DateTime DateOfBirth { get; set; }
        public string Caste { get; set; }
        //[Required]
        public string PermanentAddress { get; set; }
        //[Required]
        public string PresentAddress { get; set; }
        //[Required]
        public string NativeDistrict { get; set; }

        public string Properties { get; set; }

        public string StrinkingPersonalityTrait { get; set; }
        public string PresentPartyAffiliation { get; set; }
        public string PositionInTheParty { get; set; }
        public int ReligionId { get; set; }
        public int GenderId { get; set; } 

        public int OrganizationLeaderId { get; set; }
        public int SubOrganizationLeaderId { get; set; }
        public int MaritalStatusId { get; set; }       
        public virtual List<LeaderEvent> LeaderEvents { get; set; }
        public virtual List<LeaderMedia> LeaderMedia { get; set; }
        public virtual List<LeaderPoliticalBackground> LeaderPoliticalBackgrounds { get; set; }

    }
}
