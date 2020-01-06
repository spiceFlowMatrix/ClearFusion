using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteTenderBidByIdCommand: BaseModel, IRequest<ApiResponse>
    {
        public long BidId { get; set; }
    }
}