using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
   public class GetAllAgeGroupByProjectIdQuery : IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }

    }
}
