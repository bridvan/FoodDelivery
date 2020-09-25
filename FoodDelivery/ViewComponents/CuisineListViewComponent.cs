using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.ViewComponents
{
    public class CuisineListViewComponent : ViewComponent
    {
        private IListOfAllData _listOfAll;
        public CuisineListViewComponent(IListOfAllData listOfAll)
        {
            _listOfAll = listOfAll;
        }

        public async Task<IViewComponentResult> InvokeAsync(int subjectId)
        {
            var items = await GetItemsAsync(subjectId);
            return View(items);
        }

        private Task<List<CuisineListVM>> GetItemsAsync(int subjectId)
        {
            var data = _listOfAll.GetCuisine().ToList();
            List<CuisineListVM> model = new List<CuisineListVM>();
            if (data!=null)
            {
                foreach (var item in data)
                {
                    CuisineListVM listVM = new CuisineListVM();
                    listVM.Id = item.Id;
                    listVM.Name = item.Name;
                    model.Add(listVM);
                }
            }
            return Task.FromResult(model);
        }

    }

}
