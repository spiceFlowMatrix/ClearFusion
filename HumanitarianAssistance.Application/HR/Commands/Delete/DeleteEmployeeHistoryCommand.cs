using HumanitarianAssistance.Application.Infrastructure;
using MediatR;


namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeHistoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public int HistoryId { get; set; }
    }
}
