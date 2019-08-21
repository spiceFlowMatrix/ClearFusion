using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Application.Infrastructure;
using System.Threading.Tasks;
using System.Security.Claims;
using System;
using HumanitarianAssistance.Application.Store.Commands.Create;
using HumanitarianAssistance.Application.Store.Commands.Update;
using HumanitarianAssistance.Application.Store.Commands.Delete;
using HumanitarianAssistance.Application.Store.Queries;
using HumanitarianAssistance.Application.Store.Commands.Common;

namespace HumanitarianAssistance.WebApi.Controllers.Store
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Store/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Store))]
    [Authorize]
    public class StoreController : BaseController
    {

        #region "Store Inventories"

        [HttpGet]
        public async Task<ApiResponse> GetAllInventories(int? AssetType)
        {
            return await _mediator.Send(new GetAllInventoriesQuery { AssetType = AssetType });
        }

        [HttpPost]
        public async Task<ApiResponse> AddInventory([FromBody]AddInventoryCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditInventory([FromBody]EditInventoryCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteInventory([FromBody]DeleteInventoryCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        #endregion

        #region "Store Items"

        [HttpPost]
        public async Task<ApiResponse> AddInventoryItems([FromBody]AddInventoryItemsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditInventoryItems([FromBody]EditInventoryItemsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteInventoryItems([FromBody]DeleteInventoryItemsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllInventoryItems(long Id)
        {
            return await _mediator.Send(new GetAllInventoryItemsQuery { ItemGroupId = Id });
        }

        #endregion

        #region "Store Item Types"

        [HttpPost]
        public async Task<ApiResponse> AddInventoryItemsType([FromBody]AddInventoryItemsTypeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditInventoryItemsType([FromBody]EditInventoryItemsTypeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteInventoryItemsType([FromBody]DeleteInventoryItemsTypeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllInventoryItemsType()
        {
            return await _mediator.Send(new GetAllInventoryItemsTypeQuery());
        }
        #endregion

        #region "Store Purchase"

        //Not used in front end
        [HttpGet]
        public async Task<ApiResponse> GetSerialNumber(string serialNumber)
        {
            return await _mediator.Send(new GetSerialNumberQuery { serialNumber = serialNumber });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllPurchasesByItem(string itemId)
        {
            return await _mediator.Send(new GetAllPurchasesByItemQuery { itemId = itemId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddPurchase([FromBody]AddPurchaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditPurchase([FromBody]EditPurchaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeletePurchase([FromBody]DeletePurchaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        #endregion

        #region "Store Order"

        [HttpPost]
        public async Task<ApiResponse> AddItemOrder([FromBody] AddItemOrderCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditItemOrder([FromBody] EditItemOrderCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteItemOrder([FromBody] DeleteItemOrderCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllItemsOrder(string ItemId)
        {
            return await _mediator.Send(new GetAllItemsOrderQuery
            {
                ItemId = ItemId
            });
        }

        #endregion

        #region "Purchase Unit Type"

        [HttpPost]
        public async Task<ApiResponse> AddPurchaseUnitType([FromBody]AddPurchaseUnitTypeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditPurchaseUnitType([FromBody] EditPurchaseUnitTypeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeletePurchaseUnitType([FromBody] DeletePurchaseUnitTypeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }


        [HttpGet]
        public async Task<ApiResponse> GetAllPurchaseUnitType()
        {
            return await _mediator.Send(new GetAllPurchaseUnitTypeQuery());
        }

        #endregion

        #region "Others"

        [HttpGet]
        public async Task<ApiResponse> GetItemAmounts(string ItemId)
        {
            return await _mediator.Send(new GetItemAmountsQuery
            {
                ItemId = ItemId
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetProcurementSummary(int EmployeeId, int CurrencyId)
        {
            return await _mediator.Send(new GetProcurementSummaryQuery
            {
                EmployeeId = EmployeeId,
                CurrencyId = CurrencyId
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllDepreciationByFilter([FromBody]GetAllDepreciationByFilterQuery depretiationFilter)
        {
            return await _mediator.Send(depretiationFilter);
        }

        [HttpPost]
        public async Task<ApiResponse> UpdateInvoice([FromBody]UpdateInvoiceCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllPurchaseInvoices([FromQuery]string PurchaseId)
        {
            return await _mediator.Send(new GetAllPurchaseInvoicesQuery()
            {
                PurchaseId = PurchaseId
            });
        }

        [HttpPost]
        public async Task<ApiResponse> UpdatePurchaseImage([FromBody]UpdatePurchaseImageCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        //[HttpPost]
        //public async Task<ApiResponse> AddItemSpecificationsDetails([FromBody]List<ItemSpecificationDetailModel> model)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    return await _mediator.Send(new AddItemSpecificationsDetailsCommand
        //    {
        //        ItemSpecificationDetail = model,
        //        CreatedById = userId,
        //        CreatedDate = DateTime.UtcNow
        //    });
        //}

        [HttpPost]
        public async Task<ApiResponse> EditItemSpecificationsDetails([FromBody]EditItemSpecificationsDetailsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllItemSpecificationsDetails([FromQuery]string ItemId, int ItemTypeId, int OfficeId)
        {
            return await _mediator.Send(new GetAllItemSpecificationsDetailsQuery
            {
                ItemId = ItemId,
                ItemTypeId = ItemTypeId,
                OfficeId = OfficeId
            });
        }

        [HttpPost]
        public async Task<ApiResponse> AddItemSpecificationsMaster([FromBody]AddItemSpecificationsMasterCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditItemSpecificationsMaster([FromBody]EditItemSpecificationsMasterCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetItemSpecificationsMaster([FromQuery]int ItemTypeId, int OfficeId)
        {
            return await _mediator.Send(new GetItemSpecificationsMasterQuery
            {
                ItemTypeId = ItemTypeId,
                OfficeId = OfficeId
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllStatusAtTimeOfIssue()
        {
            return await _mediator.Send(new GetAllStatusAtTimeOfIssueQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllReceiptType()
        {
            return await _mediator.Send(new GetAllReceiptTypeQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetInventoryCode([FromQuery] int Id)
        {
            return await _mediator.Send(new GetInventoryCodeQuery()
            {
                Id = Id
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetInventoryItemCode([FromQuery] long Id)
        {
            return await _mediator.Send(new GetInventoryItemCodeQuery()
            {
                Id = Id
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllStoreSourceType()
        {
            return await _mediator.Send(new GetAllStoreSourceTypeQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllStoreSourceCode(int? typeId)
        {
            return await _mediator.Send(new GetAllStoreSourceCodeQuery { typeId = typeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddStoreSourceCode([FromBody]AddStoreSourceCodeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetStoreTypeCode([FromQuery]int CodeTypeId)
        {
            return await _mediator.Send(new GetStoreTypeCodeQuery { CodeTypeId = CodeTypeId });
        }

        [HttpPost]
        public async Task<ApiResponse> EditStoreSourceCode([FromBody]EditStoreSourceCodeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> DeleteStoreSourceCode([FromQuery]int Id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteStoreSourceCodeCommand
            {
                storeSourceCodeId = Id,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllPaymentTypes()
        {
            return await _mediator.Send(new GetAllPaymentTypesQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> AddPaymentTypes([FromBody]AddPaymentTypesCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditPaymentTypes([FromBody]EditPaymentTypesCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpDelete]
        public async Task<ApiResponse> DeletePaymentTypes([FromQuery] int PaymentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeletePaymentTypesCommand
            {
                PaymentId = PaymentId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> VerifyPurchase([FromBody]VerifyPurchaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> UnverifyPurchase([FromBody]UnverifyPurchaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddStoreItemGroup([FromBody]AddStoreItemGroupCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetStoreGroupItemCode([FromQuery]string Id)
        {
            return await _mediator.Send(new GetStoreGroupItemCodeQuery { inventoryId = Id });
        }

        [HttpPost]
        public async Task<ApiResponse> EditStoreItemGroup([FromBody]EditStoreItemGroupCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllStoreItemGroups([FromQuery]string Id)
        {
            return await _mediator.Send(new GetAllStoreItemGroupsQuery { inventoryId = Id });
        }

        #endregion 
    }
}
