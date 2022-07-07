using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class Plan
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
       
        public string PlanUrl { get; set; }
       public string PlanPath { get; set; }

    }
}
