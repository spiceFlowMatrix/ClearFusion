using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherListQuery : IRequest<ApiResponse>
    {
        public string FilterValue { get; set; }
        public bool VoucherNoFlag { get; set; }
        public bool ReferenceNoFlag { get; set; }
        public bool DescriptionFlag { get; set; }
        public bool JournalNameFlag { get; set; }
        public bool DateFlag { get; set; }

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
    }
}