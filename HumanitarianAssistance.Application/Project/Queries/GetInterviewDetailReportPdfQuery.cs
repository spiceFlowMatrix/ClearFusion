using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetInterviewDetailReportPdfQuery : IRequest<byte[]> 
    {
        public List<InterviewQuestionDetailsModel> RatingBasedCriteriaList { get; set; }
        public List<InterviewQuestionDetailsModel> TechnicalQuestionList { get; set; }
        public List<LanguageDetailsModel> LanguageList { get; set; }
        public List<TraningDetailsModel> TraningList { get; set; }
        public List<InterviewerDetailsModel> InterviewerList { get; set; }

        public long CandidateId { get; set; }
        public long HiringRequestId { get; set; }
        public string Description { get; set; }
        public int NoticePeriod { get; set; }
        public DateTime AvailableDate { get; set; }
        public int WrittenTestMarks { get; set; }
        public int CurrentBase { get; set; }
        public int CurrentOther { get; set; }
        public int ExpectationBase { get; set; }
        public int ExpectationOther { get; set; }
        public int Status { get; set; }
        public bool InterviewQuestionOne { get; set; }
        public bool InterviewQuestionTwo { get; set; }
        public bool InterviewQuestionThree { get; set; }
        public bool CurrentTransport { get; set; }
        public bool CurrentMeal { get; set; }
        public bool ExpectationTransport { get; set; }
        public bool ExpectationMeal { get; set; }
        public double ProfessionalCriteriaMark { get; set; }
        public int MarksObtain { get; set; }
        public double TotalMarksObtain { get; set; }
        public string CandidateName { get; set; }
        public string Qualification { get; set; }
        public string Position { get; set; }
        public string DutyStation { get; set; }
        public string MaritalStatus { get; set; }
         public string PassportNumber { get; set; }
         public string NameOfInstitute { get; set; }
         public string DateOfBirth { get; set; }
    }
}