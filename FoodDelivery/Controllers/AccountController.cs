using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Models;

namespace FoodDelivery.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManger;
        private IEfRepository _efRepository;
        private IListOfAllData _listOfAll;
        private IHostingEnvironment Environment;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IEfRepository efRepository, IListOfAllData listOfAll, IHostingEnvironment _environment)
        {
            _userManger = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _listOfAll = listOfAll;
            _efRepository = efRepository;
            Environment = _environment;
        }

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HandleSignUp()
        {
            return View();
        }

        public IActionResult UnAuthorize()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {

            bool Status = false;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                // Checking Email Is Confirmed 
                var user = _userManger.FindByNameAsync(model.UserName).Result;
                if (user != null)
                {
                    // Login 
                    var result = await _signInManager.PasswordSignInAsync(
                        userName: model.UserName,
                        password: model.Password,
                        isPersistent: model.RememberMe,
                        lockoutOnFailure: false);
                   
                    if (result.Succeeded)
                    {
                        //HttpContext.Session.SetString("UserID",user.Id);
                      
                        Status = true;
                        return Json(new { status = Status, message = Message });
                    }


                    //if (!_userManger.IsEmailConfirmedAsync(user).Result)
                    //{
                    //    ModelState.AddModelError("", "Email Not Confirmed!");
                    //    return View(model);
                    //}
                    //else
                    //{
                    //    // Login 
                    //    var result = await _signInManager.PasswordSignInAsync(
                    //        userName: model.UserName,
                    //        password: model.Password,
                    //        isPersistent: model.RememberMe,
                    //        lockoutOnFailure: false);

                    //    if (result.Succeeded)
                    //    {
                    //        return RedirectToAction("Index", new { Areas = "", Controller = "Home" });
                    //    }
                    //}
                }
            }
            else
            {
                Status = false;
                // Adding Model Error
                Message = "Username / Password is invalid. Try again!";
            }
            return Json(new { status = Status, message = Message });
        }
        #endregion

        #region Sign Up
        [HttpGet]
        public IActionResult SignUp(string UserType)
        {
            if (UserType == "Vendor")
            {
                SignUpVendorVM model = new SignUpVendorVM();
                model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                {
                    Text = p.AreaName,
                    Value = p.Id.ToString()
                }).ToList();
                model.Category = _listOfAll.GetCategory()?.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();
                model.Cuisine = _listOfAll.GetCuisine()?.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();

                model.NunberOfLocation = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "1 - 4", Text = "1 - 4" },
                    new SelectListItem() { Value = "4 - 10", Text = "4 - 10" },
                    new SelectListItem() { Value = "10 - 20", Text = "10 - 20" }
                };


                return PartialView("_VendorSignUp", model);
            }
            else if (UserType == "User")
            {
                return PartialView("_User_SigUp");
            }
            else
            {
                SignUpDriverVM model = new SignUpDriverVM();
                model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                {
                    Text = p.AreaName,
                    Value = p.Id.ToString()
                }).ToList();
                return PartialView("_Driver_SigUp", model);
            }

        }

        #region VendorSignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpVendor(SignUpVendorVM model)
        {
            bool Status = false;
            string Message = string.Empty;
           
            if (ModelState.IsValid)
            {
             
               

                // User Name Already Exsit
                var userName = _userManger.FindByNameAsync(model.Email).Result;
                if (userName != null)
                {
                    Message = "UserName Already Exsit.Please Enter Another UserName";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                //User Email Already Exsit
                var userEmail = _userManger.FindByEmailAsync(model.Email).Result;
                if (userEmail != null)
                {
                    Message = "This Email is Already Exsit.Please Enter Another Email";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                string contentPath = this.Environment.ContentRootPath;
                string path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string UniqueFileName = "";
                string fileName = Path.GetFileName(model.SubmitterPicture.FileName);
                UniqueFileName = Guid.NewGuid() + "_" + fileName;
                // User
                var user = new AppUser();
                user.FirstName = model.FirstName;
                user.Created = DateTime.Now;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;
               


                // Add User In Database 
                var result = _userManger.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    using (FileStream stream = new FileStream(Path.Combine(path, UniqueFileName), FileMode.Create))
                    {
                        UniqueFileName = Guid.NewGuid() + "_" + fileName;
                        model.SubmitterPicture.CopyTo(stream);
                        ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                    }
                    //Adding Vendor Information 
                    Vendor vendori = new Vendor();
                    vendori.CategoryId = model.CategoryId;
                    vendori.CuisineId = model.CuisineId;
                    vendori.NumberOfLocation = model.NunberOfLocationName;
                    vendori.StoreName = model.StoreName;
                    vendori.Website_Url = model.Website_Url;
                    vendori.UserId = user.Id;
                    vendori.Address_Location = model.Address;
                    vendori.AreaId = model.AreaId;
                    vendori.UniqueFileName = UniqueFileName;
                    _efRepository.Add(vendori);
                    _efRepository.SaveChanges();

                    //Add Role
                    AddRole("Vendor", user);

                    //Send Email Confirmation
                    //SendEmailConformationLink(user);

                    Status = true;
                    Message = "Successfully Created Your Account";

                    // Redirect To SignInPage
                    return Json(new { status = Status, message = Message });

                }
                else
                {
                    Status = false;
                    Message = "Error While Creating Your Account";
                    return Json(new { status = Status, message = Message });
                }
            }

            Message = "Provide all required and valid data to proceed";
            return Json(new { status = Status, message = Message });
        }
        #endregion
        public void SendEmailConformationLink(AppUser user)
        {

            // Send Email Conformation 
            //Token email
            string emailConfrmToken = _userManger
                                      .GenerateEmailConfirmationTokenAsync(user)
                                      .Result;

            // Confomation Link
            string conformtaionLink = Url.Action("ConfirmEmail"
                , "Account", new { UserId = user.Id, token = emailConfrmToken },
                protocol: HttpContext.Request.Scheme);

            // SmtpClient Create
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            NetworkCredential obj = new NetworkCredential("nadeem.s.2582@gmail.com", "Nadeem1@");
            client.UseDefaultCredentials = true;
            client.Credentials = obj;
            client.Send("Test@localHost", user.Email, "Confirm Your Email", conformtaionLink);
        }
        public void AddRole(string RoleName, AppUser user)
        {
            string Role = string.Empty;
            // Adding Role If Not Exist
            if (!_roleManager.RoleExistsAsync(RoleName).Result)
            {
                // Add Admin Role If Not Exist
                var adminRole = new AppRole();
                adminRole.Name = RoleName;
                adminRole.Discription = "Perform All Opration";
                // Add Role In Database 
                var addRole = _roleManager.CreateAsync(adminRole).Result;
                if (!addRole.Succeeded)
                {
                    Role = RoleName;
                }
            }
            else
            {
                // Add to Role While Creting User
                var role = _userManger.AddToRoleAsync(user, RoleName).Result;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpUser(SignUpUserVM model)
        {
            bool Status = false;
            string Message = string.Empty;

            if (ModelState.IsValid)
            {
                // User Name Already Exsit
                var userName = _userManger.FindByNameAsync(model.Email).Result;
                if (userName != null)
                {
                    Message = "UserName Already Exsit.Please Enter Another UserName";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                //User Email Already Exsit
                var userEmail = _userManger.FindByEmailAsync(model.Email).Result;
                if (userEmail != null)
                {
                    Message = "This Email is Already Exsit.Please Enter Another Email";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                // User
                var user = new AppUser();
                user.FirstName = model.FirstName;
                user.Created = DateTime.Now;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                // Add User In Database 
                var result = _userManger.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    //Add Role
                    AddRole("User", user);

                    //Send Email Confirmation
                    //SendEmailConformationLink(user);

                    Status = true;
                    Message = "Successfully Created Your Account";

                    // Redirect To SignInPage
                    return Json(new { status = Status, message = Message });
                }
                else
                {
                    Status = false;
                    Message = "Error While Creating Your Account";
                    return Json(new { status = Status, message = Message });
                }
            }

            Message = "Provide all required and valid data to proceed";
            return Json(new { status = Status, message = Message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpDriver(SignUpDriverVM model)
        {
            bool Status = false;
            string Message = string.Empty;

            if (ModelState.IsValid)
            {
                // User Name Already Exsit
                var userName = _userManger.FindByNameAsync(model.Email).Result;
                if (userName != null)
                {
                    Message = "UserName Already Exsit.Please Enter Another UserName";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                //User Email Already Exsit
                var userEmail = _userManger.FindByEmailAsync(model.Email).Result;
                if (userEmail != null)
                {
                    Message = "This Email is Already Exsit.Please Enter Another Email";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                // User
                var user = new AppUser();
                user.FirstName = model.FirstName;
                user.Created = DateTime.Now;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;



                // Add User In Database 
                var result = _userManger.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {

                    Driver driver = new Driver();
                    driver.Address_Location = model.Address;
                    driver.AreaId = model.AreaId;
                    driver.UserId = user.Id;
                    _efRepository.Add(driver);
                    _efRepository.SaveChanges();

                    //Add Role
                    AddRole("Driver", user);

                    //Send Email Confirmation
                    //SendEmailConformationLink(user);

                    Status = true;
                    Message = "Successfully Created Your Account";

                    // Redirect To SignInPage
                    return Json(new { status = Status, message = Message });
                }
                else
                {
                    Status = false;
                    Message = "Error While Creating Your Account";
                    return Json(new { status = Status, message = Message });
                }
            }

            Message = "Provide all required and valid data to proceed";
            return Json(new { status = Status, message = Message });
        }

        public IActionResult CoonfirmEmailMessg()
        {
            ViewBag.message = "Email Sent Successfully Please Check Your Email";
            return View();
        }

        #endregion

        #region Sign Out
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            // TODO: Add Session Clearance code
            await _signInManager.SignOutAsync();

            return RedirectToAction("PublicSite", new { controller = "Home" });
        }
        #endregion

        #region ConfirmEmail
        public IActionResult ConfirmEmail(string userId, string token)
        {
            var user = _userManger.FindByIdAsync(userId).Result;
            var result = _userManger.ConfirmEmailAsync(user, token).Result;

            if (result.Succeeded)
            {
                ViewBag.Message = "Email Confirmed Successfully!";
                return View();
            }
            else
            {
                ViewBag.Message = "Error While Confirming your Email!";
                return View("Error");
            }
        }
        #endregion

        #region PasswordReset
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManger.
                 FindByNameAsync(model.UserName).Result;

            var result = _userManger.ResetPasswordAsync
                      (user, model.Token, model.Password).Result;
            if (result.Succeeded)
            {
                Status = true;
                Message = "Password Reset Successfully";
                return Json(new { status = Status, message = Message });
            }
            else
            {
                Status = false;
                Message = "Error While Password Reset";
                return Json(new { status = Status, message = Message });
            }

        }


        public IActionResult View1Message()
        {
            ViewBag.message = "Password reset successful!";
            return View();
        }

        #endregion

        #region Password Reset Link
        [HttpGet]
        public IActionResult ResetPasswordLink()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPasswordLink(ResetlPasswordViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;

            if (ModelState.IsValid)
            {
                var user = _userManger.FindByNameAsync(model.UserName).Result;
                if (user == null || !(_userManger.IsEmailConfirmedAsync(user).Result))
                {
                    ModelState.AddModelError("", "User Name And Email Are Not Correct");
                    return View(model);
                }

                // Genrate password Reset Token 
                var token = _userManger.
                      GeneratePasswordResetTokenAsync(user).Result;

                // Genrate Reset Link for Password 
                var resetLink = Url.Action("ResetPassword",
                                "Account", new { Token = token },
                                 protocol: HttpContext.Request.Scheme);


                // SmtpClient Create
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = @"C:\Test";
                client.Send("Test@localHost", user.Email, "Confirm Your Email", resetLink);
                Status = true;
                Message = "Please Check Your Email To Reset Your Password";
                return Json(new { status = Status, message = Message });
            }
            else
            {

                ModelState.AddModelError("", "Please Add All Required Fields");
                return View(model);
            }


        }


        public IActionResult ViewMessage()
        {
            ViewBag.message = "Email Sent Successfully Please Check Your Email";
            return View();
        }

        #endregion

    }
}