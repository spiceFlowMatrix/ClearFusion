using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllProjectFilterListQuery: IRequest<ApiResponse>
    {
        public string FilterValue { get; set; }
        public bool ProjectIdFlag { get; set; }
        public bool ProjectCodeFlag { get; set; }
        public bool ProjectNameFlag { get; set; }
        public bool DescriptionFlag { get; set; }
        public bool? DateFlag { get; set; }


        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
    }
}