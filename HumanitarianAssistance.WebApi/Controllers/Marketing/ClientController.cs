using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Commands.Create;
using HumanitarianAssistance.Application.Marketing.Commands.Delete;
using HumanitarianAssistance.Application.Marketing.Commands.Update;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Application.Marketing.Queries;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Marketing
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Client/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Marketing))]
    public class ClientController : BaseController
    {
        
        [HttpPost]
        public async Task<ApiResponse> GetClientsPaginatedList([FromBody]GetClientsPaginatedListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetClientDetailsById([FromBody]String ClientId)
        {
            return await _mediator.Send(new GetClientDetailsByIdQuery { ClientId = Convert.ToInt32(ClientId) });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllClientList()
        {
            return await _mediator.Send(new GetAllClientQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetFilteredClientList([FromBody]FilterClientListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllCategoryList()
        {
            return await _mediator.Send(new GetAllCategoryQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> AddClient([FromBody]AddClientDetailsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditClient([FromBody]EditClientDetailsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteClient([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteClientDetailsCommand
            {
                ClientId = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> AddCategory([FromBody]CategoryModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (model.CategoryId == 0)
            {
                return await _mediator.Send(new AddCategoryCommand
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                });
            }
            else
            {
                return await _mediator.Send(new EditCategoryCommand
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    ModifiedById = userId,
                    ModifiedDate = DateTime.UtcNow
                });
            }
        }

    }
}
