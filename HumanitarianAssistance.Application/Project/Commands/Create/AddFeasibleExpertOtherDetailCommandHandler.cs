using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
  public  class AddFeasibleExpertOtherDetailCommandHandler : IRequestHandler<AddFeasibleExpertOtherDetailCommand, ApiResponse>
    {
        
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddFeasibleExpertOtherDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
        public async Task<ApiResponse> Handle(AddFeasibleExpertOtherDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                CEFeasibilityExpertOtherDetail _detail = new CEFeasibilityExpertOtherDetail
                {
                    ExpertOtherDetailId = request.ExpertOtherDetailId.Value,
                    Name = request.Name,
                    ProjectId = request.ProjectId,
                    IsDeleted = false,
                    CreatedById = request.CreatedById,
                    CreatedDate = DateTime.UtcNow
                };

                await _dbContext.CEFeasibilityExpertOtherDetail.AddAsync(_detail);
                await _dbContext.SaveChangesAsync();
                response.CommonId.LongId = _detail.ExpertOtherDetailId;
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
