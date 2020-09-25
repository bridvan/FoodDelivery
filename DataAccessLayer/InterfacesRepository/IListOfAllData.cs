using Models;
using System.Collections.Generic;

namespace DataAccessLayer.InterfacesRepository
{
    public interface IListOfAllData
    {
        IEnumerable<Area> GetArea();
        Area GetAreaById(int id);
        IEnumerable<Cuisine> GetCuisine();
        Cuisine GetCuisineById(int id);
        IEnumerable<Category> GetCategory();
        Category GetCategoryById(int id);
        public IEnumerable<Vendor> GetVendor();
        public Vendor GetVendorById(string UserId);
        public IEnumerable<Driver> GetDriver();
        public Driver GetDriverById(string UserId);
        public IEnumerable<OtherLocation> GetOtherLocationByVendorID(int VendorID);

    }
}
