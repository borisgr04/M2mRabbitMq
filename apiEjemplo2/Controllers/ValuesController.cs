using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace apiEjemplo.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            var port = Request.Host.Port;

            return new string[] { "orders1", "orders2", port.Value.ToString () };
        }
    }
}