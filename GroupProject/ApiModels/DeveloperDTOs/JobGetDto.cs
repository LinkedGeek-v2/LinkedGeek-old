using System;
using GroupProject.ApiModels.CompanyDTOs;
using GroupProject.Enums;

namespace GroupProject.ApiModels.DeveloperDTOs
{
    public class JobGetDto
    {
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public DateTime DatePosted { get; set; }
        public string JobDescription { get; set; }
        public WorkingType JobType { get; set; }
        public CompanyDto Company { get; set; }
    }
}