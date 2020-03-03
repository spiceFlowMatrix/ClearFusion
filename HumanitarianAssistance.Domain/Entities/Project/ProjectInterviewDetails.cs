using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Project {
    public class ProjectInterviewDetails : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public int InterviewId { get; set; }
        public string Description { get; set; }
        public int NoticePeriod { get; set; }
        public DateTime? AvailableDate { get; set; }
        public double WrittenTestMarks { get; set; }
        public int CurrentBase { get; set; }
        public bool CurrentTransport { get; set; }
        public bool CurrentMeal { get; set; }
        public int CurrentOther { get; set; }
        public int ExpectationBase { get; set; }
        public bool ExpectationTransport { get; set; }
        public bool ExpectationMeal { get; set; }
        public int ExpectationOther { get; set; }
        public int Status { get; set; }
        public bool InterviewQuestionOne { get; set; }
        public bool InterviewQuestionTwo { get; set; }
        public bool InterviewQuestionThree { get; set; }
        public double ProfessionalCriteriaMarks { get; set; }
        public double MarksObtained { get; set; }
        public double TotalMarksObtain { get; set; }
        public virtual List<RatingBasedCriteria> RatingBasedCriteriaList { get; set; }
        public virtual List<InterviewLanguages> InterviewLanguagesList { get; set; }
        public virtual List<InterviewTrainings> InterviewTrainingsList { get; set; }
        public virtual List<InterviewTechnicalQuestion> InterviewTechnicalQuestionList { get; set; }
        public virtual List<HRJobInterviewers> HRJobInterviewersList { get; set; }
    }
}