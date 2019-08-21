using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeApplyLeave : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ApplyLeaveId { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetails { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //[StringLength(30)]
        //public int LeaveTypeId { get; set; } //1 = Full Day, 2 = Half Day
        public int LeaveReasonId { get; set; }
        public LeaveReasonDetail LeaveReasonDetails { get; set; }
        [StringLength(30)]
        public int? ApplyLeaveStatusId { get; set; } //Approved, Not Approved, Rejected
        public string Remarks { get; set; }
        public int? FinancialYearId { get; set; }
        public FinancialYearDetail FinancialYearDetail { get; set; }

    }
}
