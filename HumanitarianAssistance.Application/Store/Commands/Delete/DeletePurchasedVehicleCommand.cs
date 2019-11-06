using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchasedVehicleCommand:BaseModel,IRequest<bool>
    {
        public long PurchasedVehicleId {get; set;}
    }
}