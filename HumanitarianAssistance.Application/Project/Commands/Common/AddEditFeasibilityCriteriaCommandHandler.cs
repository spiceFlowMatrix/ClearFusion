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
    class AddEditFeasibilityCriteriaCommandHandler : IRequestHandler<AddEditFeasibilityCriteriaCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditFeasibilityCriteriaCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditFeasibilityCriteriaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            FeasibilityCriteriaDetail _detail = new FeasibilityCriteriaDetail();
            try
            {
                _detail = await _dbContext.FeasibilityCriteriaDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                       x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new FeasibilityCriteriaDetail
                    {
                        CapacityAvailableForProject = request.CapacityAvailableForProject,
                        TrainedStaff = request.TrainedStaff,
                        ByEquipment = request.ByEquipment,
                        ExpandScope = request.ExpandScope,
                        GeoGraphicalPresence = request.GeoGraphicalPresence,
                        ThirdPartyContract = request.ThirdPartyContract,
                        CostOfCompensationMonth = request.CostOfCompensationMonth,
                        CostOfCompensationMoney = request.CostOfCompensationMoney,
                        AnyInKindComponent = request.AnyInKindComponent,
                        UseableByOrganisation = request.UseableByOrganisation,
                        FeasibleExpertDeploy = request.FeasibleExpertDeploy,
                        EnoughTimeForProject = request.EnoughTimeForProject,
                        ProjectAllowedBylaw = request.ProjectAllowedBylaw,
                        ProjectByLeadership = request.ProjectByLeadership,
                        IsProjectPractical = request.IsProjectPractical,
                        PresenceCoverageInProject = request.PresenceCoverageInProject,
                        ProjectInLineWithOrgFocus = request.ProjectInLineWithOrgFocus,
                        EnoughTimeToPrepareProposal = request.EnoughTimeToPrepareProposal,
                        ProjectRealCost = request.ProjectRealCost,
                        IsCostGreaterthenBudget = request.IsCostGreaterthenBudget,
                        PerCostGreaterthenBudget = request.PerCostGreaterthenBudget,
                        IsFinancialContribution = request.IsFinancialContribution,
                        IsSecurity = request.IsSecurity,
                        IsGeographical = request.IsGeographical,
                        IsSeasonal = request.IsSeasonal,
                        ProjectId = request.ProjectId.Value,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.FeasibilityCriteriaDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.CapacityAvailableForProject = request.CapacityAvailableForProject;
                    _detail.TrainedStaff = request.TrainedStaff;
                    _detail.ByEquipment = request.ByEquipment;
                    _detail.ExpandScope = request.ExpandScope;
                    _detail.GeoGraphicalPresence = request.GeoGraphicalPresence;
                    _detail.ThirdPartyContract = request.ThirdPartyContract;
                    _detail.CostOfCompensationMonth = request.CostOfCompensationMonth;
                    _detail.CostOfCompensationMoney = request.CostOfCompensationMoney;
                    _detail.AnyInKindComponent = request.AnyInKindComponent;
                    _detail.UseableByOrganisation = request.UseableByOrganisation;
                    _detail.FeasibleExpertDeploy = request.FeasibleExpertDeploy;
                    _detail.EnoughTimeForProject = request.EnoughTimeForProject;
                    _detail.ProjectAllowedBylaw = request.ProjectAllowedBylaw;
                    _detail.ProjectByLeadership = request.ProjectByLeadership;
                    _detail.IsProjectPractical = request.IsProjectPractical;
                    _detail.PresenceCoverageInProject = request.PresenceCoverageInProject;
                    _detail.ProjectInLineWithOrgFocus = request.ProjectInLineWithOrgFocus;
                    _detail.EnoughTimeToPrepareProposal = request.EnoughTimeToPrepareProposal;
                    _detail.ProjectRealCost = request.ProjectRealCost;
                    _detail.IsCostGreaterthenBudget = request.IsCostGreaterthenBudget;
                    _detail.PerCostGreaterthenBudget = request.PerCostGreaterthenBudget;
                    _detail.IsFinancialContribution = request.IsFinancialContribution;
                    _detail.IsSecurity = request.IsSecurity;
                    _detail.IsGeographical = request.IsGeographical;
                    _detail.IsSeasonal = request.IsSeasonal;
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
