using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LPGCylinderSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LPGCylinderSystem.Data.Stores;

namespace LPGCylinderSystem.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClassRepository<ApplicationUser> _classRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ClassRepository<ApplicationUser> classRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _classRepository = classRepository;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("ADMIN"))
            {
                return RedirectToAction("Index", "Admin");
            }
            if (User.IsInRole("DELIVERYBOY"))
            {
                return RedirectToAction("Index", "DeliveryBoy");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SubsidyAsync()
        {
            var user1 = await _userManager.FindByNameAsync(User.Identity.Name.ToUpper());
            ViewBag.opted = user1.SubsidyOpted;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SubsidyVerifyAsync(string income,string certno)
        {
            var user1 = await _userManager.FindByNameAsync(User.Identity.Name.ToUpper());
            if (Convert.ToInt32(income)<200000 && user1.SubsidyOpted==false)
            {
               
                user1.SubsidyOpted = true;
                await _userManager.UpdateAsync(user1);
                ViewBag.eli = "Congratulations!! You will receive your subsidy from next booking onwards:)";
            }
            else if(user1.SubsidyOpted == false)
            {
                ViewBag.eli = "Sorry,You are not eligible for Subsidy!!";
            }
            else
            {

                user1.SubsidyOpted = false;
                await _userManager.UpdateAsync(user1);
                ViewBag.eli = "We are thankful to you, You are opted out from Subsidy :)";

            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> PreferTimeAsync()
        {
            var user1 = await _userManager.FindByNameAsync(User.Identity.Name.ToUpper());
            return View(user1);
        }

        [Authorize]
        public async Task<IActionResult> SetTime(string tem)
        {
            var user1 = await _userManager.FindByNameAsync(User.Identity.Name.ToUpper());
            user1.PreferedDeliveryTime = tem;
            ViewBag.tem = tem;
            await _userManager.UpdateAsync(user1);
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
