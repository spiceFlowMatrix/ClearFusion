using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.Controllers {
    [Route("api/Value/[Action]")]
    public class ValueController : Controller {
        [HttpGet]
        public string Get () {
            return "Value";
        }

    }
}