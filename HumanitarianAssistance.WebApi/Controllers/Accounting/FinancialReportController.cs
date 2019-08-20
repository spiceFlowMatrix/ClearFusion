// using HumanitarianAssistance.Common.Enums;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace HumanitarianAssistance.WebApi.Controllers.Accounting
// {
//     [Produces("application/json")]
//     [Route("api/ExchangeRates/[Action]/")]
//     [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Accounting))]
//     [Authorize]

//     public class FinancialReportController : BaseController
//     {
//            [HttpPost]
//         public async Task<ApiResponse> GetSavedExchangeRates([FromBody] GetSavedExchangeRatesQuery filter)
//         {
//             return await _mediator.Send(filter);
//         }
//     }
// }