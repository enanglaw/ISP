using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class LeaderMedia
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public string LeaderMediaPath { get; set; }
        [StringLength(256)]
        public string LeaderMediaUrl { get; set; }

        public int LeaderId { get; set; }
        [NotMapped]
        [ForeignKey(nameof(LeaderId))]
        public Leader Leader { get; set; }
    }
}
