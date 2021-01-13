using GroupProject.DAL;
using GroupProject.Models.AssociativeModels;
using System.Data.Entity;
using System.Linq;

namespace GroupProject.Repositories
{
    public class DeveloperSkillsRepository
    {
        private readonly ApplicationDbContext _db;

        public DeveloperSkillsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public bool ExistInDB(string userId , int skillId)
        {
            return _db.DeveloperSkills.Any(d => d.DeveloperID == userId && d.SkillID == skillId);
        }

        public void Add(DeveloperSkills developerSkill)
        {
            _db.Entry(developerSkill).State = EntityState.Added;
        }

        public void Delete(DeveloperSkills developerSkill)
        {
            _db.Entry(developerSkill).State = EntityState.Deleted;
        }
    }
}