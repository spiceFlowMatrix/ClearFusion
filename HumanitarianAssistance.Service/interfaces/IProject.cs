using DataAccess.DbEntities;
using DataAccess.DbEntities.ErrorLog;
using DataAccess.DbEntities.Project;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IProject
    {
        #region Donor Info
        Task<APIResponse> GetAllDonorList();
        Task<APIResponse> GetAllDonorFilterList(DonorFilterModel donorFilterModel);

        Task<APIResponse> AddEditDonorDetails(DonorModel model, string UserId);
        // Task<APIResponse> EditDonorDetails(DonorModel model, string UserId);
        Task<APIResponse> DeleteDonorDetails(long DonarId, string UserId);
        Task<APIResponse> GetDonarListById(long DonarId);
        #endregion

        #region Sector Info 

        Task<APIResponse> GetAllSectorList();
        Task<APIResponse> AddSectorDetails(SectorModel model, string UserId);
        Task<APIResponse> EditSectorDetails(SectorModel model, string UserId);
        Task<APIResponse> DeleteSectorDetails(SectorDetails model);
        #endregion

        #region Program Info 

        Task<APIResponse> GetAllProgramList();
        Task<APIResponse> AddProgramDetails(ProgramModel model, string UserId);
        Task<APIResponse> EditProgramDetails(ProgramModel model, string UserId);
        Task<APIResponse> DeleteProgramDetails(ProgramDetail model);
        #endregion 

        #region Area Info 

        Task<APIResponse> GetAllAreaList();
        Task<APIResponse> AddAreaDetails(AreaModel model, string UserId);
        Task<APIResponse> EditAreaDetails(AreaModel model, string UserId);
        Task<APIResponse> DeleteAreaDetails(AreaDetail model);
        void SaveErrorlog(int status, string message, string userName, string userId);
        #endregion

        #region Consideration
        Task<APIResponse> GenderConsiderationList();
        Task<APIResponse> StrengthConsiderationDetailList();
        Task<APIResponse> SecurityDetailList();
        Task<APIResponse> SecurityConsiderationDetailList();
        #endregion

        #region AddEditProjectDetail
        APIResponse AddEditProjectDetail(ProjectDetailNewModel model, string UserId);
        Task<APIResponse> DeleteProjectDetail(long ProjectId, string UserId);
        Task<APIResponse> GetAllProjectFilterList(ProjectFilterModel projectFilterModel );
        Task<APIResponse> GetAllProjectList();

        APIResponse GetProjectListById(long ProjectId);

        Task<APIResponse> AddEditProjectAssignToEmployee(ProjectAssignToModel model, string UserId);
        Task<APIResponse> DeleteProjectAssignToEmployee(ProjectAssignTo model, string UserId);

        Task<APIResponse> AddEditProjectProgram(ProjectProgramModel model, string UserId);
        Task<APIResponse> getProjectProgramById(long ProjectId);
        Task<APIResponse> getProjectAreaById(long ProjectId);
        Task<APIResponse> getProjectSectorById(long ProjectId);

        Task<APIResponse> DeleteProjectProgram(ProjectProgram model, string UserId);

        Task<APIResponse> AddEditProjectSector(ProjectSectorModel model, string UserId);

        Task<APIResponse> DeleteProjectSector(ProjectSector model, string UserId);
        Task<APIResponse> AddEditProjectArea(ProjectAreaModel model, string UserId);

        Task<APIResponse> DeleteProjectArea(ProjectArea model, string UserId);
        Task<APIResponse> AddApprovalDetail(ApproveProjectDetailModel model, string UserId);
        Task<APIResponse> WinApprovalDetail(WinApprovalProjectModel model, string UserId);

        #endregion

        #region Project Communication
        Task<APIResponse> GetChatByProjectId(long ProjectId);
        Task<APIResponse> AddProjectChat(ProjectCommunicationModel model, string UserId);

        #endregion

        Task<APIResponse> GetAllProjectDetails();
        #region GetAllProvinceDetails
        APIResponse GetAllProvinceDetails();
        APIResponse GetAllStrengthConsiderationDetails();
        APIResponse GetAllGenderConsiderationDetails();
        APIResponse GetAllSecurityDetails();
        APIResponse GetAllSecurityConsiderationDetails();
        APIResponse GetAllDistrictvalueByProvinceId(int[] provinceId);

        APIResponse AddEditProjectproposals(long projectId, string UserId,string logginUserEmailId);
        APIResponse GetProjectproposalsById(long projectId);

        APIResponse AddEditProjectotherDetail(ProjectOtherDetail otherDetail, string UserId);
        //APIResponse UploadOtherProposalFile(IFormFile file, string UserId);
        APIResponse UploadOtherProposalFile(IFormFile file, string UserId, string Projectid, string fullPath, string fileName, string logginUserEmailId,string ProposalType,string ext);

        APIResponse AddEditProjectProposalDetail(ProposalDocModel model, string UserId, string logginUserEmailId);
        APIResponse GetOtherProjectListById(long ProjectId);
        APIResponse AddEditDonorCriteria(DonorCriteriaModel model, string UserId);
        APIResponse AddEditPurposeofInitiativeCriteria(PurposeofInitiativeCriteriaModel model, string UserId);
        APIResponse AddEditEligibilityCriteriaDetail(EligibilityCriteriaDetailModel model, string UserId);
        APIResponse AddEditFeasibilityCriteria(FeasibilityCriteriaModel model, string UserId);
        APIResponse GetAllCriteriaEvaluationDetalByProjectId(long projectId);
        APIResponse AddEditPriorityCriteria(PriorityCriteriaModel model, string UserId);
        APIResponse AddEditFinancialCriteria(FinancialCriteriaModel model, string UserId);
        APIResponse AddEditRiskCriteria(RiskCriteriaModel model, string UserId);
        APIResponse AddEditTargetBeneficiary(TargetBeneficiaryDetail model, string UserId);

        APIResponse AddEditFinancialProjectDetail(FinancialProjectDetailModel model, string UserId);

        Task<APIResponse> AddPriorityOtherDetail(CEPriorityDetailModel model, string UserId);
        Task<APIResponse> AddFeasibilityExpertDetail(CEFeasibilityExpertOtherModel model, string UserId);
        Task<APIResponse> AddAgeGroupDetail(CEAgeGroupDetailModel model, string UserId);
        Task<APIResponse> AddOccupationOtherDetail(CEOccupationDetailModel model, string UserId);
        Task<APIResponse> AddAssumptionDetail(CEAssumptionDetailModel model, string UserId);
        Task<APIResponse> AddDonorEligibilityOtherDetail(DonorEligibilityCriteriaModel model, string UserId);


        Task<APIResponse> EditPriorityOtherDetail(CEPriorityDetailModel model, string UserId);
        Task<APIResponse> EditFeasibilityExpertDetail(CEFeasibilityExpertOtherModel model, string UserId);
        Task<APIResponse> EditAgeGroupDetail(CEAgeGroupDetailModel model, string UserId);
        Task<APIResponse> EditOccupationOtherDetail(CEOccupationDetailModel model, string UserId);
        Task<APIResponse> EditAssumptionDetail(CEAssumptionDetailModel model, string UserId);
        Task<APIResponse> EditDonorEligibilityOtherDetail(DonorEligibilityCriteriaModel model, string UserId);



        Task<APIResponse> GetAllPriorityDetailList();
        Task<APIResponse> GetAllPriorityDetailByProjectId(long projectId);
        Task<APIResponse> GetAllFeasibilityExpertList();
        Task<APIResponse> GetAllExpertDetailByProjectId(long projectId);

        Task<APIResponse> GetAllAgeGroupDetailList();
        Task<APIResponse> GetAllAgeGroupByProjectId(long projectId);

        Task<APIResponse> GetAllDonorEligibilityDetailByProjectId(long projectId);
        Task<APIResponse> GetAllDonorEligibilityDetailList();




        Task<APIResponse> GetAllOccuopationList();
        Task<APIResponse> GetAllOccupatiopnByProjectId(long projectId);

        Task<APIResponse> GetAllAssumptionList();
        Task<APIResponse> GetAllAssumptionByProjectId(long projectId);

        Task<APIResponse> DeletePriorityOtherDetails(long Id, string userId);
        Task<APIResponse> DeleteFeasibilityExperrtDetails(long Id, string userId);
        Task<APIResponse> DeleteAgeGroupDetails(long Id, string userId);
        Task<APIResponse> DeleteOccupationDetails(long Id, string userId);
        Task<APIResponse> DeleteAssumptionDetails(long Id, string userId);
        Task<APIResponse> DeleteDOnorEligibilityCriteriaOtherDetails(long Id, string userId);

        Task<APIResponse> AddEditCriteriaEvalutionSubmitDetail(ProjectDetailNewModel model);


        APIResponse AddEditSecurityConsidMultiDetail(SecurityConsiderationMultiSelectModel model, string UserId);
        APIResponse AddEditProvinceMultiSelectDetail(ProvinceMultiSelectModel model, string UserId);


        APIResponse GetSecurityConsiMultiSelectByProjectId(long ProjectId);
        APIResponse GetProvinceMultiSelectByProjectId(long ProjectId);
        APIResponse GetDistrictMultiSelectByProjectId(long ProjectId);
        APIResponse AddEditDistrictMultiSelectDetail(DistrictMultiSelectModel model, string UserId);
        // APIResponse SaveErrorlog(Errorlog obj, string UserId);


        Task<APIResponse>AddEditProjectJobDetail(ProjectJobDetailModel model, string UserId);

        Task<APIResponse> GetAllProjectJobDetail();
        Task<APIResponse> GetAllProjectJobByProjectId(long ProjectId);

        Task<APIResponse> AddEditProjectBudgetLineDetail(ProjectBudgetLineDetailModel model, string UserId);
        Task<APIResponse> GetallBudgetLineDetail();
        Task<APIResponse> GetallBudgetLineDetail(long projectId);

        Task<APIResponse> GetBudgetLineDetailByBudgetId(int budgetId);

        #endregion
    }
}
