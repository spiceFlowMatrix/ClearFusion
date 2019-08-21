using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllAccountTypeByCategoryQuery: IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}