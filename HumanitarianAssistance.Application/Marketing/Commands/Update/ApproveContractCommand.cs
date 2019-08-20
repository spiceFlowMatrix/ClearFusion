using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class ApproveContractCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? ContractId { get; set; }
        public string Type { get; set; }
    }
}
