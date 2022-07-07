using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class AllegationNote
    {
        [Required]
        public int Id { get; set; }
       
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Description { get; set; }
        public int AllegationId { get; set; }
        [ForeignKey(nameof(AllegationId))]
        public Allegation Allegation { get; set; }
    }
}
