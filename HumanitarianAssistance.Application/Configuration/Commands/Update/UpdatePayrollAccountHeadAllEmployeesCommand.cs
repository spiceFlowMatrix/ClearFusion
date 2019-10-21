using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class UpdatePayrollAccountHeadAllEmployeesCommand : BaseModel, IRequest<ApiResponse>
    {
        public List<PayrollHeadModel> PayrollHead { get; set; }
    }
}
