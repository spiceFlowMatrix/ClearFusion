using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllLogisticRequestQuery : IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}
