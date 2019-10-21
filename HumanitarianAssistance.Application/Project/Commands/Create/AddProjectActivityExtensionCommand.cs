using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectActivityExtensionCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ExtensionId { get; set; }

        public long ActivityId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
