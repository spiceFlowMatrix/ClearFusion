using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
   public class AddEditFinancialCriteriaCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? FinancialCriteriaDetailId { get; set; }
        public long? ProjectId { get; set; }
        public double? Total { get; set; }

        public double? ProjectActivities { get; set; }
        public double? Operational { get; set; }
        public double? Overhead_Admin { get; set; }
        public double? Lump_Sum { get; set; }
    }
}
