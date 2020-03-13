using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredBudegtListQuery: IRequest<object>
    {
         public string FilterValue { get; set; }
          public long ProjectId { get; set; }
    }
}