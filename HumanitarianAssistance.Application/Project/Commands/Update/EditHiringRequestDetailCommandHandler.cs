using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditHiringRequestDetailCommandHandler : IRequestHandler<EditHiringRequestDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditHiringRequestDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditHiringRequestDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                string description = request.Description.ToLower().Trim();

                bool recordExists = await _dbContext.ProjectHiringRequestDetail.AnyAsync(x => x.IsDeleted == false &&
                                                                                           x.Description.ToLower().Trim() == description && x.HiringRequestId != request.HiringRequestId);

                if (!recordExists)
                {

                    ProjectHiringRequestDetail projectHiringRequest = await _dbContext.ProjectHiringRequestDetail
                                                                                              .FirstOrDefaultAsync(x => x.HiringRequestId == request.HiringRequestId &&
                                                                                                                        x.IsDeleted == false);
                    projectHiringRequest.BasicPay = request.BasicPay;
                    projectHiringRequest.BudgetLineId = request.BudgetLineId;
                    projectHiringRequest.ModifiedById = request.ModifiedById;
                    projectHiringRequest.ModifiedDate = DateTime.UtcNow;
                    projectHiringRequest.CurrencyId = request.CurrencyId;
                    projectHiringRequest.Description = request.Description;
                    projectHiringRequest.EmployeeID = request.EmployeeID;
                    projectHiringRequest.FilledVacancies = request.FilledVacancies;
                    projectHiringRequest.GradeId = request.GradeId;
                    projectHiringRequest.IsCompleted = request.IsCompleted;
                    projectHiringRequest.OfficeId = request.OfficeId;
                    projectHiringRequest.Position = request.Position;
                    projectHiringRequest.ProfessionId = request.ProfessionId;
                    projectHiringRequest.ProjectId = request.ProjectId;
                    projectHiringRequest.TotalVacancies = request.TotalVacancies;
                    await _dbContext.SaveChangesAsync();
                    // Note : edit ProjectJob in old Ui
                    if (projectHiringRequest.HiringRequestId != 0)
                    {
                        JobHiringDetails jobDetail = await _dbContext.JobHiringDetails.FirstOrDefaultAsync(x => x.HiringRequestId == request.HiringRequestId &&
                                                                                                            x.IsDeleted == false);
                        if (jobDetail != null)
                        {
                            jobDetail.JobDescription = request.Description;
                            jobDetail.ProfessionId = request.ProfessionId;
                            jobDetail.OfficeId = request.OfficeId;
                            jobDetail.IsActive = true;
                            jobDetail.GradeId = request.GradeId;
                            jobDetail.HiringRequestId = projectHiringRequest.HiringRequestId;
                            jobDetail.IsDeleted = false;
                            jobDetail.ModifiedById = request.ModifiedById;
                            jobDetail.ModifiedDate = DateTime.UtcNow;
                            jobDetail.Unit = request.TotalVacancies.Value;
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    throw new Exception("Hiring Request is already exist");
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
