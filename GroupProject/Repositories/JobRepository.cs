using AutoMapper;
using GroupProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using GroupProject.Models.CompanyModels;
using GroupProject.ViewModels.CompanyViewModels;
using GroupProject.ApiModels.DeveloperDTOs;

namespace GroupProject.Repositories
{
    public class JobRepository
    {
        private readonly ApplicationDbContext db;

        public JobRepository(ApplicationDbContext db)
        {
            this.db = db;
        }


        public List<Job> GetCompJobs(string companyID) => db.Jobs.Where(j => j.CompanyID == companyID).ToList();

        public List<JobGetDto> GetDevJobs(JobPostDto search, string userId)
        {
            //Set the query of all the jobs that the developer has not applied to
            var jobsDb = db.Jobs.Include(j => j.JobsApplied).Where(j => !j.JobsApplied.Any(ja => ja.JobID == j.JobID && ja.DeveloperID == userId)).Include(j => j.Company);

            //Get a list of jobs filtered by title and location
            if (!String.IsNullOrEmpty(search.JobTitle) && !String.IsNullOrEmpty(search.CityName))
            {
                return jobsDb.Where(j => j.JobTitle.ToLower().Contains(search.JobTitle.ToLower()) && j.Company.User.Address.City.CityName == search.CityName)
                    .Select(Mapper.Map<Job, JobGetDto>)
                    .ToList();
            }

            //Get a list of jobs filtered by title
            if (!String.IsNullOrEmpty(search.JobTitle)) return jobsDb.Where(j => j.JobTitle.ToLower().Contains(search.JobTitle.ToLower())).Select(Mapper.Map<Job, JobGetDto>).ToList();

            //Get a list of jobs filtered by location
            if (!String.IsNullOrEmpty(search.CityName)) return jobsDb.Where(j => j.Company.User.Address.City.CityName == search.CityName).Select(Mapper.Map<Job, JobGetDto>).ToList();

            return new List<JobGetDto>();
        }

        public void AddJobPosted(JobPostViewModel viewModel) => db.Jobs.Add(new Job(viewModel.JobTitle, viewModel.JobDescription, viewModel.JobType, viewModel.CompanyID));

        public void DeleteJob(int id) => db.Entry(new Job(id)).State = EntityState.Deleted;
    }
}