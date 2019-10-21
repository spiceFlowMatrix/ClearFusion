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
    public class DeleteProjectJobCommandHandler : IRequestHandler<DeleteProjectJobCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteProjectJobCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteProjectJobCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var projectJobDetails = await _dbContext.ProjectJobDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectJobId == request.JobId);
                if (projectJobDetails != null)
                {
                    projectJobDetails.ModifiedById = request.ModifiedById;
                    projectJobDetails.ModifiedDate = request.ModifiedDate;
                    projectJobDetails.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Poject Job deleted successfully";
                }
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
