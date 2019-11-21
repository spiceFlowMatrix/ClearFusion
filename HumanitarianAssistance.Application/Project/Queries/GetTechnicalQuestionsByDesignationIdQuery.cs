using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetTechnicalQuestionsByDesignationIdQuery: IRequest<ApiResponse>
    {
        public int DesignationId { get; set; }
    }
}