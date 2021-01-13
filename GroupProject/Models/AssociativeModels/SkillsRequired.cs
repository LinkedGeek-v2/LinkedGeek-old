using GroupProject.Models.CompanyModels;
using GroupProject.Models.DeveloperModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.AssociativeModels
{
    public class SkillsRequired
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Skill")]
        public int SkillID { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Job")]
        public int JobID { get; set; }

        public Skill Skill { get; set; }
        public Job Job { get; set; }
    }
}