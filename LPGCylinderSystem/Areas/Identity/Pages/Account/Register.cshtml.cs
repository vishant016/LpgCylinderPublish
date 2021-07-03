using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using java.awt.print;
using LPGCylinderSystem.Data.Stores;
using LPGCylinderSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using VisioForge.Shared.MediaFoundation.OPM;

namespace LPGCylinderSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        //private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        //public RegisterModel(
        //    UserManager<IdentityUser> userManager,
        //    SignInManager<IdentityUser> signInManager,
        //    ILogger<RegisterModel> logger,
        //    IEmailSender emailSender)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _logger = logger;
        //    _emailSender = emailSender;
        //}

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public string ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    [Display(Name = "Email")]
        //    public string Email { get; set; }

        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
        //    [Display(Name = "Password")]
        //    public string Password { get; set; }

        //    [DataType(DataType.Password)]
        //    [Display(Name = "Confirm password")]
        //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //    public string ConfirmPassword { get; set; }



        //}

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    ReturnUrl = returnUrl;
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //}

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
        //        var result = await _userManager.CreateAsync(user, Input.Password);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User created a new account with password.");

        //            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //            var callbackUrl = Url.Page(
        //                "/Account/ConfirmEmail",
        //                pageHandler: null,
        //                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
        //                protocol: Request.Scheme);

        //            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
        //                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //            if (_userManager.Options.SignIn.RequireConfirmedAccount)
        //            {
        //                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
        //            }
        //            else
        //            {
        //                await _signInManager.SignInAsync(user, isPersistent: false);
        //                return LocalRedirect(returnUrl);
        //            }
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return Page();
        //}


        private readonly RoleManager<ApplicationRole> _roleManager;
        public ClassRepository<ApplicationUser> _classRepository;

      

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ClassRepository<ApplicationUser> classRepository,
        ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
           _classRepository = classRepository;   
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public object Bookings { get; private set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Name { get; set; }
            [Required]
            [RegularExpression(@"^[6789]\d{9}$",
        ErrorMessage = "Enter valid mobile number!")]
            public string MobileNo { get; set; }
            public string Connection_Id { get; set; }
            [Required]
            [StringLength(12, ErrorMessage = "The Aadhar number sholud be of 12 digits!", MinimumLength = 12)]
            [RegularExpression(@"\d{12}$",
         ErrorMessage = "The Aadhar number sholud be of 12 digits only!")]
            public string AadharNo { get; set; }
            public string AccountNo { get; set; }
            public string IFSC { get; set; }
            public bool SubsidyOpted { get; set; }
            public DateTime PreferedDeliveryTime { get; set; }
            
            public string Address { get; set; }
            
            public string Pincode { get; set; }
            public string Agency_Id { get; set; }
            public string Income { get; set; }
            public List<Booking> Bookings { get; set; }
            public bool IsAdmin { get; set; }
            public bool IsDeliveryBoy { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                List<Booking> booking = new List<Booking>();
                booking.Add(new Booking());
                booking.Add(new Booking());
                List <ServiceRequest > serviceRequests  = new List<ServiceRequest>();
                serviceRequests.Add(new ServiceRequest());
                List<Complaint> Complaint = new List<Complaint>();
                Complaint.Add(new Complaint());
                Random r = new Random();
                string ran = r.Next(10000, 99999).ToString();
                var users = await _classRepository.FindByConnectionIdAsync(ran);
                while (users!=null)
                {
                    ran = r.Next(10000, 99999).ToString();
                }


                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    IsAdmin = Input.IsAdmin,
                    IsDeliveryBoy=Input.IsDeliveryBoy,
                    Name = Input.Name,
                    MobileNo = Input.MobileNo,
                    AadharNo=Input.AadharNo,
                    AccountNo=Input.AccountNo,
                    IFSC=Input.IFSC,
                    Address=Input.Address,
                    Pincode=Input.Pincode,
                    Connection_Id = ran,

                    //Bookings=booking,
                    
                    //ServiceRequests=serviceRequests,
                    //Complaints=Complaint
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (user.IsAdmin)
                    {
                        await AddToRoleAdmin(user);
                        _logger.LogInformation("User added to admin group");
                    }
                    if (user.IsDeliveryBoy)
                    {
                        await AddToRoleDeliveryBoy(user);
                        _logger.LogInformation("User added to admin group");
                    }


                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.UserName, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    _classRepository.SendMailForPaper(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        async Task<bool> AddToRoleAdmin(ApplicationUser user)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new ApplicationRole("Admin"));
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            return true;
        }

        async Task<bool> AddToRoleDeliveryBoy(ApplicationUser user)
        {
            if (!await _roleManager.RoleExistsAsync("DeliveryBoy"))
            {
                await _roleManager.CreateAsync(new ApplicationRole("DeliveryBoy"));
            }

            await _userManager.AddToRoleAsync(user, "DeliveryBoy");

            return true;
        }

    }
}
