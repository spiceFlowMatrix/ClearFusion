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
    public class EditActivitiesControlCommandHandler : IRequestHandler<EditActivitiesControlCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EditActivitiesControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EditActivitiesControlCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var activitiesDetail = await _dbContext.ProjectActivitiesControl
                                                 .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id);

                if (activitiesDetail == null)
                {
                    throw new Exception(StaticResource.ActivitiesControlNotfound);
                }


                activitiesDetail.UserID = request.UserId;
                activitiesDetail.RoleId = request.RoleId;

                activitiesDetail.ModifiedDate = request.ModifiedDate;
                activitiesDetail.ModifiedById = request.CreatedById;
                activitiesDetail.IsDeleted = false;

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
    }
}
