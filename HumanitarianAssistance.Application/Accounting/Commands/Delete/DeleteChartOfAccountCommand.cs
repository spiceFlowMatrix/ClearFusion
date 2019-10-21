using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Delete
{
    public class DeleteChartOfAccountCommand : BaseModel, IRequest<ApiResponse>
    {
        public long AccountId { get; set; }
    }
}