using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetRemainingVacancyByJobIdQuery: IRequest<ApiResponse>
    {
        public long? JobId { get; set; }
    }
}