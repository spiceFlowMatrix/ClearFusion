using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetContractTypeContentQueryHandler: IRequestHandler<GetContractTypeContentQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetContractTypeContentQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetContractTypeContentQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.data.ContractTypeContentList = await _dbContext.ContractTypeContent.FirstOrDefaultAsync(x => x.EmployeeContractTypeId == request.EmployeeContractTypeId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}