using System;

namespace HumanitarianAssistance.Application.CommonModels
{
    public class ProjectBudgetLineDetailModel
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
        public DateTime? CreatedDate { get; set; }
        public double? Expenditure { get; set; }

        // for dropdown
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}