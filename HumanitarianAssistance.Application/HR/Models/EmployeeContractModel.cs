using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeContractModel
    {
        public int? EmployeeContractId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string EmployeeCode { get; set; }
        public int? DesignationId { get; set; }
        public string Designation { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? DurationOfContract { get; set; }
        public double? Salary { get; set; }
        public int? Grade { get; set; }
        public string GradeName { get; set; }
        public string ProjectName { get; set; }
        public long Project { get; set; }
        public string ProjectCode { get; set; }
        public int? DutyStationId { get; set; }
        public string DutyStation { get; set; }
        public int? CountryId { get; set; }
        public string Country { get; set; }
        public int? ProvinceId { get; set; }
        public string Province { get; set; }
        public long? BudgetLineId { get; set; }
        public string BudgetLine { get; set; }
        public int? JobId { get; set; }
        public string Job { get; set; }
        public int? WorkTime { get; set; }
        public int? WorkDayHours { get; set; }
        public string ContentEnglish { get; set; }
        public string ContentDari { get; set; }
        public string EmployeeImage { get; set; }
        public string CountryDari { get; set; }
        public string DesignationDari { get; set; }
        public string DutyStationDari { get; set; }
        public string FatherNameDari { get; set; }
        public string GradeDari { get; set; }
        public string JobDari { get; set; }
        public string ProvinceDari { get; set; }
        public string EmployeeNameDari { get; set; }
        public string ProjectNameDari { get; set; }
        public string BudgetLineDari { get; set; }
    }
}