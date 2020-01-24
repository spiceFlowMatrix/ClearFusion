using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Update {
    public class EditCandidateDetailsCommandHandler : IRequestHandler<EditCandidateDetailsCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public EditCandidateDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (EditCandidateDetailsCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var candidateDetail = await _dbContext.CandidateDetails.Where (x => x.CandidateId == request.CandidateId && x.IsDeleted == false).FirstOrDefaultAsync ();
                candidateDetail.FirstName = request.FirstName;
                candidateDetail.LastName = request.LastName;
                candidateDetail.Email = request.Email;
                candidateDetail.Password = request.Password;
                candidateDetail.PhoneNumber = request.PhoneNumber;
                candidateDetail.GenderId = request.Gender;
                candidateDetail.DateOfBirth = request.DateOfBirth;
                candidateDetail.EducationDegreeId = request.EducationDegree;
                candidateDetail.ProfessionId = request.Profession;
                candidateDetail.CountryId = request.Country;
                candidateDetail.ProvinceId = request.Province;
                candidateDetail.DistrictID = request.District;
                candidateDetail.ExperienceYear = request.ExperienceYear;
                candidateDetail.ExperienceMonth = request.ExperienceMonth;
                candidateDetail.PreviousWork = request.PreviousWork;
                candidateDetail.CurrentAddress = request.CurrentAddress;
                candidateDetail.PermanentAddress = request.PermanentAddress;
                candidateDetail.RelevantExperienceInYear = request.RelevantExperienceInYear;
                candidateDetail.IrrelevantExperienceInYear = request.IrrelevantExperienceInYear;
                candidateDetail.ModifiedById = request.ModifiedById;
                candidateDetail.ModifiedDate = request.ModifiedDate;
                candidateDetail.Remarks = request.Remarks;
                await _dbContext.SaveChangesAsync ();

                if(request.IsCvUpdated) 
                {
                    var invoiceDocuments = await _dbContext.EntitySourceDocumentDetails
                    .Include(x=>x.DocumentFileDetail)
                    .Where(x=>x.IsDeleted == false && x.EntityId == candidateDetail.CandidateId && x.DocumentFileDetail.PageId == (int)FileSourceEntityTypes.HiringRequestCandidateCV).ToListAsync();
                    foreach (var doc in invoiceDocuments) {
                        doc.IsDeleted = true;
                        doc.DocumentFileDetail.IsDeleted = true;
                    }
                    await _dbContext.SaveChangesAsync();
                }

                response.CommonId.Id = (int) candidateDetail.CandidateId;
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