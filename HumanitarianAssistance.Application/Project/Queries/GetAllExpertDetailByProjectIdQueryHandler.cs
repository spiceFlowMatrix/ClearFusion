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

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllExpertDetailByProjectIdQueryHandler : IRequestHandler<GetAllExpertDetailByProjectIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllExpertDetailByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllExpertDetailByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.CEFeasibilityExpertOtherDetail.Where(x => x.IsDeleted == false && 
                                                                                      x.ProjectId == request.ProjectId)
                                                                          .OrderByDescending(x => x.ExpertOtherDetailId)
                                                                          .ToListAsync();

                response.data.FeasibilityExpertOtherDetail = list;
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
