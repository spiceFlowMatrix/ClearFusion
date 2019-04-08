using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HumanitarianAssistance.Controllers {
    [Route ("api/Dashboard/[Action]")]
    public class DashboardController : Controller {
        private IConfiguration _configuration;

        public DashboardController (IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetTrainingLink () {
            if (_configuration["DOCS_URL"] != null){
                return Ok(_configuration["DOCS_URL"].ToString());
            } else {
                return NotFound();
            }
        }
    }
}