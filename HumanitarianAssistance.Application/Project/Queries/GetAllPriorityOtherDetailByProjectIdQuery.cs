using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Queries
{
  public class GetAllPriorityOtherDetailByProjectIdQuery : IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}
