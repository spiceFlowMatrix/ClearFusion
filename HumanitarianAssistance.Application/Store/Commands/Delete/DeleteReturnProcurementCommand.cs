using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteReturnProcurementCommand:BaseModel,IRequest<object>
    {
        public long Id { get; set; }
    }
}