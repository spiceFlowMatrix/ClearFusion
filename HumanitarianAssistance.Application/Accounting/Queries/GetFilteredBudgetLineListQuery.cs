using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetFilteredBudgetLineListQuery: IRequest<object>
    {
        public long ProjectId { get; set; }
        public string FilterValue { get; set; }
    }
}