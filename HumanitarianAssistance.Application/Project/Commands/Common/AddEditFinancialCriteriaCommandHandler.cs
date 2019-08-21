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
    public class AddEditFinancialCriteriaCommandHandler : IRequestHandler<AddEditFinancialCriteriaCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditFinancialCriteriaCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditFinancialCriteriaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            FinancialCriteriaDetail _detail = new FinancialCriteriaDetail();
            try
            {
                _detail = await _dbContext.FinancialCriteriaDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                     x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new FinancialCriteriaDetail
                    {
                        ProjectActivities = request.ProjectActivities,
                        Operational = request.Operational,
                        Overhead_Admin = request.Overhead_Admin,
                        Lump_Sum = request.Lump_Sum,
                        ProjectId = request.ProjectId.Value,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.FinancialCriteriaDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.ProjectActivities = request.ProjectActivities;
                    _detail.Operational = request.Operational;
                    _detail.Overhead_Admin = request.Overhead_Admin;
                    _detail.Lump_Sum = request.Lump_Sum;
                    _detail.ProjectId = request.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = request.ModifiedById;
                    _detail.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
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

