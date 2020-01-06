using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetRequestDetailByIdQuery: IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
    }

    public class LogisticsRequestsDetailModel {
        public long RequestId { get; set; }
        public string Description { get; set; }
        public string RequestCode { get; set; }
        public int Status { get; set; }
        public double TotalCost { get; set; }
        public long ProjectId { get; set; }
        public string BudgetLine { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }   
        public int ComparativeStatus { get; set; }
        public int TenderStatus { get; set; }
        public string ProcessingType { get; set; }
        public long BudgetLineId { get; set; }
        public long CurrencyId { get; set; }
        public long OfficeId { get; set; } 
    }
}