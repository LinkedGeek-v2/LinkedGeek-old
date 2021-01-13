using GroupProject.DAL;
using GroupProject.Models.DeveloperModels;
using System.Data.Entity;

namespace GroupProject.Repositories
{
    public class EducationRepository
    {
        private readonly ApplicationDbContext _db;

        public EducationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddOrEdit(Education education)
        {
            _db.Entry(education).State = education.EducationID== 0 ? EntityState.Added : EntityState.Modified;

        }

        public void Delete(Education education)
        {
            _db.Entry(education).State = EntityState.Deleted;
        }
    }
}