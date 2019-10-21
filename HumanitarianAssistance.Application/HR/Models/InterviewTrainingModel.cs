using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class InterviewTrainingModel
    {
        public int TraininigType { get; set; }
        public string TrainingName { get; set; }
        public string StudyingCountry { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
