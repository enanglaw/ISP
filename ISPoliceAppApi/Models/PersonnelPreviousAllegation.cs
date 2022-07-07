using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class PersonnelPreviousAllegation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public string Result { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentUrl   { get; set; }

        public int PersonnelId { get; set; }
        [ForeignKey(nameof(PersonnelId))]
        public Personnel Personnel { get; set; }
    }
}
