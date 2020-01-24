using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {

    public class GetCandidateAllDetailByCandidateIdQueryHandler : IRequestHandler<GetCandidateAllDetailByCandidateIdQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;
        public GetCandidateAllDetailByCandidateIdQueryHandler (HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement) {
            _dbContext = dbContext;
            _fileManagement = fileManagement;
        }
        public async Task<ApiResponse> Handle (GetCandidateAllDetailByCandidateIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var candidateCv = await (from es in _dbContext.EntitySourceDocumentDetails 
                join df in _dbContext.DocumentFileDetail on es.DocumentFileId 
                equals df.DocumentFileId into docs from doc in docs.DefaultIfEmpty () 
                join u in _dbContext.UserDetails on doc.CreatedById 
                equals u.AspNetUserId into finaldocs from final in finaldocs.DefaultIfEmpty ()
                 where doc.IsDeleted == false && es.EntityId == request.CandidateId &&
                    doc.PageId == (int) FileSourceEntityTypes.HiringRequestCandidateCV select new {
                        DocumentFileId = doc.DocumentFileId,
                            UploadedBy = $"{final.FirstName} {final.LastName}",
                            EntityId = es.EntityId
                    }).FirstOrDefaultAsync ();
                CandidateCvModel model = new CandidateCvModel ();
                if (candidateCv != null) {
                    FileModel fileModel = new FileModel () {
                    PageId = (int) FileSourceEntityTypes.HiringRequestCandidateCV,
                    RecordId = candidateCv.EntityId,
                    DocumentFileId = candidateCv.DocumentFileId
                    };
                    var documentModel = await _fileManagement.GetFilesByDocumentFileId (fileModel);
                    model.AttachmentUrl = documentModel.SignedURL;
                    model.AttachmentName = documentModel.DocumentName;
                }
                var CandidateDetails = await (from cd in _dbContext.CandidateDetails.Where (x => x.CandidateId == request.CandidateId && x.IsDeleted == false) select new {
                    CandidateId = cd.CandidateId,
                        FirstName = cd.FirstName,
                        LastName = cd.LastName,
                        Email = cd.Email,
                        Password = cd.Password,
                        PhoneNumber = cd.PhoneNumber,
                        GenderId = cd.GenderId,
                        DateOfBirth = cd.DateOfBirth,
                        EducationDegreeId = cd.EducationDegreeId,
                        ProfessionId = cd.ProfessionId,
                        CountryId = cd.CountryId,
                        ProvinceId = cd.ProvinceId,
                        DistrictID = cd.DistrictID,
                        ExperienceYear = cd.ExperienceYear,
                        ExperienceMonth = cd.ExperienceMonth,
                        PreviousWork = cd.PreviousWork,
                        CurrentAddress = cd.CurrentAddress,
                        PermanentAddress = cd.PermanentAddress,
                        RelevantExperienceInYear = cd.RelevantExperienceInYear,
                        IrrelevantExperienceInYear = cd.IrrelevantExperienceInYear,
                        CreatedById = cd.CreatedById,
                        CreatedDate = cd.CreatedDate,
                        Remarks = cd.Remarks,
                        AttachmentName = model.AttachmentName,
                        AttachmentUrl = model.AttachmentUrl
                }).FirstOrDefaultAsync ();

                response.ResponseData = CandidateDetails;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;

        }
    }
}