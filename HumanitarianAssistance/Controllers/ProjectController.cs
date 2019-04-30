using System;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Project;
using System.Security.Claims;
using DataAccess.DbEntities.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Common;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/Project/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class ProjectController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IHostingEnvironment _hostingEnvironment;
    private IProject _iProject;
    private IProjectActivityService _iActivity;
    public ProjectController(
       UserManager<AppUser> userManager,
      IProject iProject,
      IHostingEnvironment hostingEnvironment,
      IProjectActivityService iActivity
      )
    {
      _userManager = userManager;
      _iProject = iProject;
      _iActivity = iActivity;
      _hostingEnvironment = hostingEnvironment;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }
    #region Donor information
    [HttpPost]

    public async Task<APIResponse> GetAllDonorFilterList([FromBody] DonorFilterModel donorFilterModel)
    {
      APIResponse apiresponse = await _iProject.GetAllDonorFilterList(donorFilterModel);
      return apiresponse;
    }
    [HttpGet]
    public async Task<APIResponse> GetAllDonorList()
    {
      APIResponse apiresponse = await _iProject.GetAllDonorList();
      return apiresponse;
    }


    [HttpPost]
    public async Task<APIResponse> AddEditDonorDetails([FromBody]DonorModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iProject.AddEditDonorDetails(model, id);
      }
      return apiResponse;
    }
    //[HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public async Task<APIResponse> EditDonorDetails([FromBody]DonorModel model)
    //{
    //  APIResponse apiResponse = null;
    //  var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //  if (user != null)
    //  {
    //    var id = user.Id;
    //    apiResponse = await _iProject.EditDonorDetails(model, id);
    //  }
    //  return apiResponse;
    //}

    [HttpPost]
    public async Task<APIResponse> DeleteDonorDetails([FromBody]long DonarId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.DeleteDonorDetails(DonarId, id);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> GetDonarListById([FromBody]long DonarId)
    {
      APIResponse apiresponse = await _iProject.GetDonarListById(DonarId);
      return apiresponse;
    }
    #endregion

    #region Sector Information

    [HttpGet]
    public async Task<APIResponse> GetAllSectorList()
    {
      APIResponse apiresponse = await _iProject.GetAllSectorList();
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddSectorDetails([FromBody]SectorModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iProject.AddSectorDetails(model, id);
      }
      return apiResponse;
    }
    [HttpPost]
    public async Task<APIResponse> EditSectorDetails([FromBody]SectorModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iProject.EditSectorDetails(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteSectorDetails([FromBody]SectorDetails model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        apiRespone = await _iProject.DeleteSectorDetails(model);
      }
      return apiRespone;
    }
    #endregion

    #region Program Information
    [HttpGet]
    public async Task<APIResponse> GetAllProgramList()
    {
      APIResponse apiresponse = await _iProject.GetAllProgramList();
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddProgramDetails([FromBody]ProgramModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iProject.AddProgramDetails(model, id);
      }
      return apiResponse;
    }



    [HttpPost]
    public async Task<APIResponse> EditProgramDetails([FromBody]ProgramModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iProject.EditProgramDetails(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteProgramDetails([FromBody]ProgramDetail model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        apiRespone = await _iProject.DeleteProgramDetails(model);
      }
      return apiRespone;
    }

    #endregion


    #region Area Information

    [HttpGet]
    public async Task<APIResponse> GetAllAreaList()
    {
      APIResponse apiresponse = await _iProject.GetAllAreaList();
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddAreaDetails([FromBody]AreaModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iProject.AddAreaDetails(model, id);
      }
      return apiResponse;
    }
    [HttpPost]
    public async Task<APIResponse> EditAreaDetails([FromBody]AreaModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iProject.EditAreaDetails(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteAreaDetails([FromBody]AreaDetail model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        apiRespone = await _iProject.DeleteAreaDetails(model);
      }
      return apiRespone;
    }
    #endregion

    #region GenderConsiderationList

    [HttpGet]
    public async Task<APIResponse> GenderConsiderationList()
    {
      APIResponse apiresponse = await _iProject.GenderConsiderationList();
      return apiresponse;
    }

    #endregion

    #region StrengthConsiderationDetailList

    [HttpGet]
    public async Task<APIResponse> StrengthConsiderationDetailList()
    {
      APIResponse apiresponse = await _iProject.StrengthConsiderationDetailList();
      return apiresponse;
    }

    #endregion

    #region SecurityDetailList

    [HttpGet]
    public async Task<APIResponse> SecurityDetailList()
    {
      APIResponse apiresponse = await _iProject.SecurityDetailList();
      return apiresponse;
    }

    #endregion

    #region SecurityConsiderationDetailList

    [HttpGet]
    public async Task<APIResponse> SecurityConsiderationDetailList()
    {
      APIResponse apiresponse = await _iProject.SecurityConsiderationDetailList();
      return apiresponse;
    }
    #endregion

    #region Add/Edit/Delete Project Info

    #region Project Details

    /// <summary>
    /// Add And Update New Project
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    ///

    [HttpPost]
    public async Task<APIResponse> AddEditProjectDetail([FromBody]ProjectDetailNewModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditProjectDetail(model, id);
      }

      return apiRespone;
    }
    /// <summary>
    /// Delete Created Project 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteProjectDetail([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.DeleteProjectDetail(ProjectId, id);
      }
      return apiRespone;
    }


    [HttpPost]
    public async Task<APIResponse> GetAllProjectFilterList([FromBody]ProjectFilterModel projectFilterModel)
    {
      APIResponse apiresponse = await _iProject.GetAllProjectFilterList(projectFilterModel);
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllProjectList()
    {
      APIResponse apiresponse = await _iProject.GetAllProjectList();
      return apiresponse;
    }
    [HttpPost]
    public APIResponse GetProjectListById([FromBody]long Id)
    {
      APIResponse apiresponse = _iProject.GetProjectListById(Id);

      return apiresponse;
    }


    [HttpPost]
    public APIResponse GetProjectOtherDetailById([FromBody]long Id)
    {
      APIResponse apiresponse = _iProject.GetOtherProjectListById(Id);

      return apiresponse;
    }







    #endregion

    #region Add/Update Assign Employee to Project
    /// <summary>
    /// Add Update Project Detail to Add/Update Assign Employee to Project
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddEditProjectAssignToEmployee([FromBody]ProjectAssignToModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddEditProjectAssignToEmployee(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteProjectAssignToEmployee([FromBody]ProjectAssignTo model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.DeleteProjectAssignToEmployee(model, id);
      }
      return apiRespone;
    }
    #endregion

    #region Add/Edit Project Program to Current Project
    [HttpPost]
    public async Task<APIResponse> AddEditProjectProgram([FromBody]ProjectProgramModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddEditProjectProgram(model, id);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> getProjectProgramById([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.getProjectProgramById(ProjectId);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> AddEditProjectArea([FromBody]ProjectAreaModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddEditProjectArea(model, id);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> getProjectAreaById([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.getProjectAreaById(ProjectId);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> getProjectSectorById([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.getProjectSectorById(ProjectId);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteProjectProgram([FromBody]ProjectProgram model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.DeleteProjectProgram(model, id);
      }
      return apiRespone;
    }
    #endregion

    #region Add/Edit Project Sector Area to Current Project
    [HttpPost]
    public async Task<APIResponse> AddEditProjectSector([FromBody]ProjectSectorModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddEditProjectSector(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteProjectSector([FromBody]ProjectSector model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.DeleteProjectSector(model, id);
      }
      return apiRespone;
    }


    [HttpPost]
    public async Task<APIResponse> DeleteProjectArea([FromBody]ProjectArea model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.DeleteProjectArea(model, id);
      }
      return apiRespone;
    }

    #endregion


    #endregion


    #region Project Communication
    [HttpPost]
    public async Task<APIResponse> GetChatByProjectId([FromBody]long ProjectId)
    {
      APIResponse apiresponse = await _iProject.GetChatByProjectId(ProjectId);
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddProjectChat([FromBody]ProjectCommunicationModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddProjectChat(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region Other Details dropdown

    [HttpGet]
    public APIResponse GetAllStrengthConsiderationDetails()
    {
      APIResponse response = _iProject.GetAllStrengthConsiderationDetails();
      return response;
    }
    [HttpGet]
    public APIResponse GetAllGenderConsiderationDetails()
    {
      APIResponse response = _iProject.GetAllGenderConsiderationDetails();
      return response;
    }
    [HttpGet]
    public APIResponse GetAllSecurityDetails()
    {
      APIResponse response = _iProject.GetAllSecurityDetails();
      return response;
    }

    [HttpGet]
    public APIResponse GetAllProvinceDetails()
    {
      APIResponse response = _iProject.GetAllProvinceDetails();
      return response;
    }

    [HttpPost]
    public APIResponse GetProvinceMultiSelectByProjectId([FromBody]long Id)
    {
      APIResponse apiresponse = _iProject.GetProvinceMultiSelectByProjectId(Id);

      return apiresponse;
    }


    [HttpPost]
    public APIResponse GetDistrictMultiSelectByProjectId([FromBody]long Id)
    {
      APIResponse apiresponse = _iProject.GetDistrictMultiSelectByProjectId(Id);

      return apiresponse;
    }


    [HttpPost]
    public async Task<APIResponse> AddEditDistrictMultiselect([FromBody]DistrictMultiSelectModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditDistrictMultiSelectDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddEditProvinceMultiselect([FromBody]ProvinceMultiSelectModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditProvinceMultiSelectDetail(Model, id);
      }
      return apiRespone;
    }


    [HttpPost]
    public APIResponse GetAllDistrictvalueByProvinceId([FromBody]int[] ProvinceId)
    {
      APIResponse response = _iProject.GetAllDistrictvalueByProvinceId(ProvinceId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> AddEditProjectotherDetail([FromBody]ProjectOtherDetailModel OtherDetail)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone =  _iProject.AddEditProjectotherDetail(OtherDetail, id);
      }
      return apiRespone;
    }

    [HttpGet]
    public APIResponse GetAllSecurityConsiderationDetails()
    {
      APIResponse response = _iProject.GetAllSecurityConsiderationDetails();
      return response;
    }
    [HttpPost]
    public APIResponse GetSecurityConsiMultiSelectByProjectId([FromBody]long Id)
    {
      APIResponse apiresponse = _iProject.GetSecurityConsiMultiSelectByProjectId(Id);

      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddEditSecurityConsiMultiselect([FromBody]SecurityConsiderationMultiSelectModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditSecurityConsidMultiDetail(Model, id);
      }
      return apiRespone;
    }

    //[HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public APIResponse GetOtherProjectListById([FromBody]long Id)
    //{
    //  APIResponse apiresponse = _iProject.GetOtherProjectListById(Id);

    //  return apiresponse;
    //}


    #endregion


    #region projectApproval
    [HttpPost]
    public async Task<APIResponse> AddApprovalProjectDetail([FromBody]ApproveProjectDetailModel model)
    {

      if (!string.IsNullOrEmpty(model.UploadedFile))
      {
        model.UploadedFile = model.UploadedFile.Substring(model.UploadedFile.IndexOf(',') + 1);
      }

      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddApprovalDetail(model, id);
      }

      return apiRespone;
    }

    #endregion

    #region Win/loss project approval
    [HttpPost]
    public async Task<APIResponse> WinApprovalProjectDetail([FromBody]WinApprovalProjectModel model)
    {
      APIResponse apiRespone = null;
      if (!string.IsNullOrEmpty(model.UploadedFile))
      {
        model.UploadedFile = model.UploadedFile.Substring(model.UploadedFile.IndexOf(',') + 1);
      }
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.WinApprovalDetail(model, id);
      }
      return apiRespone;
    }
    #endregion

    #region proposals
    //original addedit Project Proposal with google credential using read drirectory path  and  by environment variable comment.poonam 25/03/2019.
    //[HttpPost]
    //public async Task<APIResponse> AddEditProjectproposals([FromBody]long ProjectId)
    //{
    //  APIResponse apiRespone = null;
    //  var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //  if (user != null)
    //  {
    //    string logginUserEmailId = user.Email;
    //    var id = user.Id;
    //    apiRespone = await _iProject.AddEditProjectproposals(ProjectId, id, logginUserEmailId);
    //  }
    //  return apiRespone;
    //}


    /// <summary>
    /// start proposal on click 26/03/2019 pk 
    /// </summary>
    /// <param name="ProjectId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddEditProjectproposals([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        string logginUserEmailId = user.Email;
        var id = user.Id;
        apiRespone = await _iProject.StartProposal(ProjectId, id, logginUserEmailId);
      }
      return apiRespone;
    }


    [HttpPost]
    public APIResponse GetProjectproposalsById([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
      apiRespone = _iProject.GetProjectproposalsById(ProjectId);
      return apiRespone;
    }




    /// <summary>
    /// upload other proposal document using service account credentails new 26/03/2019 poonam
    /// </summary>
    [HttpPost, DisableRequestSizeLimit]
    public async Task<APIResponse> UploadEDIProposalFile([FromForm] IFormFile filesData, string projectId, string data)
    {
      APIResponse apiRespone = new APIResponse();
      string localFolderfullPath = string.Empty;
      try
      {

        var file = Request.Form.Files[0];
        long ProjectId = Convert.ToInt64(projectId);
        string ProposalType = data;
        string fileName = Request.Form.Files[0].FileName;

        string ext = Path.GetExtension(fileName).ToLower();
        if (ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif")
        {
          var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
          if (user != null)
          {
            string logginUserEmailId = user.Email;
            var id = user.Id;
            apiRespone = await _iProject.UploadOtherDocuments(file, id, ProjectId, fileName, logginUserEmailId, ProposalType, ext);

          }
        }
        else
        {
          apiRespone.StatusCode = StaticResource.FileNotSupported;
          apiRespone.Message = StaticResource.FileText;
        }

        //}
      }
      catch (System.Exception ex)
      {
        throw ex;
        //return Json("Upload Failed: " + ex.Message);
      }
      return apiRespone;
    }

    //proposal other detail due date ,assign to ,budget
    [HttpPost]
    public async Task<APIResponse> AddEditProjectProposalDetail([FromBody]ProposalDocModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        string logginUserEmailId = user.Email;
        var id = user.Id;
        apiRespone = _iProject.AddEditProjectProposalDetail(model, id, logginUserEmailId);
      }
      return apiRespone;
    }

    #endregion

    #region criteria evaluation
    [HttpPost]
    public async Task<APIResponse> AddEditDonorCriteria([FromBody]DonorCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditDonorCriteria(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddEditPurposeofInitiativeCriteria([FromBody]PurposeofInitiativeCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditPurposeofInitiativeCriteria(Model, id);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> AddEditEligibilityCriteriaDetail([FromBody]EligibilityCriteriaDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditEligibilityCriteriaDetail(Model, id);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> AddEditFeasibilityCriteria([FromBody]FeasibilityCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditFeasibilityCriteria(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public APIResponse GetAllCriteriaEvaluationDetail([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;

      apiRespone = _iProject.GetAllCriteriaEvaluationDetalByProjectId(ProjectId);

      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> AddEditPriorityCriteria([FromBody]PriorityCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditPriorityCriteria(Model, id);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> AddEditFinancialCriteria([FromBody]FinancialCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditFinancialCriteria(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddEditRiskCriteria([FromBody]RiskCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditRiskCriteria(Model, id);
      }
      return apiRespone;
    }
    [HttpPost]
    public async Task<APIResponse> AddEditTargetBeneficiary([FromBody]TargetBeneficiaryDetail Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditTargetBeneficiary(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddEditFinanacialProjectDetail([FromBody]FinancialProjectDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditFinancialProjectDetail(Model, id);
      }
      return apiRespone;
    }


    [HttpGet]
    public async Task<APIResponse> GetAllPriorityOtherDetailList()
    {
      APIResponse apiresponse = await _iProject.GetAllPriorityDetailList();
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllPriorityOtherDetailByProjectId([FromBody] long ProjectId)
    {
      APIResponse apiresponse = await _iProject.GetAllPriorityDetailByProjectId(ProjectId);
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddPriorityOtherDetail([FromBody]CEPriorityDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddPriorityOtherDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditPriorityOtherDetail([FromBody]CEPriorityDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.EditPriorityOtherDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeletePriorityDetails([FromBody]long PriorityOtherDetailId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.DeletePriorityOtherDetails(PriorityOtherDetailId, id);
      }
      return apiRespone;
    }

    #region feasibility Expert detail

    [HttpGet]
    public async Task<APIResponse> GetAllFeasibilityExpertDetailList()
    {
      APIResponse apiresponse = await _iProject.GetAllFeasibilityExpertList();
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllExpertDetailByProjectId([FromBody] long projectId)
    {
      APIResponse apiresponse = await _iProject.GetAllExpertDetailByProjectId(projectId);
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddFeasibleExpertOtherDetail([FromBody]CEFeasibilityExpertOtherModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddFeasibilityExpertDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditFeasibleExpertOtherDetail([FromBody]CEFeasibilityExpertOtherModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.EditFeasibilityExpertDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteFeasibleExpertDetails([FromBody]long PriorityOtherDetailId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.DeleteFeasibilityExperrtDetails(PriorityOtherDetailId, id);
      }
      return apiRespone;
    }
    #endregion



    #region Age group detail
    [HttpGet]
    public async Task<APIResponse> GetAllAgeGroupDetailList()
    {
      APIResponse apiresponse = await _iProject.GetAllAgeGroupDetailList();
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllAgeGroupByProjectId([FromBody]long projectId)
    {
      APIResponse apiresponse = await _iProject.GetAllAgeGroupByProjectId(projectId);
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddAgeGRoupDetail([FromBody]CEAgeGroupDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddAgeGroupDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditAGeGroupDetail([FromBody]CEAgeGroupDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.EditAgeGroupDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteAgeGroupDetails([FromBody]long ageGroupOtherDetailId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.DeleteAgeGroupDetails(ageGroupOtherDetailId, id);
      }
      return apiRespone;
    }





    #endregion

    #region OccupationDetail
    [HttpGet]
    public async Task<APIResponse> GetAllOccupationDetailList()
    {
      APIResponse apiresponse = await _iProject.GetAllOccuopationList();
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllOccupationByProjectId([FromBody] long projectId)
    {
      APIResponse apiresponse = await _iProject.GetAllOccupatiopnByProjectId(projectId);
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddOccupationDetail([FromBody]CEOccupationDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddOccupationOtherDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditOccupatiopnDetail([FromBody]CEOccupationDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.EditOccupationOtherDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteOccupationDetails([FromBody]long occupationOtherDetailId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.DeleteOccupationDetails(occupationOtherDetailId, id);
      }
      return apiRespone;
    }


    #endregion

    #region AssumptionDetail
    [HttpGet]
    public async Task<APIResponse> GetAllAssumptionDetailList()
    {
      APIResponse apiresponse = await _iProject.GetAllAssumptionList();
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllAssumptionByProjectId([FromBody]long projectId)
    {
      APIResponse apiresponse = await _iProject.GetAllAssumptionByProjectId(projectId);
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddAssumptionDetail([FromBody]CEAssumptionDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddAssumptionDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditAssumptionDetail([FromBody]CEAssumptionDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.EditAssumptionDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteAssumptionDetails([FromBody]long assumptionDetailId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.DeleteAssumptionDetails(assumptionDetailId, id);
      }
      return apiRespone;
    }

    #endregion



    #region DonorEligibilityDetail
    [HttpGet]
    public async Task<APIResponse> GetAllDonorEligibilityDetailList()
    {
      APIResponse apiresponse = await _iProject.GetAllDonorEligibilityDetailList();
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllDonorEligibilityByProjectId([FromBody]long projectId)
    {
      APIResponse apiresponse = await _iProject.GetAllDonorEligibilityDetailByProjectId(projectId);
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddDonorEligibilityDetail([FromBody]DonorEligibilityCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddDonorEligibilityOtherDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditDonorEligibilityDetail([FromBody]DonorEligibilityCriteriaModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.EditDonorEligibilityOtherDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteDonorEligibilityDetails([FromBody]long donorEligibilityDetailId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.DeleteDOnorEligibilityCriteriaOtherDetails(donorEligibilityDetailId, id);
      }
      return apiRespone;
    }



    [HttpPost]

    public async Task<APIResponse> AddEditCriteriaEvaluationSubmit([FromBody]ProjectDetailNewModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iProject.AddEditCriteriaEvalutionSubmitDetail(model);
      }
      return apiRespone;
    }


    #endregion


    #endregion

    #region Error Log
    //public async void SaveErrorlog(int status, string message)
    //{
    //  var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //  if (user != null)
    //  {
    //    var id = user.Id;
    //    _iProject.SaveErrorlog(status, message, null, id);
    //  }
    //}
    #endregion

    #region ProjectJob

    //project job

    [HttpGet]
    public async Task<APIResponse> GetProjectJobDetail()
    {
      APIResponse response = await _iProject.GetAllProjectJobDetail();
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> GetProjectJobDetailByProjectId([FromBody]long projectId)
    {
      APIResponse response = await _iProject.GetAllProjectJobDetail(projectId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> AddProjectJobDetail([FromBody]ProjectJobDetailModel Model)

    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddEditProjectJobDetail(Model, id);
      }
      return apiRespone;
    }

    /// <summary>
    /// delete selected projectJob
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteProjectJob([FromBody]long jobId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.DeleteProjectJob(jobId, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetProjectJobDetailByProjectJobId([FromBody]long projectJobId)
    {
      APIResponse response = await _iProject.GetAllProjectJobByProjectId(projectJobId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllProjectJobFilterList([FromBody]ProjectJobFilterModel projectJobFilterModel)
    {
      APIResponse apiresponse = await _iProject.GetAllProjectJobsFilterList(projectJobFilterModel);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetProjectJobDetailByBudgetLineId([FromBody]long budgetLineId)
    {
      APIResponse response = await _iProject.GetProjectJobDetailByBudgetLineId(budgetLineId);
      return response;
    }


    #endregion

    #region BudgetLine Detail

    [HttpPost]
    public async Task<APIResponse> AddBudgetLineDetail([FromBody]ProjectBudgetLineDetailModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iProject.AddEditProjectBudgetLineDetail(Model, id);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<APIResponse> GetProjectBudgetLineDetail()
    {
      APIResponse response = await _iProject.GetallBudgetLineDetail();
      return response;
    }


    [HttpPost]
    public async Task<APIResponse> GetProjectBudgetLineDetail([FromBody] long projectId)
    {
      APIResponse response = await _iProject.GetallBudgetLineDetail(projectId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> GetBudgetLineDetailByBudgetId([FromBody] int budgetId)
    {
      APIResponse response = await _iProject.GetBudgetLineDetailByBudgetId(budgetId);
      return response;
    }


    [HttpPost]
    public async Task<APIResponse> GetAllBudgetLineList([FromBody]BudgetLineFilterModel budgetNewFilterModel, long projectId)
    {
      APIResponse response = new APIResponse();
      response = await _iProject.GetAllBudgetFilterList(budgetNewFilterModel, projectId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> GetTransactionListByProjectId([FromBody] long projectId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iProject.GetTransactionListByProjectId(projectId, userName);
      }

      return response;
    }
    [HttpPost]
    public async Task<APIResponse> GetTransactionList([FromBody] TransactionDetailModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iProject.GetTransactionList(userName, model.CurrencyId, model.BudgetLineId);
      }

      return response;
    }
    #endregion

    #region ProjectActivity
    [HttpPost]
    public async Task<APIResponse> GetProjectActivityDetail([FromBody]long id)
    {
      APIResponse response = new APIResponse();
      response = await _iActivity.GetallProjectActivityDetail(id);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> AddProjectActivityDetail([FromBody]ProjectActivityModel model)
    {
      APIResponse apiRespone = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        apiRespone = await _iActivity.AddProjectActivityDetail(model, id);
      }

      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditProjectActivityDetail([FromBody]ProjectActivityModel model)
    {
      APIResponse apiRespone = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        apiRespone = await _iActivity.EditProjectActivityDetail(model, id);
      }

      return apiRespone;
    }



    [HttpPost]
    public async Task<APIResponse> DeleteActivityDetail([FromBody]long activityId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iActivity.DeleteProjectActivity(activityId, id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> StartProjectActivity([FromBody]long activityId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iActivity.StartProjectActivity(activityId, id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> EndProjectActivity([FromBody]long activityId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iActivity.EndProjectActivity(activityId, id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> MarkImplementationAsCompleted([FromBody]long activityId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iActivity.MarkImplementationAsCompleted(activityId, id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> MarkMonitoringAsCompleted([FromBody]long activityId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iActivity.MarkMonitoringAsCompleted(activityId, id);
      }
      return response;
    }


    [HttpPost]
    public async Task<APIResponse> AllActivityStatus([FromBody]long projectId)
    {
      APIResponse response = new APIResponse();
      response = await _iActivity.AllProjectActivityStatus(projectId);
      return response;
    }
    #endregion

    #region upload files for activity documents 28/03/2019
    [HttpPost, DisableRequestSizeLimit]
    public async Task<APIResponse> UploadProjectDocumnentFile([FromForm] IFormFile filesData, string activityId, string statusId)
    {
      APIResponse apiRespone = new APIResponse();
      string localFolderfullPath1 = string.Empty;
      try
      {
        //var filrec = Request.Form.Files;

        var file = Request.Form.Files[0];
        long activityID = Convert.ToInt64(activityId);
        int statusID = Convert.ToInt32(statusId);
        string fileName = Request.Form.Files[0].FileName;
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        if (ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif")
        {
          var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
          if (user != null)
          {
            string logginUserEmailId = user.Email;
            var id = user.Id;
            apiRespone = await _iActivity.UploadProjectActivityDocumentFile(file, id, activityID, fileName, logginUserEmailId, ext, statusID);
          }
        }
        else
        {
          apiRespone.StatusCode = StaticResource.FileNotSupported;
          apiRespone.Message = StaticResource.FileText;
        }
      }
      catch (Exception ex)
      {
        throw ex;
        //return Json("Upload Failed: " + ex.Message);
      }
      return apiRespone;
    }


    [HttpPost]
    public async Task<APIResponse> GetActivityDocumentDetail([FromBody]long activityId)
    {
      APIResponse response = new APIResponse();
      response = await _iActivity.GetUploadedDocument(activityId);
      return response;
    }

    #endregion

    #region "FilterProjectCashFlow"
    [HttpPost]
    public async Task<APIResponse> FilterProjectCashFlow([FromBody]ProjectCashFlowFilterModel model)
    {
      APIResponse apiresponse = await _iProject.FilterProjectCashFlow(model);
      return apiresponse;
    }


    [HttpPost]
    public async Task<APIResponse> FilterBudgetLineBreakdown([FromBody]BudgetLineBreakdownFilterModel model)
    {
      APIResponse apiResponse = new APIResponse();
      apiResponse = await _iProject.FilterBudgetLineBreakdown(model);
      return apiResponse;
    }
    #endregion


    #region demo upload file 

    [HttpPost, DisableRequestSizeLimit]
    public async Task<APIResponse> UploadFileDemo([FromForm] IFormFile fileData, string activityId, string statusId)
    {

      APIResponse apiResponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        string logginUserEmailId = user.Email;
        var userId = user.Id;
        apiResponse = await _iActivity.UploadFileDemo(fileData, userId, logginUserEmailId);
      }
      return apiResponse;
    }
    #endregion

    #region start proposal drag and drop pk 26/03/2019 

    [HttpPost, DisableRequestSizeLimit]
    public async Task<APIResponse> StartProposalDragAndDropFile([FromForm] IFormFile filesData, string projectId, string data)
    {
      APIResponse apiRespone = new APIResponse();
      string localFolderfullPath = string.Empty;
      try
      {

        var file = Request.Form.Files[0];
        long ProjectId = Convert.ToInt64(projectId);
        string ProposalType = data;
        string fileName = Request.Form.Files[0].FileName;

        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        if (ext == ".doc" || ext == ".docx")
        {
          var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
          if (user != null)
          {
            string logginUserEmailId = user.Email;
            var id = user.Id;
            apiRespone = await _iProject.StartProposalDragAndDrop(file, id, ProjectId, fileName, logginUserEmailId, ProposalType, ext);

          }
        }
        else
        {
          apiRespone.StatusCode = StaticResource.FileNotSupported;
          apiRespone.Message = StaticResource.FileText;
        }

        //}
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return apiRespone;
    }


    #endregion

    #region"UploadReviewFile"
    [HttpPost, DisableRequestSizeLimit]
    public async Task<APIResponse> UploadReviewFile([FromForm] IFormFile filesData, ApproveProjectDetailModel model)
    {
      APIResponse apiRespone = new APIResponse();
      string localFolderfullPath = string.Empty;
      try
      {
        if (filesData == null)
        {
          var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
          if (user != null)
          {
            string logginUserEmailId = user.Email;
            var id = user.Id;
            apiRespone = await _iProject.AddApprovalDetail( model,id);

          }
          else
          {
            apiRespone.StatusCode = StaticResource.FileNotSupported;
            apiRespone.Message = StaticResource.FileText;
          }
        }
        else
        {
          var file = Request.Form.Files[0];
          long ProjectId = Convert.ToInt64(model.ProjectId);
          string fileName = Request.Form.Files[0].FileName;

          string ext = System.IO.Path.GetExtension(fileName).ToLower();
          if (ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif")
          {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user != null)
            {
              string logginUserEmailId = user.Email;
              var id = user.Id;
              apiRespone = await _iProject.UploadReviewDragAndDrop(file, id, ProjectId, fileName, logginUserEmailId, ext, model);

            }
          }

          else
          {
            apiRespone.StatusCode = StaticResource.FileNotSupported;
            apiRespone.Message = StaticResource.FileText;
          }

        }   //}
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return apiRespone;
    }

    #endregion

    #region"UploadFinalizeFile"
    [HttpPost, DisableRequestSizeLimit]
    public async Task<APIResponse> UploadFinalizeFile([FromForm] IFormFile filesData, WinApprovalProjectModel model)
    {
      APIResponse apiRespone = new APIResponse();
      string localFolderfullPath = string.Empty;
      try
      {

        var file = Request.Form.Files[0];
        long ProjectId = Convert.ToInt64(model.ProjectId);
        string fileName = Request.Form.Files[0].FileName;

        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        if (ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif")
        {
          var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
          if (user != null)
          {
            string logginUserEmailId = user.Email;
            var id = user.Id;
            apiRespone = await _iProject.UploadFinalizeDragAndDrop(file, id, ProjectId, fileName, logginUserEmailId, ext, model);

          }
        }
        else
        {
          apiRespone.StatusCode = StaticResource.FileNotSupported;
          apiRespone.Message = StaticResource.FileText;
        }

        //}
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return apiRespone;
    }
    #endregion


    #region "DownloadFileFromBucket"
    [HttpPost]
    public async Task<APIResponse> DownloadFileFromBucket([FromBody]DownloadObjectGCBucketModel model)
    {
      APIResponse apiresponse = await _iProject.DownloadFileFromBucket(model);
      return apiresponse;
    }
    #endregion

    #region "DeleteActivityDocument"
    [HttpPost]
    public async Task<APIResponse> DeleteActivityDocument([FromBody]long activityDocumentId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        var userName = user.UserName;
        response = await _iActivity.DeleteActivityDocument(activityDocumentId, id);
      }
      return response;
    }
    #endregion



    #region "GetProjectProposalReport"
    [HttpPost]
    public async Task<APIResponse> GetProjectProposalReport([FromBody]ProjectProposalReportFilterModel model)
    {
      APIResponse apiresponse = await _iProject.GetProjectProposalReport(model);
      return apiresponse;
    }
    #endregion

    #region "ProjectIndicators"
    [HttpPost]
    public async Task<APIResponse> GetAllProjectIndicators([FromBody]PagingModel paging)
    {
      APIResponse apiresponse = await _iProject.GetAllProjectIndicators(paging);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetProjectIndicatorDetailById([FromBody]long indicatorId)
    {
      APIResponse apiresponse = await _iProject.GetProjectIndicatorDetailById(indicatorId);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddProjectIndicator()
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        string id = user.Id;
        apiresponse = await _iProject.AddProjectIndicator(id);
      }

      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> EditProjectIndicator([FromBody]EditIndicatorModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        string id = user.Id;
        apiresponse = await _iProject.EditProjectIndicator(model, id);
      }

      return apiresponse;
    }
    #endregion

    [HttpPost]
    public async Task<APIResponse> GetProjectProposalAmountSummary([FromBody]ProjectProposalReportFilterModel model)
    {
      APIResponse apiresponse = await _iProject.GetProjectProposalAmountSummary(model);
      return apiresponse;
    }

  }
}
