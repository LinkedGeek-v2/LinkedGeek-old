using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using System.Data.Entity;

namespace GroupProject.Repositories
{
    public class ExperienceRepository
    {
        private readonly ApplicationDbContext _db;

        public ExperienceRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public void AddOrEdit(Experience experience)
        {
            _db.Entry(experience).State = experience.ExperienceID== 0 ? EntityState.Added : EntityState.Modified;
        }

        public void Delete(Experience experience)
        {
            _db.Entry(experience).State = EntityState.Deleted;
        }
    }
}