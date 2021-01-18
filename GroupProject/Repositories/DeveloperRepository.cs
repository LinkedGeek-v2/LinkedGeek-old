using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using System.Linq;
using System.Data.Entity;

namespace GroupProject.Repositories
{
    public class DeveloperRepository
    {
        private readonly ApplicationDbContext _db;

        public DeveloperRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public Developer GetDeveloperForProfilePageWithID(string UserID)
        {
           return _db.Developers
               .Include(d => d.User)
               .Include(d => d.User.Followers)
               .Include(d => d.User.Address.City.Country)
               .Include(d => d.Educations)
               .Include(d => d.Experiences)
               .Include(d => d.DeveloperSkills.Select(s => s.Skill))
               .Single(d => d.DeveloperID == UserID);
        }

        public void AddDeveloper(Developer developer)
        {
            _db.Developers.Add(developer);
        }
    }
}