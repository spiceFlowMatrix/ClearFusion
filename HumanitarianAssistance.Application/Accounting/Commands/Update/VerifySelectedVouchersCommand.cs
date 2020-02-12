using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class VerifySelectedVouchersCommand: BaseModel, IRequest<object>
    {
        public List<long> VoucherNos { get; set; }
    }
}