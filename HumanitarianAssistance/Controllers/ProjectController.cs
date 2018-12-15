using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net.Http.Headers;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/Project/[Action]")]
  public class ProjectController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IHostingEnvironment _hostingEnvironment;
    private IProject _iProject;
    public ProjectController(
       UserManager<AppUser> userManager,
      IProject iProject,
      IHostingEnvironment hostingEnvironment
      )
    {
      _userManager = userManager;
      _iProject = iProject;
      _hostingEnvironment = hostingEnvironment;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }
    #region Donor information
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllDonorList()
    {
      APIResponse apiresponse = await _iProject.GetAllDonorList();
      return apiresponse;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetDonarListById([FromBody]long DonarId)
    {
      APIResponse apiresponse = await _iProject.GetDonarListById(DonarId);
      return apiresponse;
    }
    #endregion
    
    #region Sector Information

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllSectorList()
    {
      APIResponse apiresponse = await _iProject.GetAllSectorList();
      return apiresponse;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllProgramList()
    {
      APIResponse apiresponse = await _iProject.GetAllProgramList();
      return apiresponse;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllAreaList()
    {
      APIResponse apiresponse = await _iProject.GetAllAreaList();
      return apiresponse;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GenderConsiderationList()
    {
      APIResponse apiresponse = await _iProject.GenderConsiderationList();
      return apiresponse;
    }

    #endregion

    #region StrengthConsiderationDetailList

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> StrengthConsiderationDetailList()
    {
      APIResponse apiresponse = await _iProject.StrengthConsiderationDetailList();
      return apiresponse;
    }

    #endregion

    #region SecurityDetailList

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> SecurityDetailList()
    {
      APIResponse apiresponse = await _iProject.SecurityDetailList();
      return apiresponse;
    }

    #endregion

    #region SecurityConsiderationDetailList

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEditProjectDetail([FromBody]ProjectDetailNewModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone =  _iProject.AddEditProjectDetail(model, id);
      }

      return apiRespone;
    }
    /// <summary>
    /// Delete Created Project 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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


    [HttpGet]
     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllProjectList()
    {
      APIResponse apiresponse = await _iProject.GetAllProjectList();
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public APIResponse GetProjectListById([FromBody]long Id)
    {
      APIResponse apiresponse = _iProject.GetProjectListById(Id);

      return apiresponse;
    }


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public APIResponse GetProjectOtherDetailById([FromBody]long Id)
    {
      APIResponse apiresponse =  _iProject.GetOtherProjectListById(Id);

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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetChatByProjectId([FromBody]long ProjectId)
    {
      APIResponse apiresponse = await _iProject.GetChatByProjectId(ProjectId);
      return apiresponse;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public APIResponse GetAllProvinceDetails()
    {
      APIResponse response =  _iProject.GetAllProvinceDetails();
      return response;
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public  APIResponse GetAllStrengthConsiderationDetails()
    {
      APIResponse response =  _iProject.GetAllStrengthConsiderationDetails();
      return response;
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public APIResponse GetAllGenderConsiderationDetails()
    {
      APIResponse response = _iProject.GetAllGenderConsiderationDetails();
      return response;
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public APIResponse GetAllSecurityDetails()
    {
      APIResponse response =  _iProject.GetAllSecurityDetails();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public APIResponse GetAllSecurityConsiderationDetails()
    {
      APIResponse response = _iProject.GetAllSecurityConsiderationDetails();
      return response;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllDistrictvalueByProvinceId([FromBody]int[] ProvinceId)
    {
      APIResponse response = await _iProject.GetAllDistrictvalueByProvinceId(ProvinceId);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEditProjectotherDetail([FromBody]ProjectOtherDetail OtherDetail)
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddApprovalProjectDetail([FromBody]ApproveProjectDetailModel model)
    {
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> WinApprovalProjectDetail([FromBody]WinApprovalProjectModel model)
    {
      APIResponse apiRespone = null;
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
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEditProjectproposals([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone =  _iProject.AddEditProjectproposals(ProjectId, id);
      }
      return apiRespone;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public  APIResponse GetProjectproposalsById([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
        apiRespone = _iProject.GetProjectproposalsById(ProjectId);
      return apiRespone;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> UploadEDIProposalFile()
    {
      APIResponse apiRespone = new APIResponse();
      string fullPath = string.Empty;
      try
      {
        var file = Request.Form.Files[0];
        string folderName = Path.Combine(Directory.GetCurrentDirectory(), "UploadotherDoc/");
        long count = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('_').Length;
        string ProjectId  = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('_')[count - 2];
        string DocType = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('_')[count - 1];
        string fileNames = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('_')[0];
        string ext = System.IO.Path.GetExtension(fileNames).ToLower();
        if (ext != ".jpeg" && ext != ".png")
        {
          //fileNames =fileNames;
          string webRootPath = _hostingEnvironment.WebRootPath;
          string newPath = Path.Combine(webRootPath, folderName);
          if (!Directory.Exists(newPath))
          {
            Directory.CreateDirectory(newPath);
          }
          string fileName = string.Empty;
          if (file.Length > 0)
          {
            //ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('_')[0];
            fileName = DocType + "_" + fileNames;
            fullPath = Path.Combine(newPath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
              file.CopyTo(stream);
            }
          }
          var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
          if (user != null)
          {
            var id = user.Id;
            apiRespone = _iProject.UploadOtherProposalFile(file, id, ProjectId, fullPath, fileName);
            if (apiRespone.StatusCode == StaticResource.successStatusCode)
            {
              DirectoryInfo di = new DirectoryInfo(folderName);
              FileInfo[] fi = di.GetFiles();
              FileInfo f = fi.Where(p => p.Name == fileName).FirstOrDefault();
              f.Delete();
            }
          }
        }
        else
        {
          apiRespone.StatusCode = StaticResource.FileNotSupported;
          apiRespone.Message = StaticResource.FileText;
        }
      
      }
      catch (System.Exception ex)
      {
        throw ex;
        //return Json("Upload Failed: " + ex.Message);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEditProjectProposalDetail([FromBody]ProposalDocModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = _iProject.AddEditProjectProposalDetail(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region criteria evaluation
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public APIResponse GetAllCriteriaEvaluationDetail([FromBody]long ProjectId)
    {
      APIResponse apiRespone = null;
     
        apiRespone = _iProject.GetAllCriteriaEvaluationDetalByProjectId(ProjectId);
      
      return apiRespone;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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



    #endregion

  }
}
