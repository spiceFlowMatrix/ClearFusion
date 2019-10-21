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
    public class AddEditEligibilityCriteriaDetailCommandHandler : IRequestHandler<AddEditEligibilityCriteriaDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditEligibilityCriteriaDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditEligibilityCriteriaDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            EligibilityCriteriaDetail _detail = new EligibilityCriteriaDetail();
            try
            {
                _detail = await _dbContext.EligibilityCriteriaDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                             x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new EligibilityCriteriaDetail
                    {
                        DonorCriteriaMet = request.DonorCriteriaMet,
                        EligibilityDealine = request.EligibilityDealine,
                        CoPartnership = request.CoPartnership,
                        ProjectId = request.ProjectId,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.EligibilityCriteriaDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.DonorCriteriaMet = request.DonorCriteriaMet;
                    _detail.EligibilityDealine = request.EligibilityDealine;
                    _detail.CoPartnership = request.CoPartnership;
                    _detail.ProjectId = request.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = request.ModifiedById;
                    _detail.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
                response.data.eligibilityCriteriaDetail = _detail;
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
