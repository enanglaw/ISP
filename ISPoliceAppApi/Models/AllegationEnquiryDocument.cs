using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class AllegationEnquiryDocument
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }

        public string DocumentUrl { get; set; }

        public int AllegationEnquiryId { get; set; }
        [ForeignKey(nameof(AllegationEnquiryId))]
        public AllegationEnquiry AllegationEnquiry { get; set; }
    }
}
