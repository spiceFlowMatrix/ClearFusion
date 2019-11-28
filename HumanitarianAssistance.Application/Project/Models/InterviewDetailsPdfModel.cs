using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class InterviewDetailsPdfModel
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
        public DateTime? AvailableDate { get; set; }
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
        public string LogoPath { get; set; }
        public string CheckRadioPath { get; set; }
        public string UncheckRadioPath { get; set; }
        public string CheckedIconPath { get; set; }
        public string UnCheckedIconPath { get; set; }
        public string PersianChaName { get; set; }
    }
}