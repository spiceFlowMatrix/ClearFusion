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

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class CompleteHiringRequestCommandHandler : IRequestHandler<CompleteHiringRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public CompleteHiringRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(CompleteHiringRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectHiringRequestDetail projectHiringRequestDetail = new ProjectHiringRequestDetail();
                if (request.hiringRequestId != 0)
                {
                    projectHiringRequestDetail = await _dbContext.ProjectHiringRequestDetail
                                                                          .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                    x.HiringRequestId == request.hiringRequestId);

                    projectHiringRequestDetail.IsCompleted = true;
                    projectHiringRequestDetail.ModifiedById = request.ModifiedById;
                    projectHiringRequestDetail.ModifiedDate = request.ModifiedDate;
                    await _dbContext.SaveChangesAsync();
                }
                response.ResponseData = projectHiringRequestDetail;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
