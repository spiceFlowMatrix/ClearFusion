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
    public class AddActivitiesControlCommandHandler : IRequestHandler<AddActivitiesControlCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddActivitiesControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddActivitiesControlCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivitiesControl obj = new ProjectActivitiesControl
                {
                    ProjectId = request.ProjectId,
                    UserID = request.UserId,
                    RoleId = request.UserId,
                    CreatedDate = request.CreatedDate,
                    CreatedById = request.CreatedById,
                };

                // validation
                await ValidateActivitiesControl(request.ProjectId, request.UserId, request.RoleId);

                await _dbContext.ProjectActivitiesControl.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                response.CommonId.LongId = obj.Id;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<bool> ValidateActivitiesControl(long projectId, int userId, int roleId)
        {
            bool validateActivities = await _dbContext.ProjectActivitiesControl
                                                           .AnyAsync(x => x.IsDeleted == false &&
                                                                          x.ProjectId == projectId &&
                                                                          x.UserID == userId &&
                                                                          x.RoleId == roleId);
            if (validateActivities)
            {
                throw new Exception(StaticResource.sameRoleAlreadyExistForTheUser);
            }

            return true;
        }
    }
}
