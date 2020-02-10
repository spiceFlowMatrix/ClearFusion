using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class MonthlyPaySlipReportPdfQuery: BaseModel, IRequest<byte[]>
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
    }
}