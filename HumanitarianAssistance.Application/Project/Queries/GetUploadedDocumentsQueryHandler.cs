using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetUploadedDocumentsQueryHandler : IRequestHandler<GetUploadedDocumentsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetUploadedDocumentsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetUploadedDocumentsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var listobj = _dbContext.ActivityDocumentsDetail.Where(x => x.ActivityId == request.ActivityId && x.IsDeleted == false).AsQueryable();
                if (request.MonitoringId != null)
                {
                    listobj = listobj.Where(x => x.MonitoringId == request.MonitoringId);
                }

                var obj = await listobj.Select(x => new ActivityDocumentDetailModel()
                {
                    ActivityId = x.ActivityId,
                    StatusId = x.StatusId,
                    ActivityDocumentsFilePath = x.ActivityDocumentsFilePath,
                    ActivityDocumentsFileName = x.ActivityDocumentsFilePath.Substring(x.ActivityDocumentsFilePath.LastIndexOf('/') + 1),
                    ActivityDocumentId = x.ActtivityDocumentId

                }).ToListAsync();

                response.data.ActivityDocumentDetailModel = obj;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;

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
