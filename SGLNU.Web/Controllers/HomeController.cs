using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Enteties;
using SuLNU.Web.Models;
using SuLNU.Web.Models.Interfaces;
using SuLNU.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SGLNU.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailConfirmation _emailConfirmation;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IEmailConfirmation emailConfirmation, IUserService userService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _emailConfirmation = emailConfirmation;
            _userService = userService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index(HomeViewModel homeViewModel)
        {
            return View(homeViewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MailSended()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendQuestion(HomeViewModel homeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailConfirmation.SendEmailAsync("orest.onyshchenko@gmail.com",
                 $"Питання студента {homeViewModel.Name}, {homeViewModel.Email}",
                 $"Студент запитується - {homeViewModel.FeedBackDescription}",
                 homeViewModel.Email);
                }
                catch (Exception)
                {
                    ErrorViewModel error = new ErrorViewModel();
                    
                    return View("Error", error);
                }
            }
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
