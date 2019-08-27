using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers
{
    [ApiController]
    [Route("api/Pdf/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.PdfExport))]
    [Authorize]
    public class PdfController : BaseController
    {
        [HttpGet]
        public async Task<ApiResponse> GetAllChartOfAccountHierarchyPdf()
        {
            return await _mediator.Send(new GetAllAccountsInHierarchyQuery());
        }

    }
}