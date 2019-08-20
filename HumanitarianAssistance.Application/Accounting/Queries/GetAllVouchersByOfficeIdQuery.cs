using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVouchersByOfficeIdQuery : IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
    }
}