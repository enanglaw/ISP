using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public partial class Organization
    {

        public Organization()
        {
            OrganizationMedia = new List<OrganizationMedia>();
            OrganizationEvent = new List<OrganizationEvent>();
            SubOrganizationCategory = new List<SubOrganizationCategory>();
        }
        public int OrganizationId { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Ideology { get; set; }
        [Required]
        [StringLength(256)]
        public string SymbolUrl { get; set; }
        [StringLength(256)]
        public string SymbolPath { get; set; }
        [StringLength(512)]
        public string FlagPath { get; set; }

        [StringLength(256)]
        public string FlagUrl { get; set; }
        public virtual List<SubOrganizationCategory> SubOrganizationCategory { get; set; }

     
        public virtual List<OrganizationEvent> OrganizationEvent { get; set; }

       
        public virtual List<OrganizationMedia> OrganizationMedia { get; set; }

    }
}
