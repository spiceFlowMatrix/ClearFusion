using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
        public class DeleteContractDetailCommandHandler : IRequestHandler<DeleteContractDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteContractDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(DeleteContractDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var contractInfo = await _dbContext.ContractDetails.FirstOrDefaultAsync(c => c.ContractId == request.ContractId);
                contractInfo.IsDeleted = true;
                contractInfo.ModifiedById = request.ModifiedById;
                contractInfo.ModifiedDate = request.ModifiedDate;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Contract Deleted Successfully";
                response.data.jobListTotalCount = _dbContext.ContractDetails.Count(x => x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
    }
    }
}