using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetGeneratorListQuery:IRequest<GeneratorTrackerListModel>
    {
        public double? TotalCost { get; set; }
        public int? Voltage { get; set; }
        public int? ModelYear { get; set; }
        public int? OfficeId { get; set; }

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
    }
}