using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{
    public class ReverseEmployeeSalaryVoucherCommand : IRequest<ApiResponse>
    {
        public long VoucherNo { get; set; }
    }
}