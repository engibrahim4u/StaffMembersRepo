using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.EmailService;
using WebApp.Helper;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Models.Repository;
using WebApp.Services;

namespace WebApp.Controllers
{
    //namespace StoreProject.Controllers 
    //{
        [Authorize]
        public class AccountController : Controller
        {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAcademicDegreeRepository academicDegrees;
        private readonly IScientificLevelRepository scientificLevels;
        private readonly ICountryRepository countries;
        private readonly IEMailRepository eMails;


        //private readonly IStudyLevelRepository studyLevels;
        //private readonly IStudentRepository students;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;

            public IStringLocalizer<AccountController> Localizer { get; }

            public AccountController(UserManager<ApplicationUser> userManager
                                    , SignInManager<ApplicationUser> signInManager,
                                     RoleManager<IdentityRole> roleManager,
                                     IAcademicDegreeRepository _academicDegrees,
                                     IScientificLevelRepository _scientificLevels,
                                     ICountryRepository _countries,
                                      //IStudentRepository _students,

                                      IEMailRepository _eMails,
                                      IEmailSender emailSender,
                                      IStringLocalizer<AccountController> _localizer,
                                    ILogger<AccountController> logger)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
            academicDegrees = _academicDegrees;
            scientificLevels = _scientificLevels;
            countries = _countries;
            eMails = _eMails;

            //studyLevels = _studyLevels;
            //students = _students;
            _emailSender = emailSender;
                Localizer = _localizer;
                _logger = logger;
            }

            [AllowAnonymous]
            private void AddRoles()
            {
                if (!_roleManager.RoleExistsAsync(Constants.NormalRoleName).Result)
                {
                    var roleresult = _roleManager.CreateAsync(new IdentityRole { Name = Constants.NormalRoleName }).Result;
                }
                if (!_roleManager.RoleExistsAsync(Constants.AdminRoleName).Result)
                {
                    var roleresult = _roleManager.CreateAsync(new IdentityRole { Name = Constants.AdminRoleName }).Result;
                }
                if (!_roleManager.RoleExistsAsync(Constants.SuperAdminRoleName).Result)
                {
                    var roleresult = _roleManager.CreateAsync(new IdentityRole { Name = Constants.SuperAdminRoleName }).Result;
                }
            if (!_roleManager.RoleExistsAsync(Constants.SupervisorRoleName).Result)
            {
                var roleresult = _roleManager.CreateAsync(new IdentityRole { Name = Constants.SupervisorRoleName }).Result;
            }
            if (!_roleManager.RoleExistsAsync(Constants.Instructor).Result)
            {
                var roleresult = _roleManager.CreateAsync(new IdentityRole { Name = Constants.Instructor }).Result;
            }
        }

            [HttpGet]
            public IActionResult Index()
            {
                if (User.IsInRole("user")) return RedirectToAction("Index", "User");
                if (User.IsInRole("instructor")) return RedirectToAction("Index", "Instructor");

            return View();
            }
            [AllowAnonymous]
            public IActionResult Register()
            {
                if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Account");
            RegisterVM model = new RegisterVM();
            return View(model);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            [AllowAnonymous]
            public async Task<IActionResult> Register(RegisterVM model)
            {
            var emailCode = EmailCode.NewRegister.ToString();
            var emailData = await eMails.FindAsync(emailCode);
            model.Email = model.Email.ToLower();
            var user = new ApplicationUser
            { Name=model.FullName, Email = model.Email, UserName = model.Email, Mobile = model.Mobile, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                switch (model.RegisterType)
                {
                    case "trainee":
                        await _userManager.AddToRoleAsync(user, Constants.NormalRoleName);
                        await _signInManager.SignInAsync(user, false);
                        await SendSuccessRegistrationMail(model, emailData);

                        return RedirectToAction("Index", "Trainee");
                    case "instructor":
                        await _userManager.AddToRoleAsync(user, Constants.Instructor);
                        await _signInManager.SignInAsync(user, false);
                        await SendSuccessRegistrationMail(model, emailData);

                        return RedirectToAction("Index", "Instructor");

                    default:
                        await SendSuccessRegistrationMail(model, emailData);
                        await _userManager.AddToRoleAsync(user, Constants.NormalRoleName);
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Account");


                }

                //ViewBag.Created = "Success";
            }
            AddErrors(result);
            return View(model);

          
            }
       public async Task AddUserRoles(ApplicationUser user,string registerType)
        {
            switch (registerType)
            {
                case "trainee":
                    await _userManager.AddToRoleAsync(user, Constants.NormalRoleName);

                    break;
                case "instructor":
                    await _userManager.AddToRoleAsync(user, Constants.Instructor);

                    break;
                case "Both":
                    await _userManager.AddToRoleAsync(user, Constants.AuthorRoleName);
                    await _userManager.AddToRoleAsync(user, Constants.AuditorRoleName);

                    break;
            }
        }
     
        public async Task SendSuccessRegistrationMail(RegisterVM register, EMail emailData)
        {
            try
            {
                //var degree = await academicDegrees.FindAsync(register.AcademicDegreeId);
                var degree = "";
            var keyValuePairs = Services.ServiceTools.NewRegisterKeyValuePairs(degree, register);
            var msg = Services.ServiceTools.ReplacementPlaceholder(emailData.Msg, keyValuePairs);
            var message = new Message(register.Email, new string[] { register.Email }, emailData.Title, msg, null);

            await _emailSender.SendEmailAsync(message);
            }
            catch(Exception ex)
            {

            }

        }
        [Authorize(Roles = "superAdmin,admin")]
         [HttpGet]
        public IActionResult AddUser()
            {
            UserVM user = new UserVM()
            {
                Roles = AvailableRoles()
            };
                return View(user);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]

    [Authorize(Roles = "superAdmin,admin")]
        public async Task<IActionResult> AddUser(UserVM model)
            {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                { Email = model.Email, UserName = model.Email, Mobile = model.Mobile,Name=model.Name, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, model.SelectedRole);

                    ViewBag.Created = "Success";
                }
                AddErrors(result);
            }
                return View(model);
            }
          private List<RoleVM> AvailableRoles()
        {
            var list =  _roleManager.Roles.Select(x => new RoleVM
            {
               ID=x.Name,
                Name =ServiceTools.RoleNameAr(x.Name)
            }).Where(x=> x.ID!="superAdmin" && x.ID!="author" && x.ID!="auditor").ToList();
            return list;
        }

            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> ConfirmEmail(string token, string email)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return View("Error");

                var result = await _userManager.ConfirmEmailAsync(user, token);
                return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
            }

            [HttpGet]
            [AllowAnonymous]
            public IActionResult SuccessRegistration()
            {
                return View();
            }
            [HttpGet]
            [AllowAnonymous]
            public IActionResult AddFirstUser(string returnUrl = null)
            {
                var UsersTableEmpty = _userManager.Users;
                if (UsersTableEmpty.Any()) return RedirectToAction("Login");
                ViewData["ReturnUrl"] = returnUrl;
                AddRoles();
                return View();
            }
            private UserVM ListOfRoles()
            {
                UserVM model = new UserVM();
                var roles = _roleManager.Roles.Where(x => x.Name != "superAdmin")
                      .OrderByDescending(x => x.Name)
                      .Select(x => new RoleVM { ID = x.Name, Name = x.Name }).ToList();
                model.Roles = roles;
                return model;
            }
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddFirstUser(UserVM model, string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                //if (ModelState.IsValid)
                //{

                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name, Mobile = model.Mobile, EmailConfirmed = true };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "superAdmin");
                        _logger.LogInformation("New user added");
                        return RedirectToAction("Login", "Account");
                    }
                    AddErrors(result);
                //}
                //If we got this far, something failed. Redisplay form.
                return View(model);
            }

            [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            [AllowAnonymous]
            public async Task<IActionResult> Login(string returnUrl = "")
            {
                SetArabicLanguage();

                LoginVM model = new LoginVM
                {
                    ReturnUrl = returnUrl,
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                };
                ViewData["ReturnUrl"] = returnUrl;
                if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Account");
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [AllowAnonymous]
            public async Task<IActionResult> Login(LoginVM model, string returnUrl)
            {

                if (ModelState.IsValid)
                {
                    var user = _userManager
                        .Users.SingleOrDefault(x => x.UserName == model.Email || x.Mobile == model.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password
                                                                                           , model.RememberMe, false);
                        if (result.Succeeded)
                            if (string.IsNullOrEmpty(returnUrl))
                                return RedirectToAction("UserProfile", "Account");
                            else
                                RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError("", Localizer["Invalid Username or Password"]);

                }
                else
                    ModelState.AddModelError("", Localizer["Try Again"]);

                return View(model);
            }
            private void AddErrors(IdentityResult result)
            {
                foreach (var error in result.Errors)
                {
                string err = Localizer["Try Again"];
                    ModelState.AddModelError(string.Empty, Localizer[error.Description]);
                }
            }

            private IActionResult RedirectToLocal(string returnUrl)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Account");
                }
            }

            public IActionResult AccessDenied(string returnUrl)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();

            }

            [HttpGet]
            public IActionResult ChangePassword()
            {
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return RedirectToAction("Login");
                    }
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                        return View();
                    }
                    await _signInManager.RefreshSignInAsync(user);
                    return View("ChangePasswordConfirmation");
                }
                return View(model);
            }

            public IActionResult ChangePasswordConfirmation()
            {
                return View();
            }
            [AllowAnonymous]
            [HttpGet]
            public IActionResult ForgotPassword()
            {
                return View();
            }

            //[Authorize(Roles = "superAdmin")]
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordModel)
            {
                if (!ModelState.IsValid)
                    return View(forgotPasswordModel);
                try
                {
                    var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                    if (user == null)
                        return RedirectToAction(nameof(ForgotPasswordConfirmation));

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(user.Email,new string[] { user.Email }, "Reset password token", callback, null);

                await _emailSender.SendEmailAsync(message);
                }
                catch (Exception)
                {
                    ViewBag.Error = "Error";
                    return View(forgotPasswordModel);
                }


                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

        [Authorize(Roles = "superAdmin,admin")]
        [HttpGet]
            public IActionResult ForgotPasswordAdmin()
            {
                return View();
            }

            //[Authorize(Roles = "superAdmin")]
            [HttpPost]
        [Authorize(Roles = "superAdmin,admin")]

        [ValidateAntiForgeryToken]
            public async Task<IActionResult> ForgotPasswordAdmin(ForgotPasswordAdminVM forgotPasswordModel)
            {
                if (!ModelState.IsValid)
                    return View(forgotPasswordModel);
                try
                {
                    var user = _userManager
                       .Users.SingleOrDefault(x => x.UserName == forgotPasswordModel.Email || x.Mobile == forgotPasswordModel.Email);
                    if (user == null)
                        return RedirectToAction(nameof(ForgotPasswordConfirmation));

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    ViewBag.token = token;
                    ViewBag.email = user.Email;


                    var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

                    ViewBag.Message = callback;

                }
                catch (Exception)
                {
                    ViewBag.Error = "Error";
                    return View(forgotPasswordModel);
                }


                return View();

            }

            [AllowAnonymous]
            public IActionResult ForgotPasswordConfirmation()
            {
                return View();
            }

            [HttpGet]
            [AllowAnonymous]

            public IActionResult ResetPassword(string token, string email)
            {
                var model = new ResetPasswordVM { Token = token, Email = email };
                return View(model);
            }

            [HttpPost]
            [AllowAnonymous]

            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordModel)
            {
                if (!ModelState.IsValid)
                    return View(resetPasswordModel);

                var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
                if (user == null)
                    RedirectToAction(nameof(ResetPasswordConfirmation));

                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        //ModelState.TryAddModelError(error.Code, error.Description);
                    ModelState.TryAddModelError(Localizer[error.Code], Localizer[error.Description]);

                }

                return View();
                }

                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            [HttpGet]
            [AllowAnonymous]
            public IActionResult ResetPasswordConfirmation()
            {
                return View();
            }

            private List<DateVM> AvailableDay()
            {
                var x = new List<DateVM>();
                for (int i = 1; i <= 31; i++)
                {
                    x.Add(new DateVM { Id = i });
                }
                return x;
            }

            private List<DateVM> AvailableMonth()
            {
                var x = new List<DateVM>();
                for (int i = 1; i <= 12; i++)
                {
                    x.Add(new DateVM { Id = i });
                }
                return x;
            }

            private List<DateVM> AvailableYear()
            {
                var x = new List<DateVM>();
                int currentYear = MyTimeZone.Now.Year;
                for (int i = currentYear - 25; i <= currentYear - 5; i++)
                {
                    x.Add(new DateVM { Id = i });
                }
                return x;
            }

            [AllowAnonymous]
            public IActionResult IsUserExsist(string Email = "xx")
            {
                bool IsUserAlreadyRegistered = _userManager.Users.Any(item => item.UserName == Email);
                if (IsUserAlreadyRegistered)
                    return Json($"البريد الالكتروني {Email} مستخدم ادخل ايميل اخر");
                return Json(data: true);
            }

           
            [AllowAnonymous]
            public IActionResult IsMobileExsist(string Mobile = "xx")
            {
                bool IsPhoneAlreadyRegistered = _userManager.Users.Any(item => item.Mobile == Mobile);
                if (IsPhoneAlreadyRegistered)
                    return Json($"رقم الجوال {Mobile} مستخدم من قبل");
                return Json(data: true);
            }


            [HttpGet]
            public IActionResult UpdateEmail()
            {
                var userEmail = User.Identity.Name;
                var model = new EmailVM { UserName = userEmail };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> UpdateEmail(EmailVM userVM)
            {
                var userName = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    var UserExsistBefore = await _userManager.FindByEmailAsync(userVM.UserName);
                    if (UserExsistBefore != null && userName != userVM.UserName)
                    {
                        ModelState.AddModelError("", Localizer["Email already registerd for another user"]);
                    }
                    else
                    {
                        var user = await _userManager.FindByEmailAsync(userName);
                        user.UserName = userVM.UserName;
                        user.Email = userVM.UserName;
                        var result = await _userManager.UpdateAsync(user);
                        if (!result.Succeeded)
                        {
                            foreach (var err in result.Errors)
                                ModelState.AddModelError("", err.Description);
                        }
                        else
                        {
                         
                            await _signInManager.SignOutAsync();
                            return RedirectToAction("Login", "Account");

                        }
                    }
                }
                return View(userVM);
            }

            public async Task<IActionResult> UpdateMobile()
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);

                var model = new MobileVM { Mobile = user.Mobile };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> UpdateMobile(MobileVM userVM)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (ModelState.IsValid)
                {
                    var UserExsistBefore = _userManager.Users.FirstOrDefault(x => x.Mobile == userVM.Mobile);
                    if (UserExsistBefore != null && user.Mobile != userVM.Mobile)
                    {
                        ModelState.AddModelError("", Localizer["Mobile already registerd for another user"]);
                    }
                    else
                    {
                        user.Mobile = userVM.Mobile;
                        var result = await _userManager.UpdateAsync(user);
                        if (!result.Succeeded)
                        {
                            foreach (var err in result.Errors)
                                ModelState.AddModelError("", err.Description);
                        }
                        else
                        {
                            
                            ViewBag.Updated = Localizer["Update Successful"];

                        }
                    }
                }
                return View(userVM);
            }


            //
            public async Task<IActionResult> UpdatePersonalData()
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);

                var model = new PersonalDataVM { Name = user.Name, Mobile = user.Mobile, UserName = user.UserName };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> UpdatePersonalData(PersonalDataVM userVM)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (ModelState.IsValid)
                {

                    user.Name = userVM.Name;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var err in result.Errors)
                            ModelState.AddModelError("", err.Description);
                    }
                    else
                    {
                        ViewBag.Updated = Localizer["Update Successful"];

                    }
                }
                return View(userVM);
            }

            //
            [HttpPost]
            [AllowAnonymous]
            public IActionResult ExternalLogin(string provider, string returnUrl)
            {
                var redirectUrl = Url.Action("ExternalLoginCallBack", "Account",
                    new { ReturnUrl = returnUrl });
                var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                return new ChallengeResult(provider, properties);
            }

            [AllowAnonymous]
            public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string remotError = null)
            {
                returnUrl = returnUrl ?? Url.Content("~/");
                LoginVM loginViewModel = new LoginVM
                {
                    ReturnUrl = returnUrl,
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                };
                if (remotError != null)
                {
                    ModelState.AddModelError("", $"Error from Eternal Provider:{remotError}");
                    return View("Login", loginViewModel);

                }
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    ModelState.AddModelError("", $"Error loading Eternal Information");
                    return View("Login", loginViewModel);

                }
                var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                          info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (signInResult.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                    if (email != null)
                    {
                        var user = await _userManager.FindByEmailAsync(email);
                        if (user == null)
                        {
                            user = new ApplicationUser
                            {
                                UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                                Email = info.Principal.FindFirstValue(ClaimTypes.Email)

                            };
                            await _userManager.CreateAsync(user);
                        }
                        await _userManager.AddLoginAsync(user, info);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);

                    }
                    else
                    {
                        ViewBag.ErrorTitle = $"Email can not received from:{info.LoginProvider}";
                        ViewBag.ErrorMessage = "Please contact support";
                        return View("Error");
                    }
                }
            }
            private void SetArabicLanguage()
            {

                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("ar-EG")),
                    new CookieOptions { Expires = MyTimeZone.Now.AddDays(100) });
            }

            [AllowAnonymous]
            public IActionResult TestPage()
            {
               
                return View();
            }

            [AllowAnonymous]
            public IActionResult TestPage2()
            {

                return View();
            }

            [AllowAnonymous]
            public IActionResult ForgotPasswordTrainee()
            {
                if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Account");

                return View();
        }

     

    
      
        
       
        private async Task<string> GetUserId()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return user.Id;
            //return "f97fa099-5eaa-4868-9b99-7ab9d9b33b4f";
        }
        private async Task<ApplicationUser> GetUser()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return user;
        }

       
    }
}
