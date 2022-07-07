using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class Allegation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Complainant { get; set; }
        public int PersonalProfileId { get; set; }
        public DateTime DateOfComplaint { get; set; }
        [Required]
        public string AccusedName { get; set; }
        [Required]
        public string AccusedPosting { get; set; }
        [Required]
        public string AccusedRank { get; set; }
        [Required]
        public string ComplaintDetails { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
