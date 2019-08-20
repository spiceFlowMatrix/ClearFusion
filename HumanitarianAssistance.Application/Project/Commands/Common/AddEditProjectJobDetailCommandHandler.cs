using AutoMapper;
using HumanitarianAssistance.Application.CommonServices;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectJobDetailCommandHandler : IRequestHandler<AddEditProjectJobDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProjectServices _iProjectServices;
        public AddEditProjectJobDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iProjectServices = iProjectServices;
        }
        public async Task<ApiResponse> Handle(AddEditProjectJobDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.ProjectId != 0)
                {
                    ProjectJobDetail obj = _mapper.Map<AddEditProjectJobDetailCommand, ProjectJobDetail>(request);

                    if (request.ProjectJobId == 0)
                    {
                        obj.CreatedDate = request.CreatedDate;
                        obj.IsDeleted = false;
                        obj.CreatedById = request.CreatedById;

                        await _dbContext.ProjectJobDetail.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                        if (obj.ProjectJobId != 0)
                        {
                            // update project job code
                            obj.ProjectJobCode = await _iProjectServices.GetProjectJobCode(obj);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        ProjectJobDetail projectJobDetail = await _dbContext.ProjectJobDetail.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                                                x.ProjectJobId == request.ProjectJobId);
                        if (projectJobDetail != null)
                        {
                            projectJobDetail.ProjectJobCode = obj.ProjectJobCode;
                            projectJobDetail.ProjectJobName = obj.ProjectJobName;
                            projectJobDetail.ProjectJobId = obj.ProjectJobId;
                            projectJobDetail.ProjectId = obj.ProjectId;
                            obj.ModifiedById = request.ModifiedById;
                            obj.ModifiedDate = request.ModifiedDate;
                            obj.IsDeleted = false;
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.projectNotFound;
                }
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
