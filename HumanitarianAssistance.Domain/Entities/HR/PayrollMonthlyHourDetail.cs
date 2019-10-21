using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class PayrollMonthlyHourDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int PayrollMonthlyHourID { get; set; }
        //public string OfficeCode { get; set; }
        public int OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }
        public int? PayrollMonth { get; set; }
        public int? PayrollYear { get; set; }
        public int? Hours { get; set; }
		public DateTime? InTime { get; set; }
		public DateTime? OutTime { get; set; }
        public int? WorkingTime { get; set; }
        public int? WorkingDay { get; set; }
        public long? AttendanceGroupId { get; set; }
        [ForeignKey("AttendanceGroupId")]
        public AttendanceGroupMaster AttendanceGroupMaster { get; set; }
    }
}
