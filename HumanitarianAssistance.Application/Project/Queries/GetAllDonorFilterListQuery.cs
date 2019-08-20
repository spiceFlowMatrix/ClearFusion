using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllDonorFilterListQuery : IRequest<ApiResponse>
    {
        public string FilterValue { get; set; }
        public bool? DonorNameFlag { get; set; }
        public bool? DateFlag { get; set; }

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; } 
    }
}