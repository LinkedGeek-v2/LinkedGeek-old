using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroupProject.Models.CompanyModels;
using GroupProject.Models.DeveloperModels;

namespace GroupProject.Models.AssociativeModels
{
    public class JobsApplied
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Developer")]
        public string DeveloperID { get; private set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Job")]
        public int JobID { get; private set; }

        public Developer Developer { get; set; }
        public Job Job { get; set; }

        private JobsApplied()
        { }

        public JobsApplied(string userId, int jobId)
        {
            DeveloperID = userId;
            JobID = jobId;
        }
    }
}