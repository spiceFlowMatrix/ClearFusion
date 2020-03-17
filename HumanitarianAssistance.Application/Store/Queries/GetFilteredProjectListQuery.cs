using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredProjectListQuery: IRequest<object>
    {
         public string FilterValue { get; set; }
        
    }
}