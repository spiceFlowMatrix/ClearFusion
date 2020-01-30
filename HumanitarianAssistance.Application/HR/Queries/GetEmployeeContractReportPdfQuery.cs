using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeContractReportPdfQuery: IRequest<byte[]>
    {
        public int ContractId { get; set; }
    }
}