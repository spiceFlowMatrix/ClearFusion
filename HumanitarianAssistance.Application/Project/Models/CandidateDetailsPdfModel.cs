using System;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class CandidateDetailsPdfModel
    {
        public long CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountStatus { get; set; }
        public string Gender { get; set; }
        public string EducationDegree { get; set; }
        public string Profession { get; set; }
         public long InterviewId { get; set; }  
        public string Status { get; set; }       
        public double TotalExperienceInYear { get; set; }
        public double RelevantExperienceInYear { get; set; }
        public double IrrelevantExperienceInYear { get; set; }
    }
}