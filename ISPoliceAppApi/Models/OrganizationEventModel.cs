using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public partial class OrganizationEventModel
    {
        
       
        public string title { get; set; }
        public DateTime eventDate { get; set; }
      
        public string description { get; set; }
       
        public int organizationId { get; set; }

        public int subOrganizationId { get; set; }



        








    }
}
