using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class SaveCalculatorConfigDataCommand: BaseModel, IRequest<object>
    {
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ComparisionDate { get; set; }
        public long DebitAccount { get; set; }
        public long CreditAccount { get; set; }
    }
}