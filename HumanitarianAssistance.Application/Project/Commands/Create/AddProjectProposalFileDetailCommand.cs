using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectProposalFileDetailCommand: BaseModel, IRequest<object>
    {
        public long ProjectId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int ProposalTypeId { get; set; }
    }
}