using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Version 1.0", DateTime.Now.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR")) };
        }
    }
}