using System;
using System.Collections.Generic;
using GroupProject.Enums;
using GroupProject.Models.CompanyModels;

namespace GroupProject.ViewModels.CompanyViewModels
{
    public class JobGetViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<string> JobTypes { get; set; }

        public JobGetViewModel()
        { }

        public JobGetViewModel(List<Job> jobs)
        {
            Jobs = jobs;
            JobTypes = Enum.GetNames(typeof(WorkingType));
        }
    }
}