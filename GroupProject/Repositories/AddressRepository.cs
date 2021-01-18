using GroupProject.DAL;
using GroupProject.Models.SharedModels;
using System.Data.Entity;
using System.Linq;

namespace GroupProject.Repositories
{
    public class AddressRepository
    {
        private readonly ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public void AddOrEdit(Address address)
        {
            if (_db.Addresses.Any(ad => ad.AddressID == address.AddressID))
            {
                _db.Entry(address).State = EntityState.Modified;
            }
            else
            {
                _db.Entry(address).State = EntityState.Added;
            }
        }

        public Address GetAddressWithId(string UserID)
        {
            return _db.Addresses.SingleOrDefault(adr => adr.AddressID == UserID);
        }
    }
}