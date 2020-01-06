using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
                bool recordExists = await _dbContext.ProjectHiringRequestDetail.AnyAsync(x => x.IsDeleted == false &&
                                                                                            x.HiringRequestId == request.HiringRequestId);

                if (recordExists)
                {
                    ProjectHiringRequestDetail projectHiringRequest = await _dbContext.ProjectHiringRequestDetail
                                                                                              .FirstOrDefaultAsync(x => x.HiringRequestId == request.HiringRequestId &&
                                                                                                                        x.IsDeleted == false);
                    // Note : edit ProjectJob in old Ui
                    if (projectHiringRequest != null && request.JobCategory != null && request.TotalVacancy != projectHiringRequest.TotalVacancies)
                    {
                        var jobdetail = await _dbContext.ProjectJobHiringDetail.Where(x => x.JobId == request.JobCategory).FirstOrDefaultAsync();
                        var temp = projectHiringRequest.TotalVacancies - request.TotalVacancy;
                        if (temp > 0)
                        {
                            jobdetail.FilledVacancies = jobdetail.FilledVacancies + temp;
                        }
                        else
                        {
                            jobdetail.FilledVacancies = jobdetail.FilledVacancies - (temp * -1);
                        }
                        await _dbContext.SaveChangesAsync();
                    }
                    projectHiringRequest.ModifiedById = request.ModifiedById;
                    projectHiringRequest.ModifiedDate = request.ModifiedDate;
                    projectHiringRequest.IsDeleted = false;
                    projectHiringRequest.OfficeId = request.Office;
                    // projectHiringRequest.ProfessionId = request.Position;
                    projectHiringRequest.HourlyRate= request.PayHourlyRate;
                    projectHiringRequest.ProjectId = request.ProjectId;
                    projectHiringRequest.TotalVacancies = request.TotalVacancy;
                    projectHiringRequest.AnouncingDate = request.AnouncingDate;
                    projectHiringRequest.PositionId= request.Position;
                    projectHiringRequest.BudgetLineId= request.BudgetLine;
                    projectHiringRequest.Background = request.Background;
                    projectHiringRequest.JobStatus = request.JobStatus;
                    projectHiringRequest.JobId = request.JobCategory;
                    projectHiringRequest.JobTypeId= request.JobType;

                    projectHiringRequest.KnowladgeAndSkillRequired = request.KnowledgeAndSkillsRequired;
                    projectHiringRequest.SalaryRange = request.SalaryRange;
                    projectHiringRequest.Shift = request.JobShift;
                    projectHiringRequest.ProvinceId = request.ProvinceId;
                    projectHiringRequest.SpecificDutiesAndResponsblities = request.SpecificDutiesAndResponsibilities;
                    projectHiringRequest.SubmissionGuidlines = request.SubmissionGuidelines;
                    projectHiringRequest.ClosingDate = request.ClosingDate;
                    projectHiringRequest.ContractDuration = request.ContractDuration;
                    projectHiringRequest.ContractType = request.ContractType;
                    projectHiringRequest.CountryId = request.Nationality;
                    projectHiringRequest.GenderId = request.Gender;
                    projectHiringRequest.CurrencyId= request.PayCurrency;
                    projectHiringRequest.EducationDegreeId= request.EducationDegree;
                    projectHiringRequest.MinimumEducationLevel = request.MinEducationLevel;
                    projectHiringRequest.Experience = request.Experience;
                    projectHiringRequest.GradeId= request.JobGrade;
                    projectHiringRequest.ProfessionId= request.Profession;
                    projectHiringRequest.Organization = request.Organization;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Unable to Updated Hiring Request");
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
