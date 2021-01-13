using GroupProject.Enums;
using System;

namespace GroupProject.ApiModels.DeveloperDTOs
{
    public class ExperienceDto
    {
        public int ExperienceID { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime? EndYear { get; set; }
        public WorkingType ExperienceType { get; set; }
    }
}