using System;

namespace GroupProject.ApiModels.CompanyDTOs
{
    public class CompanyDto
    {
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public DateTime? FoundationDate { get; set; }
        public string FounderName { get; set; }
        public string Description { get; set; }
    }
}