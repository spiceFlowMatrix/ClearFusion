using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class WinApprovalProjectDetailCommandHandler : IRequestHandler<WinApprovalProjectDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public WinApprovalProjectDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(WinApprovalProjectDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.UploadedFile))
                {
                    request.UploadedFile = request.UploadedFile.Substring(request.UploadedFile.IndexOf(',') + 1);

                }

                WinProjectDetails obj = new WinProjectDetails();
                obj.ProjectId = request.ProjectId;
                obj.CommentText = request.CommentText;
                obj.FileName = request.FileName;
                obj.FilePath = request.FilePath;
                obj.IsWin = request.IsWin;
                obj.IsDeleted = false;
                obj.CreatedById = request.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                await _dbContext.WinProjectDetails.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.CommonId.IsApproved = request.IsWin;

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
