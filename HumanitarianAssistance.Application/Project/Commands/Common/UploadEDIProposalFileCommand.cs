using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
   public class UploadEDIProposalFileCommand : BaseModel, IRequest<ApiResponse>
    {
         
            public IFormFile file { get; set; }
            public long  ProjectId { get; set; }
            public string fileName { get; set; }
            public string logginUserEmailId { get; set; }
            public string ProposalType { get; set; }
            public string ext { get; set; }
    }
}
