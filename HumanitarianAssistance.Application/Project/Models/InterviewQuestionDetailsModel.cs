using System;
namespace HumanitarianAssistance.Application.Project.Models {
    public class LanguageDetailsModel {
        public string LanguageName { get; set; }
        public string LanguageReading { get; set; }
        public string LanguageWriting { get; set; }
        public string LanguageListining { get; set; }
        public string LanguageSpeaking { get; set; }
    }
    public class TraningDetailsModel {
        public string TraningType { get; set; }
        public string TraningName { get; set; }
        public string TraningCountryAndCity { get; set; }
        public string TraningStartDate { get; set; }
        public string TraningEndDate { get; set; }
    }
    public class InterviewerDetailsModel {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
    }
    public class InterviewQuestionDetailsModel {
        public int? QuestionId { get; set; }
        public int? Score { get; set; }
    }
}