using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddEmployeeAppraisalCommand: BaseModel, IRequest<ApiResponse>
    {

        public AddEmployeeAppraisalCommand()
		{
			EmployeeAppraisalQuestionList = new List<EmployeeAppraisalQuestionModel>();
			EmployeeEvaluationModelList = new List<EmployeeEvaluationTrainingModel>();
			StrongPoints = new List<string>();
			WeakPoints = new List<string>();
		}

		public int EmployeeEvaluationId { get; set; }
		public int EmployeeAppraisalDetailsId { get; set; }
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
		public int? TotalScore { get; set; }
		public bool AppraisalStatus { get; set; }
        public double? AppraisalScore { get; set; }  
        public List<EmployeeAppraisalQuestionModel> EmployeeAppraisalQuestionList { get; set; }
		public List<EmployeeEvaluationTrainingModel> EmployeeEvaluationModelList { get; set; }
		public List<string> StrongPoints { get; set; }
		public List<string> WeakPoints { get; set; }
		public string FinalResultQues1 { get; set; }
		public string FinalResultQues2 { get; set; }
		public string FinalResultQues3 { get; set; }
		public string FinalResultQues4 { get; set; }
		public string FinalResultQues5 { get; set; }
		public int DirectSupervisor { get; set; }
        public List<int> EmployeeAppraisalTeamMemberList { get; set; }
        public string CommentsByEmployee { get; set; }
        public string EvaluationStatus { get; set; }
    }
}