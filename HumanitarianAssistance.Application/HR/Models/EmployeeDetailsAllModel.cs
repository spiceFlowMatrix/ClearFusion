using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
   public class EmployeeDetailsAllModel : BaseModel
    {
        public int? EmployeeTypeId { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhoto { get; set; }
        public int? Age { get; set; }
        public string SexName { get; set; }
        public string DocumentGUID { get; set; }
        public string EmployeeDOB { get; set; }
        public DateTime? HiredOn { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
        public int? DesignationId { get; set; }
        public int? ExperienceYear { get; set; }
        public int? ExperienceMonth { get; set; }
        public string MaritalStatus { get; set; }
        public string PassportNo { get; set; }
        public string University { get; set; }
        public string BirthPlace { get; set; }
        public string IssuePlace { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int? ProfessionId { get; set; }
        public string PreviousWork { get; set; }
        public int? Qualificationid { get; set; }
    }
}
