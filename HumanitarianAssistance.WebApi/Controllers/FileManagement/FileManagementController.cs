using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.FileManagement.Commands.Create;
using HumanitarianAssistance.Application.FileManagement.Commands.Delete;
using HumanitarianAssistance.Application.FileManagement.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.FileManagement
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/FileManagement/[Action]")]
    [Authorize]
    public class FileManagementController : BaseController
    {
        [HttpPost]
        public async Task<ApiResponse> SaveUploadedFileInfo([FromBody]SaveUploadedFileInfoCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> GetSignedURL([FromBody]GetSignedURLQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ApiResponse> GetDocumentFiles([FromBody]GetDocumentFilesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteDocumentFiles([FromBody]DeleteDocumentFilesCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(command);
        }
    }    
}
