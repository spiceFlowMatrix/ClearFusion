using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Common;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Delete;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.HR {
    [Produces ("application/json")]
    [Route ("api/AuditLog/[Action]")]
    [Authorize]
    public class AuditLogController : BaseController {
        [HttpPost]
        public async Task<IActionResult> GetAuditLogDetails ([FromBody] int EmployeeId) {
            return Ok(await _mediator.Send (new GetAuditLogDetailsQuery { EmployeeId = EmployeeId }));
        }

    }
}