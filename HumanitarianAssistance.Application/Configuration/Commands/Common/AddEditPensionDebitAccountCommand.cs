using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Common
{
    public class AddEditPensionDebitAccountCommand : BaseModel, IRequest<ApiResponse>
    {
        public long accountId { get; set; } 
    }
}
