using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models.DeveloperModels
{
    public class Skill
    {
        public int SkillID { get; set; }

        [Display(Name = "Skill")]
        [Required(ErrorMessage = "You must enter a skill name!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Skill name must be between 2 and 100 characters!")]
        public string SkillName { get; set; }
    }
}