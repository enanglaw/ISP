using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class PersonnelGallantryAward
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string IssueingAuthority { get; set; }
        public DateTime IssuingDate { get; set; }
        public string AwardDocumentUrl { get; set; }
        public string GallantryAwardPath { get; set; }
        [StringLength(256)]
        public string GallantryAwardUrl { get; set; }

        public int PersonnelId { get; set; }
        [ForeignKey(nameof(PersonnelId))]
        public Personnel Personnel { get; set; }
    }
}
