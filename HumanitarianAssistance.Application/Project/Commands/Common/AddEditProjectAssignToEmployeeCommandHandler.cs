using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectAssignToEmployeeCommandHandler: IRequestHandler<AddEditProjectAssignToEmployeeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public AddEditProjectAssignToEmployeeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(AddEditProjectAssignToEmployeeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.ProjectAssignToId > 0)
                {
                    ProjectAssignTo obj = _mapper.Map<AddEditProjectAssignToEmployeeCommand, ProjectAssignTo>(request);
                    obj.ProjectId = request.ProjectId;
                    obj.EmployeeId = request.EmployeeId;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    await _dbContext.ProjectAssignTo.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                }
                else
                {
                    var existRecord = await _dbContext.ProjectAssignTo.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectAssignToId == request.ProjectAssignToId && x.ProjectId == request.ProjectId);
                    if (existRecord != null)
                    {
                        _mapper.Map(request, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = request.ModifiedById;
                        existRecord.ModifiedDate = DateTime.UtcNow;
                        _dbContext.ProjectAssignTo.Update(existRecord);
                        await _dbContext.SaveChangesAsync();
                    }
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