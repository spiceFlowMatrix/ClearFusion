using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class ProjectActivityReportPdfQuery : IRequest<byte[]>
    {
        public long  ProjectId { get; set; }  
        public List<long> ActivityId { get; set; }          
    }
}
