using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class FilterProjectCashFlowQuery : IRequest<ApiResponse>
    {
        public List<long> ProjectId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public long DonorID { get; set; }
    }
}
