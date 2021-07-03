using LPGCylinderSystem.Data.Stores;
using LPGCylinderSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Controllers
{
    [Authorize(Roles = "DELIVERYBOY")]
    public class DeliveryBoyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClassRepository<ApplicationUser> _classRepository;

        public string Sessiondid = "_did";

        public DeliveryBoyController(UserManager<ApplicationUser> userManager, ClassRepository<ApplicationUser> classRepository)
        {
            _userManager = userManager;
            _classRepository = classRepository;
        }
        
        public IActionResult Index(string dacstatus,string bbid)
        {   

            IEnumerable<ApplicationUser> users = _classRepository.GetuserssAsync();
            var user1 = _userManager.FindByNameAsync(User.Identity.Name.ToUpper());
            string id1 = user1.Result.Id.ToString();
            HttpContext.Session.SetString(Sessiondid, id1);

            int flag = 0;
            foreach (var user in users)
            {
                if (user.Bookings != null)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                ViewBag.error = "Error";
                return View();
            }
            else
            {
                ViewBag.did = id1;
                ViewBag.dacstatus = dacstatus;
                ViewBag.bbid = bbid;
                return View(users);
            }
            
        }

        public async Task<IActionResult> DeliverNowAsync(string uid,string id,string dac)
        {
            IEnumerable<ApplicationUser> users = _classRepository.GetuserssAsync();
            var user = _classRepository.FindByIdAsync(uid);
            var book = user.Result.Bookings;
            foreach (Booking b in user.Result.Bookings)
            {
                if (b.Booking_Id == id)
                {
                    if(dac!=null)
                    {
                        if(dac==b.DAC)
                        {
                            b.OrderStatus = "Delivered";
                        }
                        else
                        {
                            ViewBag.dacstatus = "Please enter valid DAC  ";
                            ViewBag.bbid ="for Booking ID "+ id;
                        }
                    }
                    else
                    {
                        b.OrderStatus = "Delivered";
                    }
                    
                    
                   break;
                }
            }
            ViewBag.did = HttpContext.Session.GetString(Sessiondid);
            user.Result.Bookings = book;
            await _userManager.UpdateAsync(user.Result);
            return RedirectToAction("Index", "DeliveryBoy", new { ViewBag.dacstatus,ViewBag.bbid });
        }
    }
}
