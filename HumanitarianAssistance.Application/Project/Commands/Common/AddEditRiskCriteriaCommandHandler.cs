using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditRiskCriteriaCommandHandler : IRequestHandler<AddEditRiskCriteriaCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditRiskCriteriaCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditRiskCriteriaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            RiskCriteriaDetail _detail = new RiskCriteriaDetail();
            try
            {
                _detail = await _dbContext.RiskCriteriaDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new RiskCriteriaDetail
                    {
                        Security = request.Security,
                        Staff = request.Staff,
                        ProjectAssets = request.ProjectAssets,
                        Suppliers = request.Suppliers,
                        Beneficiaries = request.Beneficiaries,
                        OverallOrganization = request.OverallOrganization,
                        DeliveryFaiLure = request.DeliveryFaiLure,
                        PrematureSeizure = request.PrematureSeizure,
                        GovernmentConfiscation = request.GovernmentConfiscation,
                        DesctructionByTerroristActivity = request.DesctructionByTerroristActivity,
                        Reputation = request.Reputation,
                        Religious = request.Religious,
                        Sectarian = request.Sectarian,
                        Ethinc = request.Ethinc,
                        Social = request.Social,
                        Traditional = request.Traditional,
                        FocusDivertingrisk = request.FocusDivertingrisk,
                        Financiallosses = request.Financiallosses,
                        Opportunityloss = request.Opportunityloss,


                        Probablydelaysinfunding = request.Probablydelaysinfunding,
                        OtherOrganizationalHarms = request.OtherOrganizationalHarms,
                        OrganizationalDescription = request.OrganizationalDescription,
                        ProjectId = request.ProjectId.Value,
                        Geographical = request.Geographical,
                        Insecurity = request.Insecurity,
                        Season = request.Season,
                        Ethnicity = request.Ethnicity,
                        Culture = request.Culture,
                        ReligiousBeliefs = request.ReligiousBeliefs,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };


                    await _dbContext.RiskCriteriaDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.Security = request.Security;
                    _detail.Staff = request.Staff;
                    _detail.ProjectAssets = request.ProjectAssets;
                    _detail.Suppliers = request.Suppliers;
                    _detail.Beneficiaries = request.Beneficiaries;
                    _detail.OverallOrganization = request.OverallOrganization;
                    _detail.DeliveryFaiLure = request.DeliveryFaiLure;
                    _detail.PrematureSeizure = request.PrematureSeizure;
                    _detail.GovernmentConfiscation = request.GovernmentConfiscation;
                    _detail.DesctructionByTerroristActivity = request.DesctructionByTerroristActivity;
                    _detail.Reputation = request.Reputation;
                    _detail.Religious = request.Religious;
                    _detail.Sectarian = request.Sectarian;
                    _detail.Ethinc = request.Ethinc;
                    _detail.Social = request.Social;
                    _detail.Traditional = request.Traditional;
                    _detail.FocusDivertingrisk = request.FocusDivertingrisk;
                    _detail.Financiallosses = request.Financiallosses;
                    _detail.Opportunityloss = request.Opportunityloss;


                    _detail.Probablydelaysinfunding = request.Probablydelaysinfunding;
                    _detail.OtherOrganizationalHarms = request.OtherOrganizationalHarms;
                    _detail.OrganizationalDescription = request.OrganizationalDescription;
                    _detail.ProjectId = request.ProjectId.Value;
                    _detail.Geographical = request.Geographical;
                    _detail.Insecurity = request.Insecurity;
                    _detail.Season = request.Season;
                    _detail.Ethnicity = request.Ethnicity;
                    _detail.Culture = request.Culture;
                    _detail.ReligiousBeliefs = request.ReligiousBeliefs;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = request.ModifiedById;
                    _detail.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();

                }



                if (request.ProjectSelectionId != null)
                {
                    //check is project exists
                    bool projectPresent = await _dbContext.FinancialProjectDetail.AnyAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                         x.IsDeleted == false);

                    //if exist then remove
                    if (projectPresent)
                    {
                        FinancialProjectDetail projectExist = await _dbContext.FinancialProjectDetail
                                                                                       .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                                 x.IsDeleted == false);

                        // if exist then remove it
                        _dbContext.FinancialProjectDetail.RemoveRange(projectExist);
                        await _dbContext.SaveChangesAsync();
                    }

                    List<FinancialProjectDetail> projectList = new List<FinancialProjectDetail>();

                    foreach (var item in request.ProjectSelectionId)
                    {
                        FinancialProjectDetail _data = new FinancialProjectDetail
                        {
                            ProjectSelectionId = item,
                            ProjectId = request.ProjectId.Value,
                            IsDeleted = false,
                            CreatedById = request.CreatedById,
                            CreatedDate = DateTime.UtcNow
                        };

                        projectList.Add(_data);
                    }

                    //Add
                    _dbContext.FinancialProjectDetail.AddRange(projectList);
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
