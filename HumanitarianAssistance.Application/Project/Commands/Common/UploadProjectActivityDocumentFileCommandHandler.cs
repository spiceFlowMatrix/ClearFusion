using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class UploadProjectActivityDocumentFileCommandHandler : IRequestHandler<UploadProjectActivityDocumentFileCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public UploadProjectActivityDocumentFileCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UploadProjectActivityDocumentFileCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ActivityDocumentDetailModel activityModel = new ActivityDocumentDetailModel();
                var projectDetail = await _dbContext.ProjectActivityDetail
                                                           .Include(x => x.ProjectBudgetLineDetail.ProjectDetail)
                                                           .FirstOrDefaultAsync(x => x.ActivityId == request.ActivityID && x.IsDeleted == false);

                string folderName = projectDetail?.ProjectBudgetLineDetail.ProjectDetail.ProjectCode;

                string googleApplicationCredentail = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
                if (googleApplicationCredentail == null)
                {
                    string GoogleServiceAccountDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                    GoogleServiceAccountDirectory = GoogleServiceAccountDirectory.Replace(@"\", "/");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", GoogleServiceAccountDirectory);
                }
                using (Stream objStream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                {
                    string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                    if (bucketName != null)
                    {
                        ActivityDocumentsDetail docObj = new ActivityDocumentsDetail();

                        string folderWithProposalFile = StaticResource.ProjectsFolderName + "/" + folderName + "/" + StaticResource.ProjectActivityFolderName + "/" + request.FileName;
                        string uploadedFileResponse = await GCBucket.UploadOtherProposalDocuments(bucketName, folderWithProposalFile, request.File, request.FileName, request.Ext);
                        if (!string.IsNullOrEmpty(uploadedFileResponse))
                        {
                            docObj.ActivityId = request.ActivityID;
                            docObj.ActivityDocumentsFilePath = uploadedFileResponse;
                            docObj.StatusId = request.StatusID;
                            docObj.CreatedById = request.CreatedById ;
                            docObj.IsDeleted = false;
                            docObj.MonitoringId = request.MonitoringID;
                            docObj.CreatedDate = request.CreatedDate;

                            await _dbContext.ActivityDocumentsDetail.AddAsync(docObj);
                            await _dbContext.SaveChangesAsync();
                        }

                        ActivityDocumentDetailModel obj = new ActivityDocumentDetailModel
                        {
                            ActivityDocumentId = docObj.ActtivityDocumentId,
                            ActivityDocumentsFilePath = docObj.ActivityDocumentsFilePath,
                            ActivityDocumentsFileName = docObj.ActivityDocumentsFilePath.Split('/').Last(),
                            StatusId = docObj.StatusId,
                            ActivityId = docObj.ActivityId,
                        };

                        response.data.activityDocumnentDetail = obj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.BucketNameNotFound;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
