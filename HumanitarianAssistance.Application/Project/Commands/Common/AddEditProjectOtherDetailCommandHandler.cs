using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectOtherDetailCommandHandler: IRequestHandler<AddEditProjectOtherDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEditProjectOtherDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditProjectOtherDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            long LatestProjectOtherDetailId = 0;

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    ProjectOtherDetail Projectdetail = await _dbContext.ProjectOtherDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectId == request.ProjectId);
                    
                    if (Projectdetail == null)
                    {
                        ProjectOtherDetail obj = new ProjectOtherDetail
                        {
                            opportunityNo = request.opportunityNo,
                            opportunity = request.opportunity,
                            opportunitydescription = request.opportunitydescription,
                            ProvinceId = request.ProvinceId,
                            ProjectId = request.ProjectId,
                            DistrictID = request.DistrictID,
                            OfficeId = request.OfficeId,
                            StartDate = request.StartDate,
                            EndDate = request.EndDate,
                            CurrencyId = request.CurrencyId,
                            budget = request.budget,
                            beneficiaryMale = request.beneficiaryMale,
                            beneficiaryFemale = request.beneficiaryFemale,
                            projectGoal = request.projectGoal,
                            projectObjective = request.projectObjective,
                            mainActivities = request.mainActivities,
                            DonorId = request.DonorId,
                            SubmissionDate = request.SubmissionDate,
                            REOIReceiveDate = request.REOIReceiveDate,
                            StrengthConsiderationId = request.StrengthConsiderationId,
                            GenderConsiderationId = request.GenderConsiderationId,
                            GenderRemarks = request.GenderRemarks,
                            SecurityId = request.SecurityId,
                            SecurityConsiderationId = request.SecurityConsiderationId,
                            SecurityRemarks = request.SecurityRemarks,
                            IsDeleted = false,
                            CreatedById = request.CreatedById,
                            CreatedDate = DateTime.UtcNow,
                            InDirectBeneficiaryFemale = request.InDirectBeneficiaryFemale,
                            InDirectBeneficiaryMale = request.InDirectBeneficiaryMale,
                            OpportunityType = request.OpportunityType
                        };

                        await _dbContext.ProjectOtherDetail.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();

                        LatestProjectOtherDetailId = obj.ProjectOtherDetailId;
                    }
                    else
                    {
                        //var existProjectRecord = await _dbContext.ProjectOtherDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectOtherDetailId == request.ProjectOtherDetailId && x.ProjectId == request.ProjectId);
                        
                        if (Projectdetail != null)
                        {
                            Projectdetail.opportunityNo = request.opportunityNo;
                            Projectdetail.ProjectId = request.ProjectId;
                            Projectdetail.opportunity = request.opportunity;
                            Projectdetail.opportunitydescription = request.opportunitydescription;
                            Projectdetail.ProvinceId = request.ProvinceId;
                            Projectdetail.DistrictID = request.DistrictID;
                            Projectdetail.OfficeId = request.OfficeId;
                            Projectdetail.StartDate = request.StartDate;
                            Projectdetail.EndDate = request.EndDate;
                            Projectdetail.CurrencyId = request.CurrencyId;
                            Projectdetail.budget = request.budget;
                            Projectdetail.beneficiaryMale = request.beneficiaryMale;
                            Projectdetail.beneficiaryFemale = request.beneficiaryFemale;
                            Projectdetail.projectGoal = request.projectGoal;
                            Projectdetail.projectObjective = request.projectObjective;
                            Projectdetail.mainActivities = request.mainActivities;
                            Projectdetail.DonorId = request.DonorId;
                            Projectdetail.SubmissionDate = request.SubmissionDate;
                            Projectdetail.REOIReceiveDate = request.REOIReceiveDate;
                            Projectdetail.StrengthConsiderationId = request.StrengthConsiderationId;
                            Projectdetail.GenderConsiderationId = request.GenderConsiderationId;
                            Projectdetail.GenderRemarks = request.GenderRemarks;
                            Projectdetail.SecurityId = request.SecurityId;
                            Projectdetail.SecurityConsiderationId = request.SecurityConsiderationId;
                            Projectdetail.SecurityRemarks = request.SecurityRemarks;
                            Projectdetail.IsDeleted = false;
                            Projectdetail.ModifiedById = request.ModifiedById;
                            Projectdetail.ModifiedDate = DateTime.UtcNow;
                            Projectdetail.InDirectBeneficiaryFemale = request.InDirectBeneficiaryFemale;
                            Projectdetail.InDirectBeneficiaryMale = request.InDirectBeneficiaryMale;
                            Projectdetail.OpportunityType = request.OpportunityType;

                            _dbContext.ProjectOtherDetail.Update(Projectdetail);
                            await _dbContext.SaveChangesAsync();
                            LatestProjectOtherDetailId = Projectdetail.ProjectOtherDetailId;
                        }
                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.CommonId.Id = Convert.ToInt32(LatestProjectOtherDetailId);

                    response.Message = "Success";
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }

            return response;
        }
    }
}