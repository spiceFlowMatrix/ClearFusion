using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Commands.Common;
using HumanitarianAssistance.Application.Marketing.Commands.Create;
using HumanitarianAssistance.Application.Marketing.Commands.Delete;
using HumanitarianAssistance.Application.Marketing.Commands.Update;
using HumanitarianAssistance.Application.Marketing.Queries;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Marketing
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Contract/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Marketing))]
    public class ContractController : BaseController
    {
        [HttpGet]
        public async Task<ApiResponse> GetMasterPagesValues()
        {
            return await _mediator.Send(new GetMasterPagesValuesQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetFilteredContractList(FilterContractListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetContractDetailsById([FromBody]int contractId)
        {
            return await _mediator.Send(new GetContractDetailsByIdQuery { contractId = contractId });
        }
        [HttpGet]
        public async Task<ApiResponse> GetContractsList()
        {
            return await _mediator.Send(new GetAllContractDetailsQuery());
        }
        [HttpGet]
        public async Task<ApiResponse> GetContractsListByClient()
        {
            return await _mediator.Send(new GetContractsListByClientQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetContractsPaginatedList([FromBody]GetContractsPaginatedListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetContractByClientId([FromBody]int model)
        {
            return await _mediator.Send(new GetContractByClientIdQuery { ClientId = model });
        }
        [HttpPost]
        public async Task<ApiResponse> GetActivityById([FromBody]int model)
        {
            return await _mediator.Send(new GetActivityByIdQuery { ActivityTypeId = model });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllActivityTypeList()
        {
            return await _mediator.Send(new GetAllActivityTypeQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetQualityById([FromBody]int model)
        {
            return await _mediator.Send(new GetQualityByIdQuery { QualityId = model });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllQualityList()
        {
            return await _mediator.Send(new GetAllQualityQuery());
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllLanguageList()
        {
            return await _mediator.Send(new GetAllLanguageQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetMediumById([FromBody]int model)
        {
            return await _mediator.Send(new GetMediumByIdQuery { MediumId = model });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllMediumList()
        {
            return await _mediator.Send(new GetAllMediumListQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetTimeCategoryById([FromBody]int model)
        {
            return await _mediator.Send(new GetTimeCategoryByIdQuery { TimeCategoryId = model });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllTimeCategoryList()
        {
            return await _mediator.Send(new GetAllTimeCategoryQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetNatureById([FromBody]int model)
        {
            return await _mediator.Send(new GetNatureByIdQuery { NatureId = model });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllNatureList()
        {
            return await _mediator.Send(new GetAllNatureQuery());
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllMediaCategoryList()
        {
            return await _mediator.Send(new GetAllMediaCategoryQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetMediaCategoryById([FromBody]int model)
        {
            return await _mediator.Send(new GetMediaCategoryByIdQuery { MediaCategoryId = model });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllClientList()
        {
            return await _mediator.Send(new GetAllClientQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetUnitRatePaginatedList([FromBody]GetUnitRatePaginatedListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllUnitRateList()
        {
            return await _mediator.Send(new GetAllUnitRateListQuery()); 
        }
        [HttpPost]
        public async Task<ApiResponse> GetUnitRateById([FromBody]string UnitRateId)
        {
            return await _mediator.Send(new GetUnitRateByIdQuery{ UnitRateId = UnitRateId });
        }
        [HttpPost] 
        public async Task<ApiResponse> GetUnitRateByActivityTypeId([FromBody]GetUnitRateByActivityTypeIdQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> ApproveContract([FromBody]ApproveContractCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        
        [HttpPost]
        public async Task<ApiResponse> AddEditContractDetail([FromBody]AddEditContractDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }  
        [HttpPost]
        public async Task<ApiResponse> DeleteContractDetail([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteContractDetailCommand
            {
                ContractId= model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            } );
        } 
        [HttpPost]
        public async Task<ApiResponse> AddActivityType([FromBody]AddEditActivityTypeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById=userId;
            command.ModifiedDate=DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteActivityType([FromBody]int ActivityTypeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteActivityTypeCommand
            {
                ActivityTypeId=ActivityTypeId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            } );
        } 
        [HttpPost]
        public async Task<ApiResponse> AddQuality([FromBody]AddEditQualityCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById=userId;
            command.ModifiedDate=DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteQuality([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteQualityCommand
            {
                QualityId= model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            } );
        }
        [HttpPost]
        public async Task<ApiResponse> AddLanguage([FromBody]AddLanguageCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditLanguage([FromBody]EditLanguageCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteLanguage([FromBody]DeleteLanguageCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> AddMedium([FromBody]AddEditMediumCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById=userId;
            command.ModifiedDate=DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteMedium([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteMediumCommand
            {
                MediumId= model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            } );
        }
        [HttpPost]
        public async Task<ApiResponse> AddTime([FromBody]AddEditTimeCategoryCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById=userId;
            command.ModifiedDate=DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteTimeCategory([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteTimeCategoryCommand
            {
                TimeCategoryId= model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            } );
        }
        [HttpPost]
        public async Task<ApiResponse> AddNature([FromBody]AddEditNatureCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById=userId;
            command.ModifiedDate=DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteNature([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteNatureCommand
            {
                NatureId= model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            } );
        }
        [HttpPost]
        public async Task<ApiResponse> AddMediaCategory([FromBody]AddEditMediaCategoryCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById=userId;
            command.ModifiedDate=DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteMediaCategory([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteMediaCategoryCommand
            {
                MediaCategoryId= model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            } );
        }
        [HttpPost]
        public async Task<ApiResponse> AddClientDetails([FromBody]AddClientDetailsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditClientDetails([FromBody]EditClientDetailsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteClientDetails([FromBody]int model)
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
        public async Task<ApiResponse> AddUnitRate([FromBody]AddEditUnitRateCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById=userId;
            command.ModifiedDate=DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteUnitRate([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteUnitRateCommand
            {
                UnitRateId = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
    }
}
