using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Commands.Delete;
using HumanitarianAssistance.Application.Accounting.Commands.Update;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [Produces("application/json")]
    [Route("api/ExchangeRates/[Action]/")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Accounting))]
    [Authorize]
    public class ExchangeRatesController : BaseController
    {

        [HttpPost]
        public async Task<ApiResponse> GetSavedExchangeRates([FromBody] GetSavedExchangeRatesQuery filter)
        {
            return await _mediator.Send(filter);
        }

        [HttpPost]
        public async Task<ApiResponse> SaveSystemGeneratedExchangeRates([FromBody] List<GenerateExchangeRateModel> model)
        {
            return await _mediator.Send(new AddExchangeRateCommand
            {
                GenerateExchangeRateModel = model
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetExchangeRatesDetail([FromBody] GetExchangeRatesDetailQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> SaveExchangeRatesForAllOffices([FromBody] SaveExchangeRatesForAllOfficesCommand officeExchangeRateViewModel)
        {
            return await _mediator.Send(officeExchangeRateViewModel);
        }

        [HttpPost]
        public async Task<ApiResponse> VerifyExchangeRates([FromBody] DateTime model)
        {
            return await _mediator.Send(new VerifyExchangeRatesCommand { ExchangeRateDate = model });
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteExchangeRates([FromBody] DateTime model)
        {
            return await _mediator.Send(new DeleteExchangeRatesCommand { ExchangeRateDate = model });
        }
    }
}