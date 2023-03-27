using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Entities;
using SGLNU.Web.ViewModels;
using SuLNU.Web.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGLNU.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailConfirmation _emailConfirmation;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(ILogger<HomeController> logger, IEmailConfirmation emailConfirmation, IUserService userService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _emailConfirmation = emailConfirmation;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Redirect()
        {
            if (User.Identity.Name != null)
            {
                var user = await _userService.GetByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    return RedirectToAction("Dashboard", user);
                }
                else
                {
                    return RedirectToAction("SendMembershipRequest");
                }
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Dashboard(UserDTO user)
        {
            if (user.Email != null)
            {
                return View(user);
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult SendMembershipRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMembershipRequest(SendMembershipRequestViewModel viewModel)
        {
            return View();
        }
    }
}
