using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Command
{
    public class SaveGainLossAccountListCommand: IRequest<ApiResponse>
    {
        public List<long> AccountIds {get; set;}
        public int CurrencyId { get; set; }
        public string UserId {get; set;}
    }
}