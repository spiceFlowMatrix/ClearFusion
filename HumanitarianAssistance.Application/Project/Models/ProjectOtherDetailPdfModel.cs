using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectOtherDetailPdfModel
    {
        public int OpportunityType { get; set; }
        public string Donor { get; set; } 
        public string opportunityNo { get; set; }
        public string opportunity { get; set; }
        public string opportunitydescription { get; set; }
        public string CountryName { get; set; }
        public string[] Province { get; set; }
        public string[] District { get; set; }
        public string OfficeName { get; set; }
        public string SectorName { get; set; }
        public string ProgramName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string projectGoal { get; set; }
        public string projectObjective { get; set; } 
        public DateTime REOIReceiveDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string mainActivities { get; set; }
        public int beneficiaryMale { get; set; }
        public int beneficiaryFemale { get; set; }
        public int InDirectBeneficiaryMale { get; set; }
        public int InDirectBeneficiaryFemale { get; set; }
        public string StrengthConsiderationName { get; set; }
        public string GenderConsiderationName { get; set; }
        public string GenderRemarks { get; set; }
        public string SecurityName { get; set; }
        public string[] SecurityConsideration { get; set; }
        public string SecurityRemarks { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }

    public class ProjectOtherDetailNewPdfModel
    {
        public int OpportunityType { get; set; }
        public string Donor { get; set; } 
        public string opportunityNo { get; set; }
        public string opportunity { get; set; }
        public string opportunitydescription { get; set; }
        public string CountryName { get; set; }
        public string[] Province { get; set; }
        public string[] District { get; set; }
        public string OfficeName { get; set; }
        public string SectorName { get; set; }
        public string ProgramName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string projectGoal { get; set; }
        public string projectObjective { get; set; } 
        public DateTime REOIReceiveDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string mainActivities { get; set; }
        public int beneficiaryMale { get; set; }
        public int beneficiaryFemale { get; set; }
        public int InDirectBeneficiaryMale { get; set; }
        public int InDirectBeneficiaryFemale { get; set; }
        public string StrengthConsiderationName { get; set; }
        public string GenderConsiderationName { get; set; }
        public string GenderRemarks { get; set; }
        public string SecurityName { get; set; }
        public string[] SecurityConsideration { get; set; }
        public string SecurityRemarks { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public ProjectPdfFlags flags { get; set; }
    }

    public class ProjectPdfFlags
    {
        public bool opportunitytype {get; set;}
        public bool donor {get; set;}
        public bool opportunityno {get; set;}
        public bool opportunity {get; set;}
        public bool enddate {get; set;}
        public bool opportunitydesc {get; set;}
        public bool country {get; set;}
        public bool province {get; set;}
        public bool district {get; set;}
        public bool office {get; set;}
        public bool sector {get; set;}
        public bool program {get; set;}
        public bool startdate {get; set;}
        public bool projgoal {get; set;}
        public bool projobj {get; set;}
        public bool reoidate {get; set;}
        public bool submissiondate {get; set;}
        public bool mainactivities {get; set;}
        public bool dirbenmale {get; set;}
        public bool dirbenfemale {get; set;}
        public bool indirbenmale {get; set;}
        public bool indirbenfemale {get; set;}
        public bool strengthconsideration {get; set;}
        public bool genderconsideration {get; set;}
        public bool genderremarks {get; set;}
        public bool security {get; set;}
        public bool securityconsideration {get; set;}
        public bool securityremarks {get; set;}
    }
}
