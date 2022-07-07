using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class SubLeadersGroup
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public int SubOrganizationId { get; set; }
        [ForeignKey(nameof(SubOrganizationId))]
        public SubOrganizationCategory SubOrganization { get; set; }


    }
}
