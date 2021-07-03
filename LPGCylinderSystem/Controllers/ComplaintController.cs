
using LPGCylinderSystem.Data.Stores;
using LPGCylinderSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Controllers
{
    [Authorize]
    public class ComplaintController : Controller
    {
        // GET: ComplaintController
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClassRepository<ApplicationUser> _classRepository;
        // GET: AdminController
        public ComplaintController(ClassRepository<ApplicationUser> classRepository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _classRepository = classRepository;
        }
        [HttpGet]
        public ActionResult Index(String  id)
        {
           
            String Booking_id = id;
            ViewData["Booking_id"] = Booking_id;
            return View();
        }

        
        public async Task<ActionResult> DetailsAsync(Complaint complaint, IFormCollection collection, CancellationToken token)
        {
            complaint.Complaint_Id = ObjectId.GenerateNewId().ToString();
            complaint.Status = "pending";
            
            //complaint.Booking_Id = collection["Booking_Id"];
          //  await _classRepository.CreateComplaintAsync(complaint, token);
            var user1 = _userManager.FindByNameAsync(User.Identity.Name.ToUpper());
            var complaints = user1.Result.Complaints;
            if (complaint.Booking_Id == null && complaints == null)
            {
                ViewData["Error"] = "Error";
                return View();
            }
            if(complaint.Booking_Id == null)
            {
                return View(complaints);
            }
            if (complaints == null)
            {
                complaints = new List<Complaint>();
                complaints.Add(complaint);
                user1.Result.Complaints = complaints;
                await _userManager.UpdateAsync(user1.Result);
                return View(complaints);
            }
            else
            {
                complaints.Add(complaint);
                user1.Result.Complaints = complaints;
                await _userManager.UpdateAsync(user1.Result);
                return View(complaints);
            }

            
        }

        // GET: ComplaintController/Create
        //[HttpGet("{id}/{uid}")]
        [HttpGet]
        public ActionResult Resolve(String id,String uid)
        {

            TempData["uid"] = uid;
           var user= _classRepository.FindByIdAsync(uid);

            //var user =await  _classRepository.FindByComplainIdAsync(id);
            foreach (var com in user.Result.Complaints)
            {
                if (com.Complaint_Id == id)
                {
                    if (com.Status == "pending")
                    {
                        return View(com);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResolveAsync(Complaint complaint,String Booking_Id,String Complaint_Id,String Status)
        {
            var user = _classRepository.FindByIdAsync(TempData["uid"].ToString());
            var complaints = user.Result.Complaints;
            List<Complaint> comp = new List<Complaint>();
            foreach(var com in complaints)
            {
                if (com.Complaint_Id == Complaint_Id)
                {
                    com.Status = "Response";
                    com.Remark = complaint.Remark;
                   
                }
                comp.Add(com);
            }
            user.Result.Complaints = comp;
           await _userManager.UpdateAsync(user.Result);
            return RedirectToAction( "Index", "Admin");
        }

        [HttpGet]
        public ActionResult Resolved(String id)
        {
            var user1 = _userManager.FindByNameAsync(User.Identity.Name.ToUpper());
           List<Complaint> complaints= user1.Result.Complaints.FindAll(x => x.Booking_Id == id);
           List<Complaint> com = new List<Complaint>();
            foreach(var complaint in complaints)
            {
                if (complaint.Status == "Response")
                {
                    com.Add(complaint);           
                }
            }
            return View(com);

        }


            // POST: ComplaintController/Create
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



    }
}
