using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.DSR
{
    public partial class ControlRoomDSR
    {
        public ControlRoomDSR()
        {
            ControlRoomDSRAccuseds = new HashSet<ControlRoomDSRAccused>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ControlRoomId { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string PSName { get; set; }
        [Required]
        public int PSId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int ZoneId { get; set; }

        [Required]
        public string GivenBy { get; set; }
        [Required]

        public string TakenBy { get; set; }
        [Required]

        public string Subject { get; set; }
        public string CaseNo { get; set; }
        public DateTime Do { get; set; }
        public DateTime Dr { get; set; }
        public string DrSource { get; set; }
        public string SOC { get; set; }
        public string Complainant { get; set; }
        public string ComplainantAddress { get; set; }
        public string PL { get; set; }
        public string PR { get; set; }
        public string TotalAccused { get; set; }
        public string Detail { get; set; }
        public string PSNote { get; set; }
        public int status { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(ControlRoomDSRCategory.ControlRoomDSRs))]
        public virtual ControlRoomDSRCategory ControlRoomCategory { get; set; }

        [InverseProperty("ControlRoom")]
        public virtual ICollection<ControlRoomDSRAccused> ControlRoomDSRAccuseds { get; set; }


    }

    public partial class ControlRoomDSRAccused
    {
        public ControlRoomDSRAccused()
        {
            ControlRoomDSRAccusedDetails = new HashSet<ControlRoomDSRAccusedDetail>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ControlRoomAccusedId { get; set; }
        public int ControlRoomId { get; set; }
        public string HSNbr { get; set; }
        public string AccusedName { get; set; }

        public string AccusedAddress { get; set; }
        public string Status { get; set; }
        public string CrimeNumber { get; set; }
        public string SectionNumber { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey(nameof(ControlRoomId))]
        [InverseProperty(nameof(ControlRoomDSR.ControlRoomDSRAccuseds))]
        public virtual ControlRoomDSR ControlRoom { get; set; }

        [InverseProperty("DSRAccused")]
        public virtual ICollection<ControlRoomDSRAccusedDetail> ControlRoomDSRAccusedDetails { get; set; }
    }
   
    public partial class ControlRoomDSRCategory
    {
        public ControlRoomDSRCategory()
        {
            ControlRoomDSRs = new HashSet<ControlRoomDSR>();
        }

        public string CategoryName { get; set; }
        [Key]
        public int CategoryId { get; set; }

        [InverseProperty("ControlRoomCategory")]
        public virtual ICollection<ControlRoomDSR> ControlRoomDSRs { get; set; }
    }
    public partial class ControlRoomDSRAccusedDetail
    {
        [Key]
        public int Id { get; set; }
        public int DSRAccusedId { get; set; }
        public string CrimeNumber { get; set; }
        public string SectionNumber { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey(nameof(DSRAccusedId))]
        [InverseProperty(nameof(ControlRoomDSRAccused.ControlRoomDSRAccusedDetails))]
        public virtual ControlRoomDSRAccused DSRAccused { get; set; }
    }
}
