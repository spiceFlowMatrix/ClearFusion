using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllRatingBasedCriteriaQuestionsByOfficeIdQuery : IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
    }
}
