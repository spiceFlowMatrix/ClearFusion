using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeContractCommand: BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeContractId { get; set; }
        public int EmployeeId { get; set; }
        public string FatherName { get; set; }
        public string EmployeeCode { get; set; }
        public int? Designation { get; set; } //id
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? DurationOfContract { get; set; }
        public double? Salary { get; set; }
        public int? Grade { get; set; }
        public int? DutyStation { get; set; } //if
        public int? Country { get; set; } //id
        public int? Province { get; set; } //id
        public int? Project { get; set; }
        public long? BudgetLine { get; set; }
        public string Job { get; set; }
        public int? WorkTime { get; set; }
        public int? WorkDayHours { get; set; }
        public string ContractStatus { get; set; }
        public string PeriodType { get; set; }
        public float? ContractNumber { get; set; }
        public float? ContractPeriod { get; set; }
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