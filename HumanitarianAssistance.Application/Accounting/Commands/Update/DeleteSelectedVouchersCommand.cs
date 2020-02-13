using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class DeleteSelectedVouchersCommand: BaseModel, IRequest<object>
    {
        public List<long> VoucherNoList { get; set; }
    }
}