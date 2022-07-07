using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class Organization_Sub_Leaders_Rs
    {
        [Key]
        public int Id { get; set; }

        public int LeadersId { get; set; }
        public int SubOrganizationId { get; set; }

        public int OrganizationId { get; set; }
    }
}
