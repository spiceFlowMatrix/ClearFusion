using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class InterviewScheduleDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ScheduleId { get; set; }
        public long JobId { get; set; }
        public JobHiringDetails JobHiringDetails { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        [StringLength(50)]
        //public int? Status { get; set; }
        public int? InterviewStatus { get; set; }
        public DateTime Date { get; set; }
        //public int? Grade { get; set; }
        public bool? Approval1 { get; set; }
        public bool? Approval2 { get; set; }
        public bool? Approval3 { get; set; }
        public bool? Approval4 { get; set; }
        public int? GradeId { get; set; }
        [ForeignKey("GradeId")]
        public JobGrade JobGrade { get; set; }
    }
}
