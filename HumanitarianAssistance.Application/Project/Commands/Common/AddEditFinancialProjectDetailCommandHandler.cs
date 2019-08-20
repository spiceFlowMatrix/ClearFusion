using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditFinancialProjectDetailCommandHandler : IRequestHandler<AddEditFinancialProjectDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditFinancialProjectDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditFinancialProjectDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            FinancialProjectDetail _detail = new FinancialProjectDetail();
            try
            {
                _detail = await _dbContext.FinancialProjectDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                           x.FinancialProjectDetailId == request.FinancialProjectDetailId &&
                                                                                           x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new FinancialProjectDetail
                    {
                        ProjectId = request.ProjectId,
                        ProjectSelectionId = request.ProjectSelectionId,
                        ProjectName = request.ProjectName,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.FinancialProjectDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.ProjectId = request.ProjectId;
                    _detail.ProjectSelectionId = request.ProjectSelectionId;
                    _detail.ProjectName = request.ProjectName;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = request.ModifiedById;
                    _detail.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
                response.CommonId.Id = Convert.ToInt32(_detail.ProjectSelectionId);
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
