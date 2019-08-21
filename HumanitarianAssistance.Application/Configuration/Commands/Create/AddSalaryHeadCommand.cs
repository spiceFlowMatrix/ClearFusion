using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
   public class AddSalaryHeadCommand : BaseModel, IRequest<ApiResponse>
    {
        public int SalaryHeadId { get; set; }
        public int HeadTypeId { get; set; }
        public string HeadName { get; set; }
        public string Description { get; set; }
        public long AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public bool SaveForAll { get; set; }
    }
}
