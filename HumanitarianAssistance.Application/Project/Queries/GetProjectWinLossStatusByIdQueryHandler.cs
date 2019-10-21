using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectWinLossStatusByIdQueryHandler : IRequestHandler<GetProjectWinLossStatusByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectWinLossStatusByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectWinLossStatusByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                WinProjectDetails projectDetail = await _dbContext.WinProjectDetails.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                if (projectDetail != null)
                {
                    WinProjectDetailsModel detail = new WinProjectDetailsModel()
                    {
                        ProjectId = projectDetail.ProjectId,
                        CommentText = projectDetail.CommentText,
                        FileName = projectDetail.FileName,
                        FilePath = projectDetail.FilePath,
                        IsWin = projectDetail.IsWin,
                    };
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                    response.ResponseData = detail;
                }
                else
                {
                    response.StatusCode = StaticResource.notFoundCode;
                    response.Message = "Detail not found";
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