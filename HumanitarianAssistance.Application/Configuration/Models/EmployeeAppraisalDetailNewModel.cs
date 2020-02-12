using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class EmployeeAppraisalDetailNewModel
    {
        public EmployeeAppraisalDetailNewModel()
        {
            AppraisalMembers = new List<AppraisalMemberListModel>();
            AppraisalTraining = new List<AppraisalTrainingListModel>();
            AppraisalStrongPoints = new List<AppraisalStrongPointsListModel>();
            AppraisalWeakPoints = new List<AppraisalWeakPointsListModel>();
            GeneralProfessionalIndicatorQuestion = new List<GeneralProfessionalIndicatorQuestionListModel>();

        }

        public int EmployeeAppraisalDetailsId { get; set; }
        public int? EmployeeId { get; set; }
        public int AppraisalPeriod { get; set; }
        public DateTime CurrentAppraisalDate { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string FinalResultQues1 { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Qualification { get; set; }
        public string DutyStation { get; set; }
        public DateTime RecruitmentDate { get; set; }

        public int OfficeId { get; set; }
        public double TotalScore { get; set; }
        public string AppraisalScore { get; set; }
        public bool AppraisalStatus { get; set; }
        public string EvaluationDisplayDate { get; set; }

        public string FinalResultQues2 { get; set; }
        public string FinalResultQues3 { get; set; }
        public string FinalResultQues4 { get; set; }
        public string FinalResultQues5 { get; set; }
        public string CommnetByEmployee { get; set; }
        public int EmployeeEvaluationId { get; set; }
        public string DirectSupervisor { get; set; }
        public string EvaluationStatus { get; set; }
        public int DepartmentId { get; set; }



        public List<AppraisalMemberListModel> AppraisalMembers { get; set; }
        public List<AppraisalTrainingListModel> AppraisalTraining { get; set; }
        public List<AppraisalStrongPointsListModel> AppraisalStrongPoints { get; set; }
        public List<AppraisalWeakPointsListModel> AppraisalWeakPoints { get; set; }
        public List<GeneralProfessionalIndicatorQuestionListModel> GeneralProfessionalIndicatorQuestion { get; set; }
    }
    public class AppraisalMemberListModel
    {
        public int EmployeeAppraisalTeamMemberId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Type { get; set; }
        public int EmployeeId { get; set; }
    }
    public class AppraisalTrainingListModel
    {
        public int TrainingProgramBasedOn { get; set; }
        public string Program { get; set; }
        public int Participated { get; set; }
        public int CatchLevel { get; set; }
        public int RefresherTrm { get; set; }
        public string OtherRecommemenedTraining { get; set; }
        public int EmployeeEvaluationTrainingId { get; set; }
    }
    public class AppraisalStrongPointsListModel
    {
        public string StrongPoints { get; set; }
        public int AppraisalStrongPointsId { get; set; }
    }
    public class AppraisalWeakPointsListModel
    {
        public string WeakPoints { get; set; }
        public int AppraisalWeakPointsId { get; set; }
    }

    public class GeneralProfessionalIndicatorQuestionListModel
    {
        public int SequenceNumber { get; set; }
        public string QuestionEnglish { get; set; }
        public int Score { get; set; }
        public string Remarks { get; set; }
        public int AppraisalGeneralQuestionsId { get; set; }

    }

}