using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddFinancialYearDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public int FinancialYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FinancialYearName { get; set; }
        public string Description { get; set; }
        public Boolean IsDefault { get; set; }
    }
}