using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeAppraisalDetailsModel
    {
		public int EmployeeId { get; set; }
		public string EmployeeCode { get; set; }
		public string EmployeeName { get; set; }
		public string FatherName { get; set; }
		public string Position { get; set; }
		public string Department { get; set; }
		public string Qualification { get; set; }
		public string DutyStation { get; set; }
		public DateTime RecruitmentDate { get; set; }
		public int AppraisalPeriod { get; set; }
		public DateTime CurrentAppraisalDate { get; set; }
		public int OfficeId { get; set; }
		public int TotalScore { get; set; }
		public List<EmployeeAppraisalQuestionModel> EmployeeAppraisalQuestionList { get; set; }
	}
}
