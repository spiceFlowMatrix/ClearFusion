using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HumanitarianAssistance.Common.Helpers;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Application.Project.Commands.Create;
using HumanitarianAssistance.Application.Project.Commands.Delete;
using HumanitarianAssistance.Application.Project.Commands.Update;
using HumanitarianAssistance.Application.Project.Commands.Common;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using System.Linq;

namespace HumanitarianAssistance.WebApi.Controllers.Project
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/ProjectLogistic/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Project))]
    [Authorize]
    public class ProjectLogisticController : BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public ProjectLogisticController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpPost]
        public async Task<ApiResponse> AddLogisticRequest([FromBody]AddLogisticRequestCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllLogisticRequest([FromBody]long ProjectId)
        {
            GetAllLogisticRequestQuery model = new GetAllLogisticRequestQuery();
            model.ProjectId = ProjectId;
            return await _mediator.Send(model);
        }
        
        [HttpPost]
        public async Task<ApiResponse> DeleteLogisticRequest([FromBody]long RequestId)
        {   
            DeleteLogisticRequestCommand model = new DeleteLogisticRequestCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetRequestDetailById([FromBody]long RequestId)
        {   
            GetRequestDetailByIdQuery model = new GetRequestDetailByIdQuery();
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }
    }

}