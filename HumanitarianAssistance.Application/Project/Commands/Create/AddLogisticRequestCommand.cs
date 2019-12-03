using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
        public string Description { get; set; }
        public long BudgetLineId { get; set; }
        public int OfficeId { get; set; }
        public int CurrencyId { get; set; }
        public double TotalCost { get; set; }
        public int Status { get; set; }
        public int ComparativeStatus { get; set; }
        public List<RequestedLogisticItems> RequestedItems { get; set; }
    }

    public class RequestedLogisticItems {
        public long ItemId { get; set; }
        public long RequestedUnits { get; set; }
        public double EstimatedUnitCost { get; set; }
    }
}
