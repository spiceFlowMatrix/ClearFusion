using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetGradeAssociatedAccountQuery : IRequest<ApiResponse>
    {
        public int GradeId { get; set; }
    }
}