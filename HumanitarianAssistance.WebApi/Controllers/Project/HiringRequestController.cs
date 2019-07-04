using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.ProjectManagement;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebAPI.Controllers.Project
{
    [Produces("application/json")]
    [Route("api/HiringRequest/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HiringRequestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHiringRequestService _hiringRequestService;
        public HiringRequestController(UserManager<AppUser> userManager, IHiringRequestService hiringRequestServ)
        {
            _userManager = userManager;
            _hiringRequestService = hiringRequestServ;
        }

        #region "GetProjectHiringRequestDetail"
        [HttpPost]
        public async Task<APIResponse> GetProjectHiringRequestDetail()
        {
            APIResponse response = await _hiringRequestService.GetallHiringRequestDetail();
            return response;
        }
        #endregion

        #region "AddHiringRequestDetail"
        [HttpPost]
        public async Task<APIResponse> AddHiringRequestDetail([FromBody]ProjectHiringRequestModel Model)
        {
            APIResponse apiRespone = null;
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.AddProjectHiringRequest(Model, id);
            }
            return apiRespone;
        }
        #endregion

        #region "AddHiringRequestDetail"
        [HttpPost]
        public async Task<APIResponse> EditHiringRequestDetail([FromBody]ProjectHiringRequestModel Model)
        {
            APIResponse apiRespone = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.EditProjectHiringRequest(Model, id);
            }
            return apiRespone;
        }
        #endregion

        #region "getAllEmployeeList"
        [HttpGet]
        public async Task<APIResponse> GetAllEmployeeList()
        {
            APIResponse apiresponse = new APIResponse();
            apiresponse = await _hiringRequestService.GetAllEmployeeList();
            return apiresponse;

        }
        #endregion

        #region "AddHiringRequestCandidate"
        [HttpPost]
        public async Task<APIResponse> AddHiringRequestCandidate([FromBody]HiringRequestCandidateModel Model)
        {
            APIResponse apiRespone = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.AddHiringRequestCandidate(Model, id);
            }
            return apiRespone;
        }
        #endregion

        #region "EditHiringRequestCandidate"
        [HttpPost]
        public async Task<APIResponse> EditHiringRequestCandidate([FromBody]ProjectHiringCandidateDetailModel Model)
        {
            APIResponse apiRespone = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.EditHiringRequestCandidate(Model, id);
            }
            return apiRespone;
        }
        #endregion

        #region "GetRequestedCandidiateListById"
        [HttpPost]
        public async Task<APIResponse> GetHiringCandidatesListById([FromBody] ProjectHiringCandidateDetailModel model)
        {
            APIResponse apiresponse = new APIResponse();
            apiresponse = await _hiringRequestService.GetAllCandidateList(model);
            return apiresponse;

        }

        #endregion

        #region "AddCandidateInterviewDetail"
        [HttpPost]
        public async Task<APIResponse> AddCandidateInterviewDetail([FromBody] CandidateInterViewModel model)
        {
            APIResponse apiRespone = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.AddCandidateInterviewDetail(model, id);
            }
            return apiRespone;
        }
        #endregion

        #region "HiringRequestSelectCandidate"
        [HttpPost]
        public async Task<APIResponse> HiringRequestSelectCandidate([FromBody] HiringSelectCandidateModel model)
        {
            APIResponse apiRespone = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.HiringRequestSelectCandidate(model, id);
            }
            return apiRespone;
        }
        #endregion

        #region "SelectCandidate"
        [HttpPost]
        public async Task<APIResponse> CompleteHiringRequest([FromBody] long hiringRequestId)
        {
            APIResponse apiRespone = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.CompleteHiringRequest(hiringRequestId, id);
            }
            return apiRespone;
        }
        #endregion

        #region "DeleteCandidatDetail"
        [HttpPost]
        public async Task<APIResponse> DeleteCandidatDetail([FromBody] ProjectHiringCandidateDetailModel model)
        {
            APIResponse apiRespone = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                var id = user.Id;
                apiRespone = await _hiringRequestService.DeleteCandidateDetail(model, id);
            }
            return apiRespone;
        }
        #endregion


    }



}
