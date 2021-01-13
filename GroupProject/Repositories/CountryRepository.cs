using GroupProject.DAL;
using GroupProject.Models.FixedModels;
using System.Collections.Generic;
using System.Linq;

namespace GroupProject.Repositories
{
    public class CountryRepository
    {
        private readonly ApplicationDbContext _db;

        public CountryRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public List<Country> GetCountries()
        {

            return _db.Countries.ToList();
        }
    }
}