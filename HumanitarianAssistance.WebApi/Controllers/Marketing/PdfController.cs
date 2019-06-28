using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;

namespace HumanitarianAssistance.WebAPI.Controllers.Marketing
{
  [Produces("application/json")]
  [Route("api/Pdf/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class PdfController : Controller
  {
    IUnitOfWork _uow;
    private IHostingEnvironment _hostingEnvironment;
    private IJobDetailsService _iJobDetailService;
    // private HttpContext currentContext;
    public PdfController(IJobDetailsService iJobDetailService, IUnitOfWork uow, UserManager<AppUser> userManager, IJobDetailsService iJobDetailsService, IHostingEnvironment environment)
    {
      this._uow = uow;
      _iJobDetailService = iJobDetailService;
      _hostingEnvironment = environment;
      //this.currentContext = currentContext;
    } 

   

    [HttpPost]
    //[Route("CreatePDF")]
    public async Task<APIResponse> CreatePDF([FromBody]int JobId)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iJobDetailService.CreatePDF(JobId);
      return apiRespone;

    }
  }
}
