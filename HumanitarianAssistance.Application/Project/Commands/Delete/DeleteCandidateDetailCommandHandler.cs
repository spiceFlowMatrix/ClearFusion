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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteCandidateDetailCommandHandler : IRequestHandler<DeleteCandidateDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteCandidateDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteCandidateDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                HiringRequestCandidates projectCandidateDetail = new HiringRequestCandidates();
                ProjectHiringRequestDetail hrDetail = new ProjectHiringRequestDetail();
                if (request.HiringRequestId != 0)
                {
                    projectCandidateDetail = await _dbContext.HiringRequestCandidates
                                                                      .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                x.HiringRequestId == request.HiringRequestId &&
                                                                                                x.CandidateId == request.CandidateId);

                    if (projectCandidateDetail != null)
                    {
                        projectCandidateDetail.IsDeleted = true;
                        projectCandidateDetail.ModifiedById = request.ModifiedById;
                        projectCandidateDetail.ModifiedDate = request.ModifiedDate;
                        await _dbContext.SaveChangesAsync();

                        // note: to update filled vacancire in hiring request detail page

                        hrDetail = await _dbContext.ProjectHiringRequestDetail
                                                            .FirstOrDefaultAsync(x => x.HiringRequestId == request.HiringRequestId &&
                                                                                      x.IsDeleted == false);
                        if (hrDetail == null)
                        {
                            throw new Exception("Hiring Job not found");
                        }
                        int count = await _dbContext.HiringRequestCandidates
                                                             .CountAsync(x => x.HiringRequestId == request.HiringRequestId &&
                                                                              x.IsDeleted == false &&
                                                                              x.IsSelected);
                        hrDetail.FilledVacancies = count;
                        hrDetail.ModifiedById = request.ModifiedById;
                        hrDetail.ModifiedDate = request.ModifiedDate;           
                        await _dbContext.SaveChangesAsync();

                    }
                    else
                    {
                        throw new Exception("No Candidate found");

                    }
                }
                response.ResponseData = hrDetail;
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
