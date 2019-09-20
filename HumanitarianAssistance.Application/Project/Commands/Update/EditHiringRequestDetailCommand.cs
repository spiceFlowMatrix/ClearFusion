using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditHiringRequestDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? HiringRequestId { get; set; }
        public string HiringRequestCode { get; set; }
        public string Description { get; set; }
        public int? ProfessionId { get; set; }
        public string Position { get; set; }
        public int? TotalVacancies { get; set; }
        public int? FilledVacancies { get; set; }
        public double? BasicPay { get; set; }
        public long? BudgetLineId { get; set; }
        public int OfficeId { get; set; }
        public int? GradeId { get; set; }
        public int? EmployeeID { get; set; }
        public long? ProjectId { get; set; }
        public bool IsCompleted { get; set; }
        public int? CurrencyId { get; set; }
        public string RequestedBy { get; set; }
        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
        public string JobCategory { get; set; }
        public string MinimumEducationLevel { get; set; }
        public string Organization { get; set; }
        public int? ProvinceId { get; set; }
        public string ContractType { get; set; }
        public int? ContractDuration { get; set; }
        public int? GenderId { get; set; }
        public string SalaryRange { get; set; }
        public DateTime? AnouncingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? CountryId { get; set; }
        public int? JobType { get; set; }
        public int? Shift { get; set; }
        public string JobStatus { get; set; }
        public string Experience { get; set; }
        public string Background { get; set; }
        public string SpecificDutiesAndResponsblities { get; set; }
        public string KnowladgeAndSkillRequired { get; set; }
        public string SubmissionGuidlines { get; set; }
    }
}
