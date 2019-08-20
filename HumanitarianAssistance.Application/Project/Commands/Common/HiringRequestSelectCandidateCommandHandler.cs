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

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class HiringRequestSelectCandidateCommandHandler : IRequestHandler<HiringRequestSelectCandidateCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public HiringRequestSelectCandidateCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(HiringRequestSelectCandidateCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {

                    HiringRequestCandidates hiringRequestCandidates = await _dbContext.HiringRequestCandidates
                                                                               .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                         x.EmployeeID == request.EmployeeId &&
                                                                                                         x.HiringRequestId == request.HiringRequestId);

                    if (hiringRequestCandidates != null)
                    {
                        hiringRequestCandidates.IsSelected = true;
                        hiringRequestCandidates.ModifiedById = request.ModifiedById;
                        hiringRequestCandidates.ModifiedDate = request.ModifiedDate;
                        hiringRequestCandidates.IsDeleted = false;
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Candidate not found");
                    }

                    //update the hiring request table when candidate is selected

                    ProjectHiringRequestDetail hrDetail = await _dbContext.ProjectHiringRequestDetail
                                                                                 .FirstOrDefaultAsync(x => x.HiringRequestId == request.HiringRequestId &&
                                                                                                          x.IsDeleted == false);
                    if (hrDetail != null)
                    {
                        int count = await _dbContext.HiringRequestCandidates.Where(x => x.HiringRequestId == request.HiringRequestId &&
                                                                                                 x.IsDeleted == false).CountAsync(x => x.IsSelected);
                        hrDetail.FilledVacancies = count;
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Hiring Job not found");
                    }
                    EmployeeSalaryAnalyticalInfo analyticalInfo = new EmployeeSalaryAnalyticalInfo();

                    analyticalInfo.IsDeleted = false;
                    analyticalInfo.CreatedById = request.CreatedById;
                    analyticalInfo.CreatedDate = request.CreatedDate;
                    analyticalInfo.EmployeeID = request.EmployeeId;
                    analyticalInfo.BudgetlineId = request.BudgetLineId;
                    analyticalInfo.ProjectId = request.ProjectId;
                    analyticalInfo.HiringRequestId = request.HiringRequestId;
                    await _dbContext.EmployeeSalaryAnalyticalInfo.AddAsync(analyticalInfo);
                    await _dbContext.SaveChangesAsync();
                    response.ResponseData = hrDetail;
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
