using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
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






        //public long ScheduleId { get; set; }
        //public long JobId { get; set; }
        //public int EmployeeId { get; set; }
        //public int? InterviewStatus { get; set; }
        //public DateTime Date { get; set; }
        //public string Time { get; set; }
        //public DateTime? NextRoundDate { get; set; }
        //public string NextRoundTime { get; set; }
        //public string Description { get; set; }
    }

    public class ScheduleCandidateModel: BaseModel
    {
        public long ScheduleId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNo { get; set; }
        public long? JobId { get; set; }
        public string JobCode { get; set; }
        public int? GradeId { get; set; }
        public string GradeName { get; set; }
        public int? InterviewStatus { get; set; }
        public DateTime Date { get; set; }
        public bool Approval1 { get; set; }
        public bool Approval2 { get; set; }
        public bool Approval3 { get; set; }
        public bool Approval4 { get; set; }


        
        //public long ScheduleId { get; set; }
        //public long? JobId { get; set; }
        //public string JobCode { get; set; }
        //public int? EmployeeId { get; set; }
        //public string EmployeeName { get; set; }
        //public string PhoneNo { get; set; }
        //public string RoundName { get; set; }
        //public int? Status { get; set; }
        //public DateTime Date { get; set; }
        //public string Time { get; set; }
        //public DateTime? NextRoundDate { get; set; }
        //public string NextRoundTime { get; set; }
        //public string Description { get; set; }
    }

    public class JobGradeModel : BaseModel
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }

    }
}
