using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddOpportunityControlCommandHandler : IRequestHandler<AddOpportunityControlCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddOpportunityControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddOpportunityControlCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectOpportunityControl obj = new ProjectOpportunityControl
                {
                    ProjectId = request.ProjectId,
                    UserID = request.UserId,
                    RoleId = request.UserId,
                    CreatedDate = request.CreatedDate,
                    CreatedById = request.CreatedById,
                    IsDeleted = false
                };
                // validation
                await ValidateOpportunityControl(null, request.ProjectId, request.UserId, request.RoleId);
                await _dbContext.ProjectOpportunityControl.AddAsync(obj);
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

        public async Task<bool> ValidateOpportunityControl(long? opportunityControlId, long projectId, int userId, int roleId)
        {
            bool validateOpportunity = await _dbContext.ProjectOpportunityControl
                                                           .AnyAsync(x => x.IsDeleted == false &&
                                                                          x.ProjectId == projectId &&
                                                                          (opportunityControlId != null ? x.Id != opportunityControlId : true) &&
                                                                          x.UserID == userId &&
                                                                          x.RoleId == roleId);
            if (validateOpportunity)
            {
                throw new Exception(StaticResource.sameRoleAlreadyExistForTheUser);
            }

            return true;
        }
    }
}
