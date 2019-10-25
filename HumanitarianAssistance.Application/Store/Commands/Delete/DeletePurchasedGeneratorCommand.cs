using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchasedGeneratorCommand:BaseModel,IRequest<bool>
    {
        public long PurchasedGeneratorId { get; set; }
        
    }
}