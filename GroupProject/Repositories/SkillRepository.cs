using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using System.Collections.Generic;
using System.Linq;

namespace GroupProject.Repositories
{
    public class SkillRepository
    {
        private readonly ApplicationDbContext _db;

        public SkillRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public List<Skill> GetSkills()
        {
            return _db.Skills.ToList();
        }
    }
}