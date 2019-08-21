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
    public class AddApprovalDetailCommandHandler : IRequestHandler<AddApprovalDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddApprovalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddApprovalDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ApproveProjectDetails obj = new ApproveProjectDetails();

                obj = await _dbContext.ApproveProjectDetails.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);
                if (obj == null)
                {
                    obj = new ApproveProjectDetails();
                    obj.ProjectId = request.ProjectId;
                    obj.CommentText = request.CommentText;
                    obj.FileName = request.FileName;
                    obj.FilePath = request.FilePath;
                    obj.IsApproved = request.IsApproved;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.ModifiedDate;
                    obj.ReviewCompletionDate = DateTime.UtcNow;
                    await _dbContext.ApproveProjectDetails.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    if (request.IsApproved == false)
                    {
                        ProjectProposalDetail details = await _dbContext.ProjectProposalDetail.Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false).FirstOrDefaultAsync();
                        if (details != null)
                        {
                            details.IsProposalAccept = request.IsApproved;
                            details.ModifiedById = request.ModifiedById;
                            details.IsDeleted = false;
                            details.ModifiedDate = request.ModifiedDate;
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    obj.ProjectId = request.ProjectId;
                    obj.CommentText = request.CommentText;
                    obj.FileName = request.FileName;
                    obj.FilePath = request.FilePath;
                    obj.IsApproved = request.IsApproved;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.ReviewCompletionDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();

                    if (request.IsApproved == false)
                    {
                        var details = _dbContext.ProjectProposalDetail.Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false).FirstOrDefault();
                        if (details != null)
                        {
                            details.IsProposalAccept = request.IsApproved;
                            details.ModifiedById = request.ModifiedById;
                            details.IsDeleted = false;
                            details.ModifiedDate = request.ModifiedDate;
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.data.ApproveProjectDetails = obj;
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
