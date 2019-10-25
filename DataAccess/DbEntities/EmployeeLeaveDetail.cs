using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class EmployeeLeaveDetail : BaseEntity
    {
        [Key]
        [Column(Order = 0)]
        public int LeaveMonth { get; set; }

        [Key]
        [Column(Order = 1)]
        public int LeaveYear { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string RegCode { get; set; }

        [Key]
        [Column(Order = 3)]
        public int EmployeeID { get; set; }
 
        [StringLength(5)]
        public string ContractStatus { get; set; }
	    public float? Attendance { get; set; }
	    public float? Casual { get; set; }
	    public float? Medical { get; set; }
        public float? Emergency { get; set; }
        public float? Maternity { get; set; }
        public float? Absent { get; set; }
        public float? Deputation { get; set; }
        public Boolean? Sent { get; set; }
        public Boolean? Status { get; set; }
        public int? TotalDuration { get; set; }
    }
}
