using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredReceivedFromEmpListQuery: IRequest<object>
    {
         public string FilterValue { get; set; }
         public int OfficeId { get; set; }
    }
}