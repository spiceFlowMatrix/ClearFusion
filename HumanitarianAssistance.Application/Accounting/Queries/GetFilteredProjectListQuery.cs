using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetFilteredProjectListQuery: IRequest<object>
    {
        public string FilterValue { get; set; }
    }
}