using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeAppraisalDetailsModel
    {
		public EmployeeAppraisalDetailsModel()
		{
			EmployeeAppraisalQuestionList = new List<EmployeeAppraisalQuestionModel>();
			EmployeeEvaluationModelList = new List<EmployeeEvaluationModel>();
		}
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



		public List<EmployeeEvaluationModel> EmployeeEvaluationModelList { get; set; }
		public List<string> StrongPoints { get; set; }
		public List<string> WeakPoints { get; set; }
		public string FinalResultQues1 { get; set; }
		public string FinalResultQues2 { get; set; }
		public string FinalResultQues3 { get; set; }
		public string FinalResultQues4 { get; set; }
		public string FinalResultQues5 { get; set; }
		public string DirectSupervisor { get; set; }
		public string AppraisalTeamMember1 { get; set; }
		public string AppraisalTeamMember2 { get; set; }
		public string CommentsByEmployee { get; set; }

	}
}
