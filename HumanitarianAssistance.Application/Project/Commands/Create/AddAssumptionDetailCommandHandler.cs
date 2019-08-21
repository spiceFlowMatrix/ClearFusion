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
    public class AddAssumptionDetailCommandHandler : IRequestHandler<AddAssumptionDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddAssumptionDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddAssumptionDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                CEAssumptionDetail _detail = new CEAssumptionDetail
                {
                    AssumptionDetailId = request.AssumptionDetailId.Value,
                    Name = request.Name,
                    ProjectId = request.ProjectId,
                    IsDeleted = false,
                    CreatedById = request.CreatedById,
                    CreatedDate = DateTime.UtcNow
                };
                await _dbContext.CEAssumptionDetail.AddAsync(_detail);
                await _dbContext.SaveChangesAsync();
                response.CommonId.LongId = _detail.AssumptionDetailId;
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
