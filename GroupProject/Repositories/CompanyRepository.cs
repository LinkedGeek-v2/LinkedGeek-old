using GroupProject.DAL;
using GroupProject.Models.CompanyModels;
using GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GroupProject.Repositories
{
    public class CompanyRepository
    {
        private readonly ApplicationDbContext db;

        public CompanyRepository(ApplicationDbContext db)
        {
            this.db = db;
        }


        public List<CompanyNamesForDeveloperProfileViewModel> GetCompaniesNamesAndIDs()
        {
            return db.Companies.Select(c => new CompanyNamesForDeveloperProfileViewModel { CompanyID = c.CompanyID, CompanyName = c.CompanyName }).ToList();
        }

        public Company VanillaCompany(string userId) => db.Companies.SingleOrDefault(c => c.CompanyID == userId);


        public Company GetCompany(string userId) => db.Companies.Include(c => c.User.Address.City.Country).SingleOrDefault(c => c.CompanyID == userId);

        public Company GetCompanyAndJobs(string userId) => db.Companies.Include(c => c.JobsPosted).SingleOrDefault(c => c.CompanyID == userId);

        public void AddCompany(Company company) => db.Companies.Add(company);
    }
}