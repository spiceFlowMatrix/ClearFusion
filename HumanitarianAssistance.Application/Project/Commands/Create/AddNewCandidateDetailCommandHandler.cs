using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create {
    public class AddNewCandidateDetailCommandHandler : IRequestHandler<AddNewCandidateDetailCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddNewCandidateDetailCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (AddNewCandidateDetailCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                CandidateDetails candidateDetail = new CandidateDetails () {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    // AccountStatus = request.AccountStatus,
                    GenderId = request.Gender,
                    DateOfBirth = request.DateOfBirth,
                    EducationDegreeId = request.EducationDegree,
                    // GradeId = request.Grade,
                    ProfessionId = request.Profession,
                    // OfficeId = request.Office,
                    CountryId = request.Country,
                    ProvinceId = request.Province,
                    DistrictID = request.District,
                    ExperienceYear = request.ExperienceYear,
                    ExperienceMonth = request.ExperienceMonth,
                    PreviousWork = request.PreviousWork,
                    CurrentAddress = request.CurrentAddress,
                    PermanentAddress = request.PermanentAddress,
                    // TotalExperienceInYear = request.TotalExperienceInYear,
                    RelevantExperienceInYear = request.RelevantExperienceInYear,
                    IrrelevantExperienceInYear = request.IrrelevantExperienceInYear,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    Remarks = request.Remarks,
                    IsDeleted = false,
                };
                await _dbContext.CandidateDetails.AddAsync (candidateDetail);
                await _dbContext.SaveChangesAsync ();
                if (candidateDetail.CandidateId != 0) {
                    HiringRequestCandidateStatus obj = new HiringRequestCandidateStatus () {
                    ProjectId = request.ProjectId,
                    HiringRequestId = request.HiringRequestId,
                    CandidateId = candidateDetail.CandidateId,
                    CandidateStatus = 0,
                    // InterviewId = 0,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate
                    };
                    await _dbContext.HiringRequestCandidateStatus.AddAsync (obj);
                    await _dbContext.SaveChangesAsync ();
                }
                response.CommonId.Id = (int)candidateDetail.CandidateId;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}