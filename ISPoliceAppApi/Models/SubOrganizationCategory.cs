using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public partial class SubOrganizationCategory
    {
        public SubOrganizationCategory()
        {

        }
        [Key]
        public int Id { get; set; }
       
        [Required]
        public string Name { get; set; } = null;
        public string Description { get; set; }
        [NotMapped]
        public int OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }
    }
}
