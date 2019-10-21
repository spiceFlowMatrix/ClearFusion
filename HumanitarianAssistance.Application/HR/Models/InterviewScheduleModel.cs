using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class InterviewScheduleModel : BaseModel
    {
        public long ScheduleId { get; set; }
        public long JobId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNo { get; set; }
        public string JobCode { get; set; }
        public int? InterviewStatus { get; set; }
        public DateTime? Date { get; set; }
        public bool? Approval1 { get; set; }
        public bool? Approval2 { get; set; }
        public bool? Approval3 { get; set; }
        public bool? Approval4 { get; set; }
        public int? GradeId { get; set; }
        public string GradeName { get; set; }
    }
}
