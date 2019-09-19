using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetallHiringRequestDetailQuery : IRequest<ApiResponse>
    {      
        public long? ProjectId { get; set; }      
        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }       
    }
}
