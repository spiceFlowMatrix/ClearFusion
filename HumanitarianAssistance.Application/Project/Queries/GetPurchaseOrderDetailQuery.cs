using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetPurchaseOrderDetailQuery: IRequest<ApiResponse>
    {
        public long requestId { get; set; }
    }

    public class PurchaseOrderDetailModel{
        public string PurchasedDate { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }
        public string BudgetLine { get; set; }
        public string ProjectJob { get; set; }
        public string Project { get; set; }
        public double TotalCost { get; set; }
    }
}