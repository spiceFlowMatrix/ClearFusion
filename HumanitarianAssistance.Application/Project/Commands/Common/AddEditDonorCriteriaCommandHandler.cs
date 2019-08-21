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
    public class AddEditDonorCriteriaCommandHandler : IRequestHandler<AddEditDonorCriteriaCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditDonorCriteriaCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditDonorCriteriaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            DonorCriteriaDetails _detail = new DonorCriteriaDetails();
            try
            {
                _detail = await _dbContext.DonorCriteriaDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                 x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new DonorCriteriaDetails
                    {
                        MethodOfFunding = request.MethodOfFunding,
                        PastFundingExperience = request.PastFundingExperience,
                        ProposalAccepted = request.ProposalAccepted,
                        ProposalExperience = request.ProposalExperience,
                        Professional = request.Professional,
                        FundsOnTime = request.FundsOnTime,
                        EffectiveCommunication = request.EffectiveCommunication,
                        Dispute = request.Dispute,
                        OtherDeliverable = request.OtherDeliverable,
                        OtherDeliverableType = request.OtherDeliverableType,
                        PastWorkingExperience = request.PastWorkingExperience,
                        CriticismPerformance = request.CriticismPerformance,
                        TimeManagement = request.TimeManagement,
                        MoneyAllocation = request.MoneyAllocation,
                        Accountability = request.Accountability,
                        DeliverableQuality = request.DeliverableQuality,
                        DonorFinancingHistory = request.DonorFinancingHistory,
                        ReligiousStanding = request.ReligiousStanding,
                        PoliticalStanding = request.PoliticalStanding,
                        ProjectId = request.ProjectId.Value,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.DonorCriteriaDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.MethodOfFunding = request.MethodOfFunding;
                    _detail.PastFundingExperience = request.PastFundingExperience;
                    _detail.ProposalAccepted = request.ProposalAccepted;
                    _detail.ProposalExperience = request.ProposalExperience;
                    _detail.Professional = request.Professional;
                    _detail.FundsOnTime = request.FundsOnTime;
                    _detail.EffectiveCommunication = request.EffectiveCommunication;
                    _detail.Dispute = request.Dispute;
                    _detail.OtherDeliverable = request.OtherDeliverable;
                    _detail.OtherDeliverableType = request.OtherDeliverableType;
                    _detail.PastWorkingExperience = request.PastWorkingExperience;
                    _detail.CriticismPerformance = request.CriticismPerformance;
                    _detail.TimeManagement = request.TimeManagement;
                    _detail.MoneyAllocation = request.MoneyAllocation;
                    _detail.Accountability = request.Accountability;
                    _detail.DeliverableQuality = request.DeliverableQuality;
                    _detail.DonorFinancingHistory = request.DonorFinancingHistory;
                    _detail.ReligiousStanding = request.ReligiousStanding;
                    _detail.PoliticalStanding = request.PoliticalStanding;
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
