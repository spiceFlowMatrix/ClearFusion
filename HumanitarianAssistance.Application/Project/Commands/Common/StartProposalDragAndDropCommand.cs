using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;


namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class StartProposalDragAndDropCommand : BaseModel, IRequest<ApiResponse>
    {
        public string logginUserEmailId { get; set; }
        public string ext { get; set; }
        public IFormFile file { get; set; }
        public long ProjectId { get; set; }
        public string FileName { get; set; }
        public string ProposalType { get; set; } 

    }
}
