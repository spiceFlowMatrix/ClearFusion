using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class VerifyVoucherCommand : BaseModel, IRequest<ApiResponse>
    {
        public long VoucherId { get; set; }
    }
}