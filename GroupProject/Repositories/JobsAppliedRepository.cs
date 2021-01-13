using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using GroupProject.DAL;
using GroupProject.Models.AssociativeModels;
using GroupProject.Models.DeveloperModels;
using GroupProject.ViewModels.CompanyViewModels;

namespace GroupProject.Repositories
{
    public class JobsAppliedRepository
    {
        private readonly ApplicationDbContext db;

        public JobsAppliedRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddJobApplied(string userId, int id)
        {
            db.JobsApplied.Add(new JobsApplied(userId, id));
        }

        public List<CompanyApplicantsViewModel> GetApplicants(string userId)
        {
            return db.JobsApplied
                .Include(j => j.Job)
                .Where(j => j.Job.CompanyID == userId)
                .Select(ja => ja.Developer)
                .Select(Mapper.Map<Developer, CompanyApplicantsViewModel>)
                .ToList();
        }
    }
}