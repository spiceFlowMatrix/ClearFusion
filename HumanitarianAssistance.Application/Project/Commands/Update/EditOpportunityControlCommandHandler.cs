using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditOpportunityControlCommandHandler : IRequestHandler<EditOpportunityControlCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EditOpportunityControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EditOpportunityControlCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var opportunityDetail = await _dbContext.ProjectOpportunityControl
                                                  .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                // validation
                await ValidateOpportunityControl(opportunityDetail.Id, request.ProjectId, request.UserId, request.RoleId);


                opportunityDetail.UserID = request.UserId;
                opportunityDetail.RoleId = request.RoleId;

                opportunityDetail.ModifiedDate = request.ModifiedDate;
                opportunityDetail.ModifiedById = request.ModifiedById;
                opportunityDetail.IsDeleted = false;

                await _dbContext.SaveChangesAsync();

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
