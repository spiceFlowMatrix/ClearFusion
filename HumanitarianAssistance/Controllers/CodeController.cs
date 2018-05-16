using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.Controllers
{
  [Produces("application/json")]
  [Route("api/Code/[Action]")]
  public class CodeController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private readonly IOfficeDetails _iofficeDetail;
    private ICurrency _icurrency;
    private IJournalDetail _ijournalDetail;
    private IEmailSetting _iemailSetting;
    private IChartAccoutDetail _ichartAccoutDetail;
    private IExchangeRate _iexchangeRate;
    private IDesignation _idesignation;
    private IProjectBudget _iProjectBudget;
    private IProfession _iprofession;
    private ICode _icode;
    private IProjectDetails _iProjectDetails;
    public CodeController(
      UserManager<AppUser> userManager,
      IOfficeDetails iofficeDetail,
      ICurrency icurrency,
      IJournalDetail ijournalDetail,
      IEmailSetting iemailSetting,
      IChartAccoutDetail ichartAccoutDetail,
      IExchangeRate iexchangeRate,
      IDesignation idesignation,
      IProjectBudget iProjectBudget,
      IProfession iprofession,
      ICode icode,
      IProjectDetails iProjectDetails
      )
    {
      _userManager = userManager;
      _iofficeDetail = iofficeDetail;
      _icurrency = icurrency;
      _ijournalDetail = ijournalDetail;
      _iemailSetting = iemailSetting;
      _ichartAccoutDetail = ichartAccoutDetail;
      _iexchangeRate = iexchangeRate;
      _idesignation = idesignation;
      _iProjectBudget = iProjectBudget;
      _iprofession = iprofession;
      _icode = icode;
      _iProjectDetails = iProjectDetails;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore

      };
    }

    //Office Module Controller
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddOfficeDetail([FromBody]OfficeDetailModel model)
    {
      APIResponse apiRespone = null;
      //if (ModelState.IsValid)
      //{
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;

        apiRespone = await _iofficeDetail.AddOfficeDetails(model);
        //}
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditOfficeDetails([FromBody]OfficeDetailModel model)
    {
      APIResponse apiRespone = null;
      //if (ModelState.IsValid)
      //{
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;

        apiRespone = await _iofficeDetail.EditOfficeDetails(model);
      }
      //}
      //else
      //{
      //  apiRespone.StatusCode = 1000;
      //  apiRespone.Message = ModelState.AddModelError()
      //}
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> DeleteOfficeDetails([FromBody] OfficeDetailsModelDelete model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;

        apiRespone = await _iofficeDetail.DeleteOfficeDetails(model);
      }
      return apiRespone;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllOfficeDetails()
    {
      APIResponse apiRespone = null;
      apiRespone = await _iofficeDetail.GetAllOfficeDetails();
      return apiRespone;

    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetOfficeDetailsByOfficeCode(string OfficeCode)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iofficeDetail.GetOfficeDetailsByOfficeCode(OfficeCode);
      return apiRespone;
    }
    //Office Module Controller

    //Currency Module Controller
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddCurrency([FromBody] CurrencyModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      //if (ModelState.IsValid)
      //{

      //}

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icurrency.AddCurrency(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditCurrency([FromBody] CurrencyModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icurrency.EditCurrency(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllCurrency()
    {
      APIResponse response = await _icurrency.GetAllCurrency();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetCurrencyByCurrencyCode(string CurrencyCode)
    {
      APIResponse apiRespone = null;
      apiRespone = await _icurrency.GetCurrencyByCurrencyCode(CurrencyCode);
      return apiRespone;
    }
    //Currency Module Controller

    //Journal Details Module Controller
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddJournalDetail([FromBody] JournalDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ijournalDetail.AddJournalDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditJournalDetail([FromBody] JournalDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ijournalDetail.EditJournalDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> DeleteJournalDetail([FromBody] JournalDetailModelDelete model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;

        apiRespone = await _ijournalDetail.DeleteJournalDetail(model);
      }
      return apiRespone;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllJournalDetail()
    {
      APIResponse response = await _ijournalDetail.GetAllJournalDetail();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetJournalDetailByCode(int JournalCode)
    {
      APIResponse apiRespone = null;
      apiRespone = await _ijournalDetail.GetJournalDetailByCode(JournalCode);
      return apiRespone;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetJournalDetailByName(string JournalName)
    {
      APIResponse apiRespone = null;
      apiRespone = await _ijournalDetail.GetJournalDetailByName(JournalName);
      return apiRespone;
    }
    //Journal Details Module Controller


    //Email Setting Details Module Controller
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmailSettingDetail()
    {
      APIResponse response = await _iemailSetting.GetAllEmailSettingDetail();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmailSettingDetail([FromBody] EmailSettingModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iemailSetting.AddEmailSettingDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmailSettingDetail([FromBody] EmailSettingModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iemailSetting.EditEmailSettingDetail(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmailType()
    {
      APIResponse response = await _iemailSetting.GetAllEmailType();
      return response;
    }

    //Email Setting Details Module Controller


    //Chart Account Details Module Controller
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllChartAccountDetail()
    {
      APIResponse response = await _ichartAccoutDetail.GetAllChartAccountDetail();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllAccountLevel()
    {
      APIResponse response = await _ichartAccoutDetail.GetAllAccountLevel();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllAccountType()
    {
      APIResponse response = await _ichartAccoutDetail.GetAllAccountType();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddChartAccountDetail([FromBody] ChartAccountDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ichartAccoutDetail.AddChartAccountDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditChartAccountDetail([FromBody] ChartAccountDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ichartAccoutDetail.EditChartAccountDetail(model);
      return response;
    }

    //Chart Account Details Module Controller

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddExchangeRate([FromBody] ExchangeRateModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iexchangeRate.AddExchangeRate(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditExchangeRate([FromBody] ExchangeRateModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iexchangeRate.EditExchangeRate(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllExchangeRate()
    {
      APIResponse response = await _iexchangeRate.GetAllExchangeRate();
      return response;
    }


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddDesignation([FromBody] DesignationModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _idesignation.AddDesignation(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditDesignation([FromBody] DesignationModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _idesignation.EditDesignation(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllDesignation()
    {
      APIResponse response = await _idesignation.GetAllDesignation();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddProjectBudget([FromBody] ProjectBudgetModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        if (user != null)
        {
          var id = user.Id;
          model.CreatedById = id;
          model.IsDeleted = false;
          model.CreatedDate = DateTime.UtcNow;
        }
        APIResponse response = await _iProjectBudget.AddProjectBudget(model);
        return response;
      }
      else
      {
        APIResponse response = new APIResponse();
        response.Message = "Model is not valid";
        response.StatusCode = 403;
        return response;
      }


    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditAllProjectBudget([FromBody] ProjectBudgetModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        if (user != null)
        {
          var id = user.Id;
          model.ModifiedById = id;
          model.IsDeleted = false;
          model.ModifiedDate = DateTime.UtcNow;
        }
        APIResponse response = await _iProjectBudget.EditProjectBudget(model);
        return response;
      }
      else
      {
        APIResponse response = new APIResponse();
        response.Message = "Model is not valid";
        response.StatusCode = 403;
        return response;
      }
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllProjectBudget()
    {
      APIResponse response = await _iProjectBudget.GetProjectBudget();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllProjectDetails()
    {
      APIResponse response = await _iProjectDetails.GetAllProjectDetails();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddProjectDetails([FromBody] ProjectDetailModel model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var user = await _userManager.FindByEmailAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
          if (user != null)
          {
            var id = user.Id;
            model.CreatedById = id;
            model.IsDeleted = false;
            model.CreatedDate = DateTime.UtcNow;
          }
          APIResponse response = await _iProjectDetails.AddProjectDetails(model);
          return response;
        }
        else
        {
          APIResponse response = new APIResponse();
          response.Message = "Model is not valid";
          response.StatusCode = 403;
          return response;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditProjectDetails([FromBody] ProjectDetailModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        if (user != null)
        {
          var id = user.Id;
          model.ModifiedById = id;
          model.IsDeleted = false;
          model.ModifiedDate = DateTime.UtcNow;
        }
        APIResponse response = await _iProjectDetails.EditProjectDetails(model);
        return response;
      }
      else
      {
        APIResponse response = new APIResponse();
        response.Message = "Model is not valid";
        response.StatusCode = 403;
        return response;
      }
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddProfession([FromBody] ProfessionModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iprofession.AddProfession(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditProfession([FromBody] ProfessionModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iprofession.EditProfession(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllProfession()
    {
      APIResponse response = await _iprofession.GetAllProfession();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllCountry()
    {
      APIResponse response = await _icode.GetAllCountry();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllProvinceDetails(int CountryId)
    {
      APIResponse response = await _icode.GetAllProvinceDetails(CountryId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllNationality()
    {
      APIResponse response = await _icode.GetAllNationality();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllQualification()
    {
      APIResponse response = await _icode.GetAllQualification();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllInterviewRoundList()
    {
      APIResponse response = await _icode.GetAllInterviewRoundList();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddLeaveReasonDetail([FromBody] LeaveReasonDetailModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        apiRespone = await _icode.AddLeaveReasonDetail(model);
      }
      return apiRespone;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllLeaveReasonList()
    {
      APIResponse response = await _icode.GetAllLeaveReasonList();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddFinancialYearDetail([FromBody] FinancialYearDetailModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        apiRespone = await _icode.AddFinancialYearDetail(model);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditFinancialYearDetail([FromBody] FinancialYearDetailModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _icode.EditFinancialYearDetail(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllFinancialYearDetail()
    {
      APIResponse response = await _icode.GetAllFinancialYearDetail();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddBudgetLineType([FromBody] BudgetLineTypeModel model)
    {
      APIResponse response = null;
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        if (user != null)
        {
          var id = user.Id;
          model.CreatedById = id;
          model.CreatedDate = DateTime.UtcNow;
          model.IsDeleted = false;
          response = await _icode.AddBudgetLineType(model);
        }
      }
      else
      {
        response = new APIResponse();
        response.StatusCode = StaticResource.failStatusCode;
        response.Message = StaticResource.SomethingWentWrong;
      }
      return response;

    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeType()
    {
      APIResponse response = await _icode.GetAllEmployeeType();
      return response;
    }



    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetBudgetLineTypes()
    {
      APIResponse response = await _icode.GetBudgetLineTypes();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditLeaveReasonDetail([FromBody] LeaveReasonDetailModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        apiRespone = await _icode.EditLeaveReasonDetail(model);
      }
      return apiRespone;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetDepartmentsByOfficeId(int OfficeId)
    {
      APIResponse response = await _icode.GetDepartmentsByOfficeId(OfficeId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetCurrentFinancialYear()
    {
      APIResponse response = await _icode.GetCurrentFinancialYear();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddDepartment([FromBody] DepartmentModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icode.AddDepartmentDetails(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditDepartment([FromBody] DepartmentModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icode.EditDepartmentDetails(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllDepartment()
    {
      APIResponse response = await _icode.GetAllDepartment();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddQualificationDetails([FromBody] QualificationDetailsModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icode.AddQualificationDetails(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditQualifactionDetails([FromBody] QualificationDetailsModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icode.EditQualifactionDetails(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddSalaryHead([FromBody] SalaryHeadModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icode.AddSalaryHead(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditSalaryHead([FromBody]SalaryHeadModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _icode.EditSalaryHead(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllSalaryHead()
    {
      APIResponse response = await _icode.GetAllSalaryHead();
      return response;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllPensionRate()
    {
      APIResponse response = await _icode.GetAllPensionRate();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddPensionRate([FromBody] EmployeePensionRateModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.AddPensionRate(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditPensionRate([FromBody] EmployeePensionRateModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.EditPensionRate(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddAppraisalQuestion([FromBody] AppraisalQuestionModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.AddAppraisalQuestion(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditAppraisalQuestion([FromBody] AppraisalQuestionModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.EditAppraisalQuestion(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAppraisalQuestions([FromQuery] int OfficeId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.GetAppraisalQuestions(OfficeId);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeAppraisalDetails([FromBody] EmployeeAppraisalDetailsModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.AddEmployeeAppraisalDetails(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeAppraisalDetails([FromBody] EmployeeAppraisalDetailsModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.EditEmployeeAppraisalDetails(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeAppraisalDetails([FromQuery] int OfficeId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.GetAllEmployeeAppraisalDetails(OfficeId);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> ApproveEmployeeAppraisalRequest([FromQuery] int EmployeeAppraisalDetailsId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.ApproveEmployeeAppraisalRequest(EmployeeAppraisalDetailsId, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> ApproveEmployeeEvaluationRequest([FromQuery] int EmployeeEvaluationId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.ApproveEmployeeEvaluationRequest(EmployeeEvaluationId, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> RejectEmployeeEvaluationRequest([FromQuery] int EmployeeEvaluationId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.RejectEmployeeEvaluationRequest(EmployeeEvaluationId, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> ApproveEmployeeInterviewRequest([FromQuery] int InterviewDetailsId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.ApproveEmployeeInterviewRequest(InterviewDetailsId, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> RejectEmployeeInterviewRequest([FromQuery] int InterviewDetailsId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.RejectEmployeeInterviewRequest(InterviewDetailsId, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeAppraisalMoreDetails([FromBody] EmployeeAppraisalDetailsModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.AddEmployeeAppraisalMoreDetails(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeAppraisalMoreDetails([FromBody] EmployeeAppraisalDetailsModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.EditEmployeeAppraisalMoreDetails(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeAppraisalMoreDetails([FromQuery] int OfficeId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.GetAllEmployeeAppraisalMoreDetails(OfficeId);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeDetailByOfficeId([FromQuery] int OfficeId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.GetEmployeeDetailByOfficeId(OfficeId);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeDetailByEmployeeId([FromQuery] int EmployeeId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.GetEmployeeDetailByEmployeeId(EmployeeId);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddInterviewTechnicalQuestions([FromBody] InterviewTechnicalQuestions model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.AddInterviewTechnicalQuestions(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditInterviewTechnicalQuestions([FromBody] InterviewTechnicalQuestions model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.EditInterviewTechnicalQuestions(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllInterviewTechnicalQuestionsByOfficeId([FromQuery] int OfficeId)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.GetAllInterviewTechnicalQuestionsByOfficeId(OfficeId);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddExitInterview([FromBody]ExitInterviewModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.AddExitInterview(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditExitInterview([FromBody]ExitInterviewModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.EditExitInterview(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllExitInterview()
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _icode.GetAllExitInterview();
      }
      return response;
    }
  }
}
