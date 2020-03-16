using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Commands.Create;
using HumanitarianAssistance.Application.Store.Commands.Delete;
using HumanitarianAssistance.Application.Store.Commands.Update;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Application.Store.Queries;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Store
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/StorePurchase/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.StorePurchase))]
    public class StorePurchaseController : BaseController
    {

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllInventoriesType()
        {
            var result = await Task.FromResult(_mediator.Send(new GetAllInventoriesTypeQuery { }));

            if (result.Exception == null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllPurchaseFilters()
        {
            PurchaseFilterDataSourceModel model = new PurchaseFilterDataSourceModel();

            try
            {

                var inventoriesResult = _mediator.Send(new GetAllInventoriesTypeQuery { });
                var receiptTypesResult = _mediator.Send(new GetAllReceiptTypeQuery { });
                var officeResult = _mediator.Send(new GetAllOfficeDetailQuery { });
                var currencyResult = _mediator.Send(new GetAllCurrencyQuery { });
                var projectResult = _mediator.Send(new GetAllProjectDetailsQuery { });

                if (projectResult.Exception == null)
                {
                    var projects = await projectResult;

                    model.ProjectModel = projects.data.ProjectDetailList
                                                 .Select(x => new ProjectModel
                                                 {
                                                     ProjectCode = x.ProjectCode,
                                                     ProjectId = x.ProjectId,
                                                     ProjectName = x.ProjectName,
                                                     ProjectCodeName = x.ProjectCode + " " + x.ProjectName
                                                 }).ToList();
                }

                if (inventoriesResult.Exception == null)
                {
                    model.InventoryTypes = await inventoriesResult;

                    if (model.InventoryTypes.Any())
                    {
                        var inventoryResult = _mediator.Send(new GetAllInventoriesQuery { AssetType = model.InventoryTypes.First().Id });

                        if (inventoryResult.Exception == null)
                        {
                            var inventories = await inventoryResult;

                            model.StoreInventoryModel = inventories.data.InventoryList;
                        }
                    }
                }

                if (receiptTypesResult.Exception == null)
                {
                    var receipts = await receiptTypesResult;
                    model.ReceiptTypes = receipts.data.ReceiptTypeList.Select(x => new ReceiptTypeModel
                    {
                        ReceiptTypeId = x.ReceiptTypeId,
                        ReceiptTypeName = x.ReceiptTypeName
                    }).ToList();
                }

                if (officeResult.Exception == null)
                {
                    var offices = await officeResult;
                    model.Offices = offices.data.OfficeDetailsList;
                }

                if (currencyResult.Exception == null)
                {
                    var currencies = await currencyResult;
                    model.CurrencyModel = currencies.data.CurrencyList;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetFilteredPurchaseList(GetFilteredPurchaseListQuery request)
        {
            var result = await Task.FromResult(_mediator.Send(request));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetItemDetailByItemId(long PurchaseId)
        {
            var result = await Task.FromResult(_mediator.Send(new GetItemDetailByPurchaseIdQuery { PurchaseId = PurchaseId }));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddStorePurchase([FromBody] AddStorePurchaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            var result = await Task.FromResult(_mediator.Send(command));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {

                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStorePurchaseById([FromQuery] long id)
        {
            var result = await Task.FromResult(_mediator.Send(new GetStorePurchaseByIdQuery { PurchaseId = id }));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportItemDataSource([FromQuery] int id)
        {
            var result = await Task.FromResult(_mediator.Send(new GetTransportItemDataSourceQuery { ItemGroupTransportType = id }));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EditStorePurchase([FromBody] EditStorePurchaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            var result = await Task.FromResult(_mediator.Send(command));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetVehicleGeneratorTrackerLogs(int id, long entityId)
        {
            var result = await Task.FromResult(_mediator.Send(new GetVehicleGeneratorTrackerLogsQuery { TransportType = id, EntityId = entityId }));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportItemCategoryType(long Id)
        {
            var result = await Task.FromResult(_mediator.Send(new GetTransportItemCategoryTypeQuery { ItemId = Id }));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetDefaultUnitTypeByItemId(int id)
        {
            var result = await Task.FromResult(_mediator.Send(new GetDefaultUnitTypeByItemIdQuery { Id = id }));
            return Ok(await result);

        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProcurementDetailWithReturnsList(int id)
        {
            var result = await Task.FromResult(_mediator.Send(new GetProcurementDetailWithReturnsListQuery { Id = id }));
            return Ok(await result);

        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddProcurementReturn([FromBody] AddProcurementReturnCommand command)
        {
            command.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await Task.FromResult(_mediator.Send(command));
            return Ok(await result);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteReturnProcurement([FromBody] long Id)
        {
            DeleteReturnProcurementCommand command = new DeleteReturnProcurementCommand();
            command.Id = Id;
            command.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await Task.FromResult(_mediator.Send(command));
            return Ok(await result);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProcurementDetailsByProcurementId(int id)
        {

            var result = await Task.FromResult(_mediator.Send(new GetProcurementDetailsByProcurementIdQuery { Id = id }));
            return Ok(await result);
        }



        [HttpPost]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetFilteredInventoryMasterList([FromBody] GetFilteredInventoryMasterListQuery command)
        {
           var result= await Task.FromResult( _mediator.Send(command));
           return Ok(await result);
        }
        
        // GetFilteredItemGroupList
          [HttpPost]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetFilteredItemGroupList([FromBody] GetFilteredItemGroupListQuery command)
        {
           var result= await Task.FromResult( _mediator.Send(command));
           return Ok(await result);
        }
          [HttpPost]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetFilteredItemList([FromBody] GetFilteredItemListQuery command)
        {
           var result= await Task.FromResult( _mediator.Send(command));
           return Ok(await result);
        }  [HttpPost]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetFilteredProjectList([FromBody] GetFilteredProjectListQuery command)
        {
           var result= await Task.FromResult( _mediator.Send(command));
           return Ok(await result);
        }  [HttpPost]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetFilteredBudegtList([FromBody] GetFilteredBudegtListQuery command)
        {
           var result= await Task.FromResult( _mediator.Send(command));
           return Ok(await result);
        }  [HttpPost]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetFilteredReceivedFromLocationList([FromBody] GetFilteredReceivedFromLocationListQuery command)
        {
           var result= await Task.FromResult( _mediator.Send(command));
           return Ok(await result);
        }  [HttpPost]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetFilteredReceivedFromEmpList([FromBody] GetFilteredReceivedFromEmpListQuery command)
        {
           var result= await Task.FromResult( _mediator.Send(command));
           return Ok(await result);
        }

    }
}