using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class ClosedHiringRequestCommandHandler : IRequestHandler<ClosedHiringRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public ClosedHiringRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(ClosedHiringRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<ProjectHiringRequestDetail> selelctedRequest = new List<ProjectHiringRequestDetail>();
                if (request.HiringRequestId != null)
                {
                    var HiringRequestDetail = await _dbContext.ProjectHiringRequestDetail
                                                                         .Where(x => x.IsDeleted == false
                                                                         && x.ProjectId == request.ProjectId
                                                                         && x.HiringRequestStatus == (int)HiringRequestStatus.Open || x.HiringRequestStatus == (int)HiringRequestStatus.InProgress).ToListAsync();

                    selelctedRequest = HiringRequestDetail.Where(x => request.HiringRequestId.Contains(x.HiringRequestId)).ToList();
                    foreach (var item in selelctedRequest)
                    {
                        item.HiringRequestStatus = (int)HiringRequestStatus.Closed;
                        item.ModifiedById = request.ModifiedById;
                        item.ModifiedDate = request.ModifiedDate;
                    }
                    _dbContext.ProjectHiringRequestDetail.UpdateRange(selelctedRequest);

                    await _dbContext.SaveChangesAsync();
                }
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