using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.ViewComponents
{
    public class AreaListViewComponent : ViewComponent
    {
        private IListOfAllData _listOfAll;
        public AreaListViewComponent(IListOfAllData listOfAll)
        {
            _listOfAll = listOfAll;
        }

        public async Task<IViewComponentResult> InvokeAsync(int subjectId)
        {
            var items = await GetItemsAsync(subjectId);
            return View(items);
        }

        private Task<List<AreaListVM>> GetItemsAsync(int subjectId)
        {
            var data = _listOfAll.GetArea().ToList();
            List<AreaListVM> model = new List<AreaListVM>();
            if (data!=null)
            {
                foreach (var item in data)
                {
                    AreaListVM listVM = new AreaListVM();
                    listVM.Id = item.Id;
                    listVM.Name = item.AreaName;
                    model.Add(listVM);
                }
            }
            return Task.FromResult(model);
        }

    }

}
