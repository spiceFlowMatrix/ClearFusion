using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using System.Collections.Generic;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllDonorEligibilityDetailQueryHandler : IRequestHandler<GetAllDonorEligibilityDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllDonorEligibilityDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllDonorEligibilityDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<DonorEligibilityCriteria> list = await _dbContext.DonorEligibilityCriteria.Where(x => x.IsDeleted == false)
                                                                                 .OrderByDescending(x => x.DonorEligibilityDetailId)
                                                                                 .ToListAsync();

                response.data.DonorEligibilityCriteria = list;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
