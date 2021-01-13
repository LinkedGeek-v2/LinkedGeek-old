using GroupProject.DAL;

namespace GroupProject.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext db;
        
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}