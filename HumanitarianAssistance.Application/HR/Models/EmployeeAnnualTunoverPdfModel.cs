namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeAnnualTunoverPdfModel
    {
        public int SerialNumber { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Office { get; set; }
        public string EmployeeStatus { get; set; }
        public string Tenure { get; set; }
        // public string ReasonForLeaving { get; set; }
         public string Remarks { get; set; }
        public string LogoPath { get; set; }
        public string PersianChaName { get; set; }
        public ReasonForLeavingModel ReasonForLeavingDetails{ get; set; }
    }
}

public class ReasonForLeavingModel
    {
        public bool Benefits { get; set; }
        public bool BetterJobOpportunity { get; set; }
        public bool FamilyReasons { get; set; }
        public bool NotChallenged { get; set; }
        public bool Pay { get; set; }
        public bool PersonalReasons { get; set; }
        public bool Relocation { get; set; }
        public bool ReturnToSchool { get; set; }
        public bool ConflictWithSuoervisors { get; set; }
        public bool ConflictWithOther { get; set; }
        public bool WorkRelationship { get; set; }
        public bool CompanyInstability { get; set; }
        public bool CareerChange { get; set; }
        public bool HealthIssue { get; set; }
    }