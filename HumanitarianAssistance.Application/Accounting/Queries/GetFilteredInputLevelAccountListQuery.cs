using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetFilteredInputLevelAccountListQuery: IRequest<object>
    {
        public string FilterValue { get; set; }
    }
}