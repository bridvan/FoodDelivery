using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace FoodDelivery.Controllers
{
    public class DriverController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManger;
        private IListOfAllData _listOfAll;
        public DriverController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IListOfAllData listOfAll)
        {
            _userManger = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _listOfAll = listOfAll;
        }
        public IActionResult List()
        {
            var data = _userManger.Users.ToList();
            List<SignUpDriverVM> model = new List<SignUpDriverVM>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (_userManger.IsInRoleAsync(item, "Driver").Result)
                    {
                        var driverData = _listOfAll.GetDriverById(item.Id);
                        SignUpDriverVM listVM = new SignUpDriverVM();
                        listVM.Id = item.Id;
                        listVM.FirstName = item.FirstName;
                        listVM.LastName = item.LastName;
                        listVM.Email = item.Email;
                        listVM.PhoneNumber = item.PhoneNumber;
                        listVM.Address = driverData.Address_Location;
                        model.Add(listVM);
                    }

                }
            }
            return View(model);
        }
    }
}