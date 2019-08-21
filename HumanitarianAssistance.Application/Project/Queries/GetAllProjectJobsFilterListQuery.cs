using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
   public  class GetAllProjectJobsFilterListQuery : IRequest<ApiResponse>
    {
        public string FilterValue { get; set; }
        public bool? ProjectJobNameFlag { get; set; }
        public bool? DateFlag { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
        public long? ProjectId { get; set; }
    }
}
