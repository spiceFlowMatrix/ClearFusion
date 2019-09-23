using System.Collections.Generic;
using HumanitarianAssistance.Application.Configuration.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetOfficeListAssignedToUserQuery: IRequest<List<OfficeDetailModel>>
    {
        public string UserId { get; set; }
    }
}