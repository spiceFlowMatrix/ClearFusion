using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HumanitarianAssistance.Entities.Models;
using DataAccess;
using HumanitarianAssistance.Service;
using Microsoft.AspNetCore.Authorization;
using DataAccess.DbEntities;

namespace HumanitarianAssistance.Controllers
{


  [Route("api/[controller]")]
  public class HomeController : Controller
  {
    private IAccountNoteDetails iaccountNote;
    public HomeController(IAccountNoteDetails _iaccountNote)
    {
      iaccountNote = _iaccountNote;
    }


    [HttpGet("~/ilovewebapi")]
    public async Task<string> GetIdByUser()
    {
      return "Hello";
    }
    [HttpPost]
    public async Task<string> AddAccountNote([FromBody]AccountNoteDetail obj)
    {
       await iaccountNote.AddNoteDetails(obj);
      return "Success";
    }
  }
}
