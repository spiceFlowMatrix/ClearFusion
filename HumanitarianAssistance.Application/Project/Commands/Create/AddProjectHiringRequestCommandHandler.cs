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

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectHiringRequestCommandHandler : IRequestHandler<AddProjectHiringRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectHiringRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddProjectHiringRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectHiringRequestDetail hiringRequestDeatil = new ProjectHiringRequestDetail()
                {                     
                    CreatedById = request.CreatedById,
                    CreatedDate =request.CreatedDate,
                    IsDeleted = false,
                    OfficeId = request.Office,
                    ProfessionId = request.Position,
                    ProjectId = request.ProjectId,
                    TotalVacancies = request.TotalVacancy,
                    AnouncingDate=request.AnouncingDate,
                    // JobType=request.JobType,
                    Background=request.Background,
                    JobStatus=request.JobStatus,
                    JobId=request.JobCategory,
                    KnowladgeAndSkillRequired=request.KnowladgeAndSkillRequired,
                    SalaryRange=request.SalaryRange,
                    Shift=request.JobShift,
                    ProvinceId=request.Province, 
                    SpecificDutiesAndResponsblities=request.SpecificDutiesAndResponsblities,
                    SubmissionGuidlines=request.SubmissionGuidlines,
                    ClosingDate=request.ClosingDate,
                    ContractDuration=request.ContractDuration,
                    ContractType=request.ContractType,
                    CountryId=request.Country,
                    GenderId=request.Gender,
                    MinimumEducationLevel=request.MinEducationLevel,
                    Experience=request.Experience,
                    Organization=request.Organization                    
                };
                if(request.JobCategory!=null)
                {
                    
                }
                await _dbContext.ProjectHiringRequestDetail.AddAsync(hiringRequestDeatil);
                await _dbContext.SaveChangesAsync();
    
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
