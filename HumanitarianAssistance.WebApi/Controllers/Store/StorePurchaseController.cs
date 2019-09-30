using System;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Queries;
using HumanitarianAssistance.Application.Infrastructure;
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
    [AllowAnonymous]
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
                    model.ReceiptTypes= receipts.data.ReceiptTypeList.Select(x=> new ReceiptTypeModel {
                        ReceiptTypeId= x.ReceiptTypeId,
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
    }
}