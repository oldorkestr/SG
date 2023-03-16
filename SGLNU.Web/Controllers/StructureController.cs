using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SGLNU.Web.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class StructureController : Controller
    {
        [HttpGet]
        public IActionResult Structure()
        {
            return View();
        }
    }
}
