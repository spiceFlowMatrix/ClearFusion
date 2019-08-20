using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllDistrictByProvinceIdQuery: IRequest<ApiResponse>
    {
        public int[] ProvinceId { get; set; }
    }
}