using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
        public class DeleteActivitiesControlCommandHandler : IRequestHandler<DeleteActivitiesControlCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteActivitiesControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteActivitiesControlCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var activitiesDetail = await _dbContext.ProjectActivitiesControl
                                                 .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.id);

                if (activitiesDetail == null)
                {
                    throw new Exception(StaticResource.ActivitiesControlNotfound);
                }


                activitiesDetail.ModifiedDate = request.ModifiedDate;
                activitiesDetail.ModifiedById = request.ModifiedById;
                activitiesDetail.IsDeleted = true;

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
