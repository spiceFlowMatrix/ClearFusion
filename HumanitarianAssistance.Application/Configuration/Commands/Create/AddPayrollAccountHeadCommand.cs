using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddPayrollAccountHeadCommand : BaseModel, IRequest<ApiResponse>
    {
        public int PayrollHeadId { get; set; }
        public int PayrollHeadTypeId { get; set; }
        public string PayrollHeadName { get; set; }
        public string Description { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public decimal? Amount { get; set; }
        public int? OfficeId { get; set; }
    }
}
