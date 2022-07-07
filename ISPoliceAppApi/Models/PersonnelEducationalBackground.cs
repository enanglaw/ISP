using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Models
{
    public class PersonnelEducationalBackground
    {
        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public string CourseOfStudy { get; set; }
        [Required]
        public string QualificationName { get; set; }
        public DateTime AdmissionYear { get; set; }
        public DateTime GraduationYear { get; set; }

        public int PersonnelId { get; set; }
        [ForeignKey(nameof(PersonnelId))]
        public Personnel Personnel { get; set; }
    }
}
