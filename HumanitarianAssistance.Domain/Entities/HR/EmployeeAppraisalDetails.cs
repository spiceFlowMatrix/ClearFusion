using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeAppraisalDetails: BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int EmployeeAppraisalDetailsId { get; set; }
		public int EmployeeId { get; set; }
		[ForeignKey("EmployeeId")]
		public EmployeeDetail EmployeeDetail { get; set; }
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
		public bool AppraisalStatus { get; set; }
		public int? TotalScore { get; set; }
        public double? AppraisalScore { get; set; }  
		public List<EmployeeAppraisalQuestions> EmployeeAppraisalQuestions{get;set;}
		public List<EmployeeAppraisalTeamMember> EmployeeAppraisalTeamMember{get;set;}
		public List<StrongandWeakPoints> StrongandWeakPoints{get;set;}
		public EmployeeEvaluation EmployeeEvaluation{get;set;}
		public List<EmployeeEvaluationTraining> EmployeeEvaluationTraining{get;set;}


    }
}
