using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEducationDegreeQuery: BaseModel, IRequest<object>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}