using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteContractDetailCommand: BaseModel, IRequest<ApiResponse>
    {
     public int ContractId { get; set; }   
    }
}