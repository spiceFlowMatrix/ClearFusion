using DataAccess.DbEntities;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Claims;

namespace HumanitarianAssistance.WebAPI.Filter
{
  
  public class CustomException : IExceptionFilter
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IHostingEnvironment _hostingEnvironment;
    private IProject _IProject;
    //public CustomException(IProject baseRepository) 
    //{
    //  UserManager<AppUser> userManager;
    //// IProject _Iproject
    //}
    public CustomException(
      UserManager<AppUser> userManager,
     IProject iProject,
     IHostingEnvironment hostingEnvironment
     )
    {
      _userManager = userManager;
      _IProject = iProject;
      _hostingEnvironment = hostingEnvironment;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }
    public void OnException(ExceptionContext context)
    {

      ExceptionModal exceptionModal = new ExceptionModal();

      var exceptionType = context.Exception.GetType();
      if (exceptionType == typeof(UnauthorizedAccessException))
      {
        exceptionModal.Message = "Unauthorized Access";
        exceptionModal.Status = (int)HttpStatusCode.InternalServerError;
      }
      else if (exceptionType == typeof(NotImplementedException))
      {
        exceptionModal.Message = "A server error occurred.";
        exceptionModal.Status = (int)HttpStatusCode.InternalServerError;
      }
      else
      {
        exceptionModal.Message = context.Exception.Message;
        exceptionModal.Status =(int)HttpStatusCode.InternalServerError;
       
      }
      context.ExceptionHandled = true;
      HttpResponse response = context.HttpContext.Response;
      response.StatusCode = (int)exceptionModal.Status;
      response.ContentType = "application/json";
      var err = exceptionModal.Message + " " + context.Exception.StackTrace;
      exceptionModal.Message = err;
        HttpRequest request = context.HttpContext.Request;
      exceptionModal.UserName = request.Headers["UserName"];
      exceptionModal.UserId = request.Headers["UserId"];
      if (exceptionModal != null)
        _IProject.SaveErrorlog((int)exceptionModal.Status, exceptionModal.Message, exceptionModal.UserName, exceptionModal.UserId);
      response.WriteAsync(err);
    }
  }

  public class ExceptionModal
  {
    public string Message { get; set; }
    public int Status { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
   
  }
}
