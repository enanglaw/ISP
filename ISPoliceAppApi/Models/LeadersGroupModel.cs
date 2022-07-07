using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class LeadersGroupModel
    {
       
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
       
        public string MobileNumber { get; set; }
       
        public string Address { get; set; }
        public int OrganizationId { get; set; }

       


    }
}
