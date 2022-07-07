using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
   
    public partial class ControlRoomDSRModel
    {
        public int ControlRoomId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime Date { get; set; }
        public string PSName { get; set; }
        public string GivenBy { get; set; }
        public string TakenBy { get; set; }
        public string Subject { get; set; }
        public string CaseNo { get; set; }
        public string Complainant { get; set; }
        public string ComplainantAddress { get; set; }

    }

}
