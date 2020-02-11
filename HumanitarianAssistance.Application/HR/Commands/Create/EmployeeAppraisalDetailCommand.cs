using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class EmployeeAppraisalDetailCommand : BaseModel, IRequest<object>
    {
        public EmployeeAppraisalDetailCommand()
        {
            AppraisalMembers = new List<AppraisalMemberList>();
            AppraisalTraining = new List<AppraisalTrainingList>();
            AppraisalStrongPoints = new List<AppraisalStrongPointsList>();
            AppraisalWeakPoints = new List<AppraisalWeakPointsList>();
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
        public string FinalResultQues6 { get; set; }
        public List<AppraisalMemberList> AppraisalMembers { get; set; }
        public List<AppraisalTrainingList> AppraisalTraining { get; set; }
        public List<AppraisalStrongPointsList> AppraisalStrongPoints { get; set; }
        public List<AppraisalWeakPointsList> AppraisalWeakPoints { get; set; }
    }
    public class AppraisalMemberList
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Type { get; set; }
        public int EmployeeId { get; set; }
    }
    public class AppraisalTrainingList
    {
        public string TrainingProgramBasedOn { get; set; }
        public string Program { get; set; }
        public string Participated { get; set; }
        public string CatchLevel { get; set; }
        public string RefresherTrm { get; set; }
        public string OtherRecommemenedTraining { get; set; }
        public int EmployeeEvaluationTrainingId { get; set; }
    }
    public class AppraisalStrongPointsList
    {
        public string StrongPoints { get; set; }
        public int AppraisalStrongPointsId { get; set; }
    }
    public class AppraisalWeakPointsList
    {
        public string WeakPoints { get; set; }
        public int AppraisalWeakPointsId { get; set; }
    }
}