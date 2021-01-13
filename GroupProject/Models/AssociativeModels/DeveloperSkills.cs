using GroupProject.Models.DeveloperModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.AssociativeModels
{
    public class DeveloperSkills
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Developer")]
        public string DeveloperID { get; private set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Skill")]
        public int SkillID { get; private set; }

        public Developer Developer { get; set; }
        public Skill Skill { get; set; }

        private DeveloperSkills() { }

        public DeveloperSkills(int skillId, string UserID)
        {
            SkillID = skillId;
            DeveloperID = UserID;
        }


        /// <summary>
        /// Creates a DeveloperSkills object to mark its State as added or deleted.
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DeveloperSkills Create(int skillId, string userID) => new DeveloperSkills(skillId, userID);
    }
}