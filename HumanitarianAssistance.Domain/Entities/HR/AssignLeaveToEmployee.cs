using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class AssignLeaveToEmployee : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        public int LeaveReasonId { get; set; }
        public LeaveReasonDetail LeaveReasonDetails { get; set; }
        public int? AssignUnit { get; set; }
        public int FinancialYearId { get; set; }
        public FinancialYearDetail FinancialYearDetails { get; set; }
        [StringLength(50)]
        public string Status { get; set; } //1 = Monthly , 2 = Based On Financial Year
        public int? UsedLeaveUnit { get; set; }
        public string Description { get; set; }
    }
}
