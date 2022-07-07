using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class PersonnelAllegationEnquiry
    {
        public int Id { get; set; }
        [Required]      
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string ParticipantPath { get; set; }
        public string ParticipantUrl { get; set; }
        public string OutComePath { get; set; }
        public string OutComeUrl { get; set; }
        public string MOMPath { get; set; }
        public string MOMUrl { get; set; }
        public string NotesPath { get; set; }
        public string NotesUrl { get; set; }
        public string MemorandumPath { get; set; }

        public string MemorandumUrl { get; set; }
        public int AllegationId { get; set; }
        [ForeignKey(nameof(AllegationId))]
        public Allegation Allegation { get; set; }
    }
}
