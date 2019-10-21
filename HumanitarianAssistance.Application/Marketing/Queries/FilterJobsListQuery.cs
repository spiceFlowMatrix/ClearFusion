using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class FilterJobsListQuery : IRequest<ApiResponse>
    {
        public bool FinalPrice { get; set; }
        public string Value { get; set; }
        public bool JobId { get; set; }
        public bool JobName { get; set; }
        public bool Approved { get; set; }
    }
}
