using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using System.Collections.Generic;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Project;
using AutoMapper;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectSubActivityDetailCommandHandler : IRequestHandler<AddProjectSubActivityDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectSubActivityDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddProjectSubActivityDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail obj = _mapper.Map<AddProjectSubActivityDetailCommand, ProjectActivityDetail>(request);
                //Note : check for all the subactivity are completed or not.
                ProjectActivityDetail parent = await _dbContext.ProjectActivityDetail
                                                                        .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                  x.ActivityId == request.ParentId &&
                                                                                                  x.StatusId == (int)ProjectPhaseType.Completed);
                obj.StatusId = (int)ProjectPhaseType.Planning;
                obj.CreatedDate =request.CreatedDate;
                obj.IsDeleted = false;
                obj.CreatedById = request.CreatedById;
                await _dbContext.ProjectActivityDetail.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                ProjectActivityModel actvityModel = new ProjectActivityModel()
                {
                    ActivityDescription = obj.ActivityDescription,
                    ActivityId = obj.ActivityId,
                    StatusId = obj.StatusId,
                    ActivityName = obj.ActivityName,
                    ActualEndDate = obj.ActualEndDate,
                    ActualStartDate = obj.ActualStartDate,
                    BudgetLineId = obj.BudgetLineId,
                    ParentId = obj.ParentId,
                    Target = obj.Target,
                    PlannedEndDate = obj.PlannedEndDate,
                    PlannedStartDate = obj.PlannedStartDate,
                    EmployeeID = obj.EmployeeID,
                    SubActivityTitle = obj.SubActivityTitle

                };

                if (parent != null)
                {
                    parent.StatusId = (int)ProjectPhaseType.Implementation;
                    await _dbContext.SaveChangesAsync();
                    actvityModel.StatusId = parent.StatusId;
                }
                response.data.ProjectActivityModel = actvityModel;
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
