using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeLeavePdfQueryHandler : IRequestHandler<GetAllEmployeeLeavePdfQuery, ApiResponse>
    {
        public async Task<ApiResponse> Handle(GetAllEmployeeLeavePdfQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}