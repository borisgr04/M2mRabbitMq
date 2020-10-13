using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace apiEjemplo.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            var port = Request.Host.Port;

            return new string[] { "logistics1", "logistics2", port.Value.ToString () };
        }
    }
}