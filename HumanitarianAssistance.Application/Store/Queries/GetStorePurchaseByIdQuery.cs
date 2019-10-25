using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStorePurchaseByIdQuery: IRequest<StorePurchaseModel>
    {
        public long PurchaseId { get; set; }
    }
}