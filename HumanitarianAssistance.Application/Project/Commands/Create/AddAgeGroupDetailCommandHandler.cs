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
    public class AddAgeGroupDetailCommandHandler : IRequestHandler<AddAgeGroupDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddAgeGroupDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddAgeGroupDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                CEAgeGroupDetail _detail = new CEAgeGroupDetail
                {
                    AgeGroupOtherDetailId = request.AgeGroupOtherDetailId.Value,
                    Name = request.Name,
                    ProjectId = request.ProjectId,
                    IsDeleted = false,
                    CreatedById = request.CreatedById,
                    CreatedDate = DateTime.UtcNow
                };

                await _dbContext.CEAgeGroupDetail.AddAsync(_detail);
                await _dbContext.SaveChangesAsync();

                response.CommonId.LongId = _detail.AgeGroupOtherDetailId;
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
