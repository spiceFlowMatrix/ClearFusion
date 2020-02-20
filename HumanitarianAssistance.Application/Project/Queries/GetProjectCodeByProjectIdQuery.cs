using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectCodeByProjectIdQuery: IRequest<object>
    {
        public long ProjectId { get; set; }
    }
}