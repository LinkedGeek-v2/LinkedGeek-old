using GroupProject.ApiModels.AssosiativeDTOs;
using GroupProject.ApiModels.DeveloperDTOs;
using GroupProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ApiModels.DeveloperDtos
{
    public class DeveloperDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime? DateOfBirth { get; set; }

        [DisplayFormat(NullDisplayText = "--")]
        public int? Age => DateTime.Now.Year - DateOfBirth.Value.Year;

        public Gender? Gender { get; set; }

        public ApplicationUserDto User { get; set; }
        public ICollection<EducationDto> Educations { get; set; }
        public ICollection<ExperienceDto> Experiences{ get; set; }
        public ICollection<DeveloperSkillsDto> DeveloperSkills { get; set; }
    }
}