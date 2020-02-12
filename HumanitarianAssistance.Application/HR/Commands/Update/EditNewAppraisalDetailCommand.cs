using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditNewAppraisalDetailCommand : BaseModel, IRequest<bool>
    {
        public EditNewAppraisalDetailCommand()
        {
            AppraisalMembers = new List<EditAppraisalMemberList>();
            AppraisalTraining = new List<EditAppraisalTrainingList>();
            AppraisalStrongPoints = new List<EditAppraisalStrongPointsList>();
            AppraisalWeakPoints = new List<EditAppraisalWeakPointsList>();
            GeneralProfessionalIndicatorQuestion = new List<EditGeneralProfessionalIndicatorQuestionList>();

        }
        public int EmployeeAppraisalDetailsId { get; set; }
        public int EmployeeId { get; set; }
        public int AppraisalPeriod { get; set; }
        public DateTime CurrentAppraisalDate { get; set; }
        public string FinalResultQues1 { get; set; }
        public string FinalResultQues2 { get; set; }
        public string FinalResultQues3 { get; set; }
        public string FinalResultQues4 { get; set; }
        public string FinalResultQues5 { get; set; }
        public string CommnetByEmployee { get; set; }
        public List<EditAppraisalMemberList> AppraisalMembers { get; set; }
        public List<EditAppraisalTrainingList> AppraisalTraining { get; set; }
        public List<EditAppraisalStrongPointsList> AppraisalStrongPoints { get; set; }
        public List<EditAppraisalWeakPointsList> AppraisalWeakPoints { get; set; }
        public List<EditGeneralProfessionalIndicatorQuestionList> GeneralProfessionalIndicatorQuestion { get; set; }
    }
    public class EditAppraisalMemberList
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Type { get; set; }
        public int EmployeeId { get; set; }
    }
    public class EditAppraisalTrainingList
    {
        public int TrainingProgramBasedOn { get; set; }
        public string Program { get; set; }
        public int Participated { get; set; }
        public int CatchLevel { get; set; }
        public int RefresherTrm { get; set; }
        public string OtherRecommemenedTraining { get; set; }
        public int EmployeeEvaluationTrainingId { get; set; }
    }
    public class EditAppraisalStrongPointsList
    {
        public string StrongPoints { get; set; }
        public int AppraisalStrongPointsId { get; set; }
    }
    public class EditAppraisalWeakPointsList
    {
        public string WeakPoints { get; set; }
        public int AppraisalWeakPointsId { get; set; }
    }

    public class EditGeneralProfessionalIndicatorQuestionList
    {
        public int SequenceNumber { get; set; }
        public string QuestionEnglish { get; set; }
        public int Score { get; set; }
        public string Remarks { get; set; }
        public int AppraisalGeneralQuestionsId { get; set; }

    }
}