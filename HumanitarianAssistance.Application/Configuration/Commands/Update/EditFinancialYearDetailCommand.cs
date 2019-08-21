using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
   public class EditFinancialYearDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public int FinancialYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FinancialYearName { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}
