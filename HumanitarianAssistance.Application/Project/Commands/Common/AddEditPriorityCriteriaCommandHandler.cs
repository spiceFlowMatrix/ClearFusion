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
    public class AddEditPriorityCriteriaCommandHandler : IRequestHandler<AddEditPriorityCriteriaCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditPriorityCriteriaCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditPriorityCriteriaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            PriorityCriteriaDetail _detail = new PriorityCriteriaDetail();
            try
            {
                _detail = await _dbContext.PriorityCriteriaDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                        x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new PriorityCriteriaDetail
                    {
                        IncreaseEligibility = request.IncreaseEligibility,
                        IncreaseReputation = request.IncreaseReputation,
                        ImproveDonorRelationship = request.ImproveDonorRelationship,
                        GoodCause = request.GoodCause,
                        ImprovePerformancecapacity = request.ImprovePerformancecapacity,
                        SkillImprove = request.SkillImprove,
                        FillingFundingGap = request.FillingFundingGap,
                        NewSoftware = request.NewSoftware,
                        NewEquipment = request.NewEquipment,
                        CoverageAreaExpansion = request.CoverageAreaExpansion,
                        NewTraining = request.NewTraining,
                        ExpansionGoal = request.ExpansionGoal,
                        ProjectId = request.ProjectId,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.PriorityCriteriaDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.IncreaseEligibility = request.IncreaseEligibility;
                    _detail.IncreaseReputation = request.IncreaseReputation;
                    _detail.ImproveDonorRelationship = request.ImproveDonorRelationship;
                    _detail.GoodCause = request.GoodCause;
                    _detail.ImprovePerformancecapacity = request.ImprovePerformancecapacity;
                    _detail.SkillImprove = request.SkillImprove;
                    _detail.FillingFundingGap = request.FillingFundingGap;
                    _detail.NewSoftware = request.NewSoftware;
                    _detail.NewEquipment = request.NewEquipment;
                    _detail.CoverageAreaExpansion = request.CoverageAreaExpansion;
                    _detail.NewTraining = request.NewTraining;
                    _detail.ExpansionGoal = request.ExpansionGoal;
                    _detail.ProjectId = request.ProjectId;
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
