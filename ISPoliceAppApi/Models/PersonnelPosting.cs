using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class PersonnelPosting
    {
        public int Id { get; set; }
        [Required]
        public string Post { get; set; }
        [Required]
        public string Place { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int PersonnelId { get; set; }
        [ForeignKey(nameof(PersonnelId))]
        public Personnel Personnel { get; set; }
    }
}
