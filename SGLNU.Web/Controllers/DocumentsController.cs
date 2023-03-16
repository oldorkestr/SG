using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGLNU.Web.Controllers
{
    [AllowAnonymous]
    public class DocumentsController : Controller
    {
        [AllowAnonymous]
        public IActionResult Regulation()
        {
            return View();
        }
    }
}
