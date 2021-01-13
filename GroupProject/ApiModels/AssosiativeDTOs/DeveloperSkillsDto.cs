using GroupProject.ApiModels.FixedDTOs;

namespace GroupProject.ApiModels.AssosiativeDTOs
{
    public class DeveloperSkillsDto
    {
        public int SkillID { get; set; }
          
        public SkillDto Skill { get; set; }
    }
}