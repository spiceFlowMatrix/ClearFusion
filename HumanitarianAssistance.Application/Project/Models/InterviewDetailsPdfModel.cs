using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Models {
    public class InterviewDetailsPdfModel {
        public List<AnswerList> RatingBasedCriteriaAnswerList { get; set; }
        public List<AnswerList> TechnicalAnswerList { get; set; }
        public List<LanguageDetailsModel> LanguageList { get; set; }
        public List<TraningDetailsModel> TraningList { get; set; }
        public List<InterviewerList> InterviewerList { get; set; }
        
         public List<EducationList> EducationList { get; set; }
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
        public string CandidateName { get; set; }
        public string Qualification { get; set; }
        public string Position { get; set; }
        public string DutyStation { get; set; }
        public string MaritalStatus { get; set; }
        public string PassportNumber { get; set; }
        public string NameOfInstitute { get; set; }
        public string DateOfBirth { get; set; }
        public string Province { get; set; }
    }

    public class AnswerList {
        public string Question { get; set; }
        public int? Score { get; set; }
    }
    public class InterviewerList {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; }
        public string Signature { get; set; }
    }
    public class EducationList
    {
        public string EducationName { get; set; }
    }

}