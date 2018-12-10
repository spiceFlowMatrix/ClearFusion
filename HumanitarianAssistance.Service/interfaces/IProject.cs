using DataAccess.DbEntities;
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
        Task<APIResponse> AddEditDonorDetails(DonorModel model, string UserId);
       // Task<APIResponse> EditDonorDetails(DonorModel model, string UserId);
        Task<APIResponse> DeleteDonorDetails(long DonarId,string UserId);
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


        #region GetAllProvinceDetails
        APIResponse GetAllProvinceDetails();
        APIResponse GetAllStrengthConsiderationDetails();
        APIResponse GetAllGenderConsiderationDetails();
        APIResponse GetAllSecurityDetails();
        APIResponse GetAllSecurityConsiderationDetails();
        Task<APIResponse> GetAllDistrictvalueByProvinceId(int[] provinceId);
       
        APIResponse AddEditProjectproposals(long projectId, string UserId);
        APIResponse GetProjectproposalsById(long projectId);
        
        APIResponse AddEditProjectotherDetail(ProjectOtherDetail otherDetail, string UserId);
        APIResponse UploadOtherProposalFile(IFormFile file, string UserId, string Projectid,string fullPath,string fileNames);
        APIResponse AddEditProjectProposalDetail(ProposalDocModel model, string UserId);       
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

        #endregion
    }
}
