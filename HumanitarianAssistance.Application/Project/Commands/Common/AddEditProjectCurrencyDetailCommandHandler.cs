using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
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
    public class AddEditProjectCurrencyDetailCommandHandler : IRequestHandler<AddEditProjectCurrencyDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddEditProjectCurrencyDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(AddEditProjectCurrencyDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ProjectProposalDetail details = new ProjectProposalDetail();
            try
            {
                details = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                if (details == null)
                {
                    details = new ProjectProposalDetail
                    {
                        ProjectId = request.ProjectId.Value,
                        CurrencyId = request.CurrencyId,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.ProjectProposalDetail.AddAsync(details);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    details.ProjectId = request.ProjectId.Value;
                    details.CurrencyId = request.CurrencyId;
                    details.ModifiedById = request.ModifiedById;
                    details.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
                response.data.ProjectProposalDetail = details;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
