using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetDefaultUnitTypeByItemIdQuery: IRequest<object>
    {
        public int Id { get; set; }
    }
}