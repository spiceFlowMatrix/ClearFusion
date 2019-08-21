using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllDistrictDetailByProvinceIdQuery : IRequest<ApiResponse>
    {
        public List<int?> ProvinceId { get; set; }
    }
}
