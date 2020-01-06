using System;

namespace HumanitarianAssistance.Application.Project.Models {
    public class CandidateDetailsPdfModel {
        public string Office { get; set; }
        public string Position { get; set; }
        public long? SerialNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }
        public string RequiredEducation { get; set; }
        public string RequiredProfession { get; set; }
        public string RequiredExperience { get; set; }
        public string CurrentEducation { get; set; }
        public string CurrentProfession { get; set; }
        public string CurrentExperience { get; set; }
        public double? RelevantExperienceInYear { get; set; }
        public double? IrrelevantExperienceInYear { get; set; }
    }
}