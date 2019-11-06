using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllJobListQuery: IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }  
         public int ProfessionId { get; set; }
         public int OfficeId { get; set; }
    }
}