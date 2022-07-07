using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.DTOs
{
   
    public partial class AllegationCreationDTO
    {
        public string Complainant { get; set; }
        public int PersonnelProfileId { get; set; }
        public string AccusedName { get; set; }
        public string AccusedPosting { get; set; }
        public string AccusedRank { get; set; }
        public  string DateOfComplaint { get; set; }
        public string ComplaintDetails { get; set; }
        public string AttachmentUrl { get; set; }
        public string Title { get; set; }
    }
    public class AllegationDropdown
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Complainant { get; set; }

    }
    public partial class AllegationUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Complainant { get; set; }
        public int PersonnelProfileId { get; set; }
        public string AccusedName { get; set; }
        public string AccusedPosting { get; set; }
        public string DateOfComplaint { get; set; }
        public string AccusedRank { get; set; }
        public string ComplaintDetails { get; set; }
        public string AttachmentUrl { get; set; }
    }
}