using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Marketing
{
  [Produces("application/json")]
  [Route("api/Contract/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class ContractController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IContractDetailsService _iContractDetailsService;
    private IMasterPageService _iMasterPageService;
    private IClientDetails _iclientDetailService;

    private ICode _iCodeService;

    public ContractController(UserManager<AppUser> userManager, IClientDetails clientDetails, IContractDetailsService iContractDetailsService, IMasterPageService iMasterPageService, ICode iCode)
    {
      _userManager = userManager;
      _iContractDetailsService = iContractDetailsService;
      _iMasterPageService = iMasterPageService;
      _iclientDetailService = clientDetails;
      _iCodeService = iCode;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    [HttpGet]
    public async Task<APIResponse> GetMasterPagesValues()
    {
      APIResponse apiRespone = null;
      apiRespone = await _iMasterPageService.GetMasterPagesValues();
      return apiRespone;
    }

    #region Contract Details

    [HttpPost]
    public async Task<APIResponse> ApproveContract([FromBody]ApproveContractModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
       if (user != null)
      {
        var id = user.Id;
        response = await _iContractDetailsService.ApproveContract(model, id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> GetFilteredContractList([FromBody]FilterContractModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iContractDetailsService.FilterContractList(model, id);
      }
      return response;
    }

    /// <summary>
    /// get contract details by id
    /// </summary>
    /// <param name="contractId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> GetContractDetailsById([FromBody] int contractId)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iContractDetailsService.GetContractDetailsById(contractId, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get all Contracts List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetContractsList()
    {
      APIResponse apiRespone = null;
      //var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      //if (user != null)
      //{
      //var id = user.Id;
      apiRespone = await _iContractDetailsService.GetAllContractDetails();
      //}
      return apiRespone;
    }

    [HttpGet]
    public async Task<APIResponse> GetContractsListByClient()
    {
      APIResponse apiRespone = null;
      //var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      //if (user != null)
      //{
      //var id = user.Id;
      apiRespone = await _iContractDetailsService.GetAllContractDetailsByClientId();
      //}
      return apiRespone;
    }

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetContractsPaginatedList([FromBody]ContractPaginationModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iContractDetailsService.GetContractsPaginatedList(model, id);
      }
      return apiRespone;
    }


    /// <summary>
    /// Add And Update New Job
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    ///
    [HttpPost]
    public async Task<APIResponse> AddEditContractDetail([FromBody]ContractDetailsModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iContractDetailsService.AddEditContractDetail(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetContractByClientId([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iContractDetailsService.GetContractByClientId(model, id);
      }
      return apiRespone;
    }    

    /// <summary>
    /// Delete Created Contract 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteContractDetail([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        //model.ModifiedById = id;
        //model.ModifiedDate = DateTime.UtcNow;
        apiRespone = await _iContractDetailsService.DeleteContractDetail(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region Activity Type

    [HttpPost]
    public async Task<APIResponse> GetActivityById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetActivityById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get Activity Type List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllActivityTypeList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllActivityType();
      return apiresponse;
    }

    /// <summary>
    /// Add New Activity Type
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddActivityType([FromBody]ActivityTypeModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditActivityType(model, id);       
      }
      return apiResponse;
    }    

    /// <summary>
    /// Delete Selected Activity Type
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteActivityType([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteActivityType(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region Quality

    [HttpPost]
    public async Task<APIResponse> GetQualityById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetQualityById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get Quality List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllQualityList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllQuality();
      return apiresponse;
    }

    /// <summary>
    /// Add New Quality
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddQuality([FromBody]QualityModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditQuality(model, id);      
      }
      return apiResponse;
    }

    /// <summary>
    /// Delete Selected Quality
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteQuality([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteQuality(model, id);
      }
      return apiRespone;
    }
    #endregion

    #region Language

    /// <summary>
    /// Get All Language List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllLanguageList()
    {
      APIResponse apiresponse = await _iCodeService.GetAllLanguage();
      return apiresponse;
    }

    /// <summary>
    /// Add new Language
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddLanguage([FromBody]LanguageModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iCodeService.AddLanguage(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Edit selected Language
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> EditLanguage([FromBody]LanguageModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iCodeService.EditLanguage(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Delete selected language
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteLanguage([FromBody]LanguageModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iCodeService.DeleteLanguage(model, id);
      }
      return apiRespone;
    }


    #endregion

    #region Medium

    [HttpPost]
    public async Task<APIResponse> GetMediumById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetMediumById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get all Medium List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllMediumList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllMedium();
      return apiresponse;
    }

    /// <summary>
    /// Add new Medium
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddMedium([FromBody]MediumModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;       
        apiResponse = await _iMasterPageService.AddEditMedium(model, id);        
      }
      return apiResponse;
    }    

    /// <summary>
    /// Delete Selected Medium
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteMedium([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteMedium(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region Time Category

    [HttpPost]
    public async Task<APIResponse> GetTimeCategoryById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetTimeCategoryById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get all time category list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllTimeCategoryList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllTimeCategory();
      return apiresponse;
    }

    /// <summary>
    /// Add new time category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddTime([FromBody]TimeCategoryModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditTimeCategory(model, id);        
      }
      return apiResponse;
    }

    /// <summary>
    /// Delete Selected Time Category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteTimeCategory([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteTimeCategory(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region Nature

    [HttpPost]
    public async Task<APIResponse> GetNatureById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetNatureById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// get all nature list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllNatureList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllNature();
      return apiresponse;
    }

    /// <summary>
    /// Add new nature
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddNature([FromBody]NatureModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditNature(model, id);        
      }
      return apiResponse;
    }

    /// <summary>
    /// Delete selected nature
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteNature([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteNature(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region Media Category

    /// <summary>
    /// get all media category list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllMediaCategoryList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllMediaCategory();
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetMediaCategoryById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetMediaCategoryById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Add new Media category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddMediaCategory([FromBody]MediaCategoryModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditMediaCategory(model, id);        
      }
      return apiResponse;
    }   

    /// <summary>
    /// Delete Selected media category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteMediaCategory([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteMediaCategory(model, id);
      }
      return apiRespone;
    }
    #endregion

    #region ClientDetails

    [HttpGet]
    public async Task<APIResponse> GetAllClientList()
    {
      APIResponse apiresponse = await _iclientDetailService.GetAllClient();
      return apiresponse;
    }
    [HttpPost]
    public async Task<APIResponse> AddClientDetails([FromBody]ClientDetailModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iclientDetailService.AddEditClientDetails(model, id);
      }
      return apiResponse;
    }
    [HttpPost]
    public async Task<APIResponse> EditClientDetails([FromBody]ClientDetailModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iclientDetailService.EditClientDetails(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteClientDetails([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        apiRespone = await _iclientDetailService.DeleteClientDetails(model, id);
      }
      return apiRespone;
    }

    #endregion

    #region Unit Rate

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetUnitRatePaginatedList([FromBody]UnitRatePaginationModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.GetUnitRatePaginatedList(model, id);
      }
      return apiRespone;
    }

    /// <summary>
    /// Get Unit Rate List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllUnitRateList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllUnitRateList();
      return apiresponse;
    }

    /// <summary>
    /// Get Unit Rate By Id
    /// </summary>
    /// <param name="UnitRateId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> GetUnitRateById([FromBody] string UnitRateId)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetUnitRateById(Convert.ToInt32(UnitRateId), id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Add Unit Rate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddUnitRate([FromBody]UnitRateModel model)
    {
       APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditUnitRate(model, id);

      }
      return apiResponse;
    }

    /// <summary>
    /// get unit rate by filters
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> GetUnitRateByActivityTypeId([FromBody]UnitRateModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetUnitRateByActivityTypeId(model, id);

      }
      return apiResponse;
    }

    /// <summary>
    /// Delete selected Unit Rate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteUnitRate([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteUnitRate(model, id);
      }
      return apiRespone;
    }

    #endregion


  }
}
