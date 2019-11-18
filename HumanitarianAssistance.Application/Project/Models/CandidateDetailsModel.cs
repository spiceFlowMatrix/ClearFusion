using System;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class CandidateDetailsModel
    {
         public long CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountStatus { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EducationDegree { get; set; }
        public string Grade { get; set; }
        public string Profession { get; set; }
        public string Office { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public long InterviewId { get; set; }  
        public int CandidateStatus { get; set; }       
        public double TotalExperienceInYear { get; set; }
        public double RelevantExperienceInYear { get; set; }
        public double IrrelevantExperienceInYear { get; set; }
    }
}