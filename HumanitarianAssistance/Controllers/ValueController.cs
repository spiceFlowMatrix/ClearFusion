using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.Controllers {
    [Route("")]
    public class ValueController : Controller {
        [HttpGet]
        public async Task<string> Get () {
            return "Value";
        }

    }
}