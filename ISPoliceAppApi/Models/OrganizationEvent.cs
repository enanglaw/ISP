using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public partial class OrganizationEvent
    {
        public OrganizationEvent()
        {

        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [Required]
        public string Description { get; set; }
       
        public int OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }





    }
}
