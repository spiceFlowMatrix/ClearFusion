using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllProvincesByCountryIdQuery: IRequest<ApiResponse>
    {
        public int[] CountryId {get; set;}
    }
}