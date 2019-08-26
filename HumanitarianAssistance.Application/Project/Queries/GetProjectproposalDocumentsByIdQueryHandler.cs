using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectproposalDocumentsByIdQueryHandler : IRequestHandler<GetProjectproposalDocumentsByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetProjectproposalDocumentsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectproposalDocumentsByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<ProjectProposalModelList> obj = new List<ProjectProposalModelList>();
                obj = await (from d in _dbContext.ProjectProposalDocument
                             join u in _dbContext.UserDetails on d.CreatedById equals u.AspNetUserId into ud
                             from u in ud.DefaultIfEmpty()
                             where d.ProjectId == request.ProjectId && d.IsDeleted == false
                             select new ProjectProposalModelList
                             {
                                 ProposalDocumentName = d.ProposalDocumentName,
                                 ProjectId = d.ProjectId,
                                 ProposalWebLink = d.ProposalWebLink,
                                 ProposalExtType = d.ProposalExtType,
                                 ProposalDocumentTypeId = d.ProposalDocumentTypeId,
                                 CreatedDate = d.CreatedDate,
                                 UserName = ud.Select(x => new
                                 {
                                     FullName = x.FirstName + " " + x.LastName,
                                     x.AspNetUserId,
                                     x.IsDeleted
                                 }).FirstOrDefault(a => a.AspNetUserId == d.CreatedById && a.IsDeleted == false).FullName
                             }).ToListAsync();
                if (obj.Count() != 0)
                {
                    response.data.ProjectProposalModelList = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.NoDataFound;
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
