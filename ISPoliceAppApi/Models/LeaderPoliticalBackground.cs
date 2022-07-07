using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class LeaderPoliticalBackground
    {

        public int Id { get; set; }
        [Required]
        public DateTime PositionYear { get; set; }
        [Required]
        public string Position { get; set; }
        public int LeaderId { get; set; }
        [NotMapped]
        [ForeignKey(nameof(LeaderId))]
        public Leader Leader { get; set; }
    }
}
