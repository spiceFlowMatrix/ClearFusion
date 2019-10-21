using AutoMapper;
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
    public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeletePolicyCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var policyInfo = await _dbContext.PolicyDetails.FirstOrDefaultAsync(c => c.PolicyId == request.PolicyId);
                policyInfo.IsDeleted = true;
                policyInfo.ModifiedById = request.ModifiedById;
                policyInfo.ModifiedDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Policy Deleted Successfully";
                int totalCount = await _dbContext.PolicyDetails
                                      .Where(v => v.IsDeleted == false)
                                     .AsNoTracking()
                                     .CountAsync();
                response.data.TotalCount = totalCount;
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
