using GroupProject.CustomValidations;
using GroupProject.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ApiModels.Incoming.ProfilePage
{
    public class EducationPostDto
    {
        public int EducationID { get; set; }

        [Required(ErrorMessage = ("You must enter the school name!"))]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ("School must be between 5 and 100 characters!"))]
        public string School { get; set; }

        [Required(ErrorMessage = "You must enter the title of Degree!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Degree must be between 2 and 100 characters!"))]
        public string Degree { get; set; }

        [Required(ErrorMessage = "You must enter the field of studies!", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Field name must be between 2 and 100 characters!"))]
        public string Field { get; set; }

        [Range(5.0, 10.0, ErrorMessage = ("Grade must be between 5.00 and 10.00!"))]
        public double? Grade { get; set; }

        [DataType(DataType.DateTime)]
        [DatesDefaultValue]
        public DateTime StartYear { get; set; }

        [DataType(DataType.DateTime)]
        [FutureDate("StartYear")]
        [DatesDefaultValue]
        public DateTime? EndYear { get; set; }

        public string DeveloperID { get; set; }
    }
}