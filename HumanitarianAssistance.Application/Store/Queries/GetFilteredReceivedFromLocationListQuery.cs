using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredReceivedFromLocationListQuery: IRequest<object>
    {
         public string FilterValue { get; set; }
    }
}