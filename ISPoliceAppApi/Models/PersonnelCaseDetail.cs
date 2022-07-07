using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class PersonnelCaseDetail
    {
        [Required]
        public int Id { get; set; }
        public DateTime CaseCreatedDate { get; set; }
        [Required]
        public string CaseNumber { get; set; }
        [Required]
        public string Title { get; set; }
        public string CreatedBy { get; set; }
     
        public string CaseSection { get; set; }

        public string CurrentStatus { get; set; }


        public int PersonnelId { get; set; }
        [ForeignKey(nameof(PersonnelId))]
        public Personnel Personnel { get; set; }
    }
}
