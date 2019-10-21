using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditAdvanceCommand: BaseModel, IRequest<ApiResponse>
    {
        public int? AdvancesId { get; set; }
        public DateTime AdvanceDate { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public int CurrencyId { get; set; }
        public long? VoucherReferenceNo { get; set; }
        public string Description { get; set; }
        public string ModeOfReturn { get; set; }
        public int ApprovedBy { get; set; }
        public double? RequestAmount { get; set; }
        public double AdvanceAmount { get; set; }
        public int OfficeId { get; set; }
        public int? NumberOfInstallments { get; set; }
    }
}