using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class FilterJobListQuery : IRequest<ApiResponse>
    {
        public bool IsApproved { get; set; }
        public string YesOrNo { get; set; }
        public double UnitRate { get; set; }
        public double TotalPrice { get; set; }
        public long ContractId { get; set; }
        public string FilterType { get; set; }
        public string JobName { get; set; }
        public long JobId { get; set; }

    }
}
