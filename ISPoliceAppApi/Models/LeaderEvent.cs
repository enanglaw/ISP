using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class LeaderEvent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public int LeaderId { get; set; }
        [NotMapped]
        [ForeignKey(nameof(LeaderId))]
        public Leader Leader { get; set; }
    }
}
