using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetSelectedBidDetailQuery: IRequest<ApiResponse>
    {
        public long RequestId { get; set;}
    }

    public class SelectedBidDetailModel 
    {
        public string ContactName { get; set;}
        public string SelectedBy { get; set;}
    }
}