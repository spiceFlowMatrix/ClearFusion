using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class SelectTenderBidCommand : BaseModel, IRequest<ApiResponse>
    {
        public long BidId { get; set; }   
    }
}
