using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class Events
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public int OrganizationId { get; set; }
        public int SubOrganizationId { get; set; }
        public int LeaderId { get; set; }
        public int SubLeaderId { get; set; }
    }
}
