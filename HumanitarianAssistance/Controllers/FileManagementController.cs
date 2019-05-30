using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/FileManagement/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class FileManagementController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private IFileManagement _iFileManagement;

    public FileManagementController(
      UserManager<AppUser> userManager,
      IFileManagement iFileManagement)
    {
      _userManager = userManager;
      _iFileManagement = iFileManagement;
    }

    [HttpPost]
    public async Task<APIResponse> SaveUploadedFileInfo([FromBody] FileManagementModel model)
    {
      APIResponse response = null;

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        model.CreatedById = user.Id;
        response = await _iFileManagement.SaveUploadedFileInfo(model);
      }

      return response;
    }

    [HttpPost]
    public async Task<APIResponse> GetSignedURL([FromBody] DownloadObjectGCBucketModel model)
    {
      APIResponse apiresponse = new APIResponse();
      apiresponse = await _iFileManagement.GetSignedURL(model);
      return apiresponse;
    }

  }
}
