using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetTimeCategoryByIdQuery : IRequest<ApiResponse>
    {
        public int TimeCategoryId { get; set; } 
    }
}
