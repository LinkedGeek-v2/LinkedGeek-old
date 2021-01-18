using GroupProject.DAL;
using GroupProject.Models.FixedModels;
using System.Collections.Generic;
using System.Linq;

namespace GroupProject.Repositories
{
    public class CityRepository
    {
        private readonly ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public List<City> GetCitiesOfCountry(string countryIsoID)
        {           
            if(string.IsNullOrEmpty(countryIsoID))
            {
                return null;
            }

            return _db.Cities.Where(c => c.CountryIsoID == countryIsoID).ToList();
        }

        public bool IsCityAndCountryCorrect(int cityID, string countryIsoId)
        {
            return _db.Cities.Any(c => c.CityID == cityID && c.CountryIsoID == countryIsoId);
        }
    }
}