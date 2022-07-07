using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class EventsModel
    {
        public int Id { get; set; }
        public int EventGroupId { get; set; }
        public string EventGroupName { get; set; }
        public string Title { get; set; }
        public DateTime EventDate  { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int SubOrganizationId { get; set; }
        public string SubOrganizationName { get; set; }
        public int LeaderId { get; set; }
        public string LeaderName { get; set; }
        public int SubLeaderId { get; set; }
        public string SubLeaderName { get; set; }

    }

    public class EventsModelDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public string EventGroupName { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int SubOrganizationId { get; set; }
        public string SubOrganizationName { get; set; }

    }
}
