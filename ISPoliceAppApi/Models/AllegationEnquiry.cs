using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class AllegationEnquiry
    {
        public AllegationEnquiry()
        {
            AllegationEnquiryDocuments = new List<AllegationEnquiryDocument>();
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Participant { get; set; }
        public string OutCome { get; set; }
        public string MOM { get; set; }
        public int AllegationId { get; set; }
        [ForeignKey(nameof(AllegationId))]
        public Allegation Allegation { get; set; }
        public virtual List<AllegationEnquiryDocument> AllegationEnquiryDocuments { get; set; }
    }
}
