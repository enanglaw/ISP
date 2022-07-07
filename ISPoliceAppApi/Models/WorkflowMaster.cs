using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPoliceAppApi.Models
{
    public partial class WorkflowMaster
    {
        public WorkflowMaster()
        {
        }

        [Key]
        public int WorkflowId { get; set; }
        [StringLength(50)]
        public string WorkflowName { get; set; }
        [Required]
        public bool? IsActive { get; set; }
    }
}
