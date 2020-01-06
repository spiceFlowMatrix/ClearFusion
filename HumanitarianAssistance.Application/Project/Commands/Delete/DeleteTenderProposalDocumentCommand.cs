using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteTenderProposalDocumentCommand: BaseModel, IRequest<ApiResponse>
    {
        public long docTypeId { get; set; }
    }
}