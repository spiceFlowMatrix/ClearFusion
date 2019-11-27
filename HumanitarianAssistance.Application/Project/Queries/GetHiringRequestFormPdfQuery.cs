using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetHiringRequestFormPdfQuery : IRequest<byte[]> {
        public long? ProjectId { get; set; }
        public long? HiringRequestId { get; set; }
    }
}