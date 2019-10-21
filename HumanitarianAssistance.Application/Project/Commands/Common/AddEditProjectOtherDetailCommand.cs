using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectOtherDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectOtherDetailId { get; set; }
        public long ProjectId { get; set; }
        public string opportunityNo { get; set; }
        public string opportunity { get; set; }
        public string opportunitydescription { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictID { get; set; }
        public int? OfficeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CurrencyId { get; set; }
        public string budget { get; set; }
        public int? beneficiaryMale { get; set; }
        public int? beneficiaryFemale { get; set; }
        public string projectGoal { get; set; }
        public string projectObjective { get; set; }
        public string mainActivities { get; set; }
        public long? DonorId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? REOIReceiveDate { get; set; }
        public long? StrengthConsiderationId { get; set; }
        public long? GenderConsiderationId { get; set; }
        public string GenderRemarks { get; set; }
        public long? SecurityId { get; set; }
        public string SecurityConsiderationId { get; set; }
        public string SecurityRemarks { get; set; }
        public int? InDirectBeneficiaryFemale { get; set; }
        public int? InDirectBeneficiaryMale { get; set; }
        public int? OpportunityType { get; set; }
        
    }
}