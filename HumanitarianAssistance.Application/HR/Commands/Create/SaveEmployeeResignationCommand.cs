using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class SaveEmployeeResignationCommand : BaseModel, IRequest<object>
    {
        public int EmployeeID { get; set; } 
        public DateTime ResignDate { get; set; }
        public bool IsUnresolvedIssue { get; set; }
        public string CommentsIssues { get; set; }
        public List<ResignationQuestionModel> QuestionType1 { get; set; }
        public List<ResignationQuestionModel> QuestionType2 { get; set; }
        public List<ResignationQuestionModel> QuestionType3 { get; set; }
        public List<ResignationQuestionModel> QuestionType4 { get; set; }
        public List<ResignationQuestionModel> QuestionType5 { get; set; }
        public List<ResignationQuestionModel> QuestionType6 { get; set; }
    }

    public class ResignationQuestionModel {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public int Answer { get; set; }
    }
}
