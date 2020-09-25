using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDelivery.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManger;
        private IListOfAllData _listOfAll;
        private IEfRepository _efRepository;
        private IHostingEnvironment Environment;
        public RestaurantController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IListOfAllData listOfAll, IEfRepository efRepository, IHostingEnvironment _environment)
        {
            _userManger = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _listOfAll = listOfAll;
            _efRepository = efRepository;
            Environment = _environment;
        }
        public IActionResult List()
        {
            var data = _userManger.Users.ToList();
            List<SignUpVendorVM> model = new List<SignUpVendorVM>();
            if (data != null)
            {

                foreach (var item in data)
                {
                    if (_userManger.IsInRoleAsync(item, "Vendor").Result)
                    {

                        var VendorData = _listOfAll.GetVendorById(item.Id);
                        SignUpVendorVM listVM = new SignUpVendorVM();
                        listVM.Id = item.Id;
                        listVM.FirstName = item.FirstName;
                        listVM.LastName = item.LastName;
                        listVM.Email = item.Email;
                        listVM.PhoneNumber = item.PhoneNumber;
                        listVM.StoreName = VendorData.StoreName;
                        listVM.Address = VendorData.Address_Location;
                        model.Add(listVM);
                    }
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Addlocation(int VendorID)
        {
            var otherLocaction = new OtherLocationVM();
            var getOtherLocationByVendorID = _listOfAll.GetOtherLocationByVendorID(VendorID).ToList();
            otherLocaction.VendorID = VendorID;
            List<OtherLocationList> filllist = new List<OtherLocationList>();
            if (getOtherLocationByVendorID.Count==0)
            {

                filllist.Add(new OtherLocationList { LocationAddress = "", LocationName = " " });
            }
         
            foreach (var item in getOtherLocationByVendorID)
            {
                filllist.Add(new OtherLocationList()
                {
                    LocationAddress=item.LocationAddress,
                    LocationName = item.LocationName
                });

            }
            otherLocaction.Lista = filllist;
            return View("ListofLocation",otherLocaction);
        }
        [HttpPost]
        public IActionResult Addlocation(OtherLocationVM Model)
        {
            OtherLocation obj = new OtherLocation();
           
         
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var Vendori = _listOfAll.GetVendorById(userId);
            obj.VendorID = Model.VendorID;
            obj.LocationAddress = Model.LocationAddress;
            obj.LocationName = Model.LocationName;
            _efRepository.Add(obj);
            _efRepository.SaveChanges();
            return RedirectToAction("Addlocation",new { VendorID=Model.VendorID });
        }
        public IActionResult DeleteVendor(int VendorID)
        {
            var vendor = _listOfAll.GetVendor().Where(x => x.Id == VendorID).FirstOrDefault(); ;
            _efRepository.Delete(vendor);
            _efRepository.SaveChanges();
            return RedirectToAction("Index", "ManageAllUser");
        }
        [HttpGet]
        public IActionResult AddVendor()
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


            return View(model);
        }
            public IActionResult AddVendor(SignUpVendorVM model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string UniqueFileName = "";
            string fileName = Path.GetFileName(model.SubmitterPicture.FileName);
            UniqueFileName = Guid.NewGuid() + "_" + fileName;
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
            vendori.UserId = userId;
            vendori.Address_Location = model.Address;
            vendori.AreaId = model.AreaId;
            vendori.UniqueFileName = UniqueFileName;
            _efRepository.Add(vendori);
            _efRepository.SaveChanges();
            return RedirectToAction("Index", "ManageAllUser");
        }
    }
}