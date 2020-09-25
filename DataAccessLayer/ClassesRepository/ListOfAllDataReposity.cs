using DataAccessLayer.InterfacesRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.ClassesRepository
{
    public class ListOfAllDataReposity: IListOfAllData
    {
        private readonly LearningDbContext _context;

        public ListOfAllDataReposity(LearningDbContext context)
        {
            _context = context;
        }

        //Get All Subject include and Exclude Courses
        public IEnumerable<Area> GetArea()
        {
            return _context.Areas;
        }

        //Get subject include and Exclude Course on the basis of ID 
        public Area GetAreaById(int id)
        {
            return _context.Areas
                           .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Category> GetCategory()
        {
            return _context.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories
                           .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Cuisine> GetCuisine()
        {
            return _context.Cuisines;
        }

        public Cuisine GetCuisineById(int id)
        {
            return _context.Cuisines
                            .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Vendor> GetVendor()
        {
            return _context.Vendors;
        }

        public Vendor GetVendorById(string UserId)
        {
            return _context.Vendors
                            .FirstOrDefault(p => p.UserId == UserId);
        }
        public IEnumerable<Driver> GetDriver()
        {
            return _context.Drivers;
        }

        public Driver GetDriverById(string UserId)
        {
            return _context.Drivers
                            .FirstOrDefault(p => p.UserId == UserId);
        }

        public IEnumerable<OtherLocation> GetOtherLocationByVendorID(int VendorID)
        {
            return _context.OtherLocations.Where(x => x.VendorID == VendorID).ToList();
        }
    }
}
