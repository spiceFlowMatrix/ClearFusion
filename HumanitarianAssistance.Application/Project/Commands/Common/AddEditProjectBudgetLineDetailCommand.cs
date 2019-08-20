using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectBudgetLineDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? BudgetLineId { get; set; }
        public string BudgetCode { get; set; }
        public string BudgetName { get; set; }
        public double? InitialBudget { get; set; }
        public long? ProjectId { get; set; }
        public long? ProjectJobId { get; set; }
        public string ProjectJobName { get; set; }
        public string ProjectJobCode { get; set; }
        public int? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string BudgetCodeName { get; set; }
        public double? DebitPercentage { get; set; }
        public double? Expenditure { get; set; }
    }
}
