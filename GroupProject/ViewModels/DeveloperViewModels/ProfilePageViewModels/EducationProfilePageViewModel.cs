using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.ViewModels.DeveloperViewModels.ProfilePageViewModels
{
    public class EducationProfilePageViewModel
    {
        public int EducationID { get; set; }

        [Display(Name = "School: ")]
        [Required(ErrorMessage = ("You must enter the school name!"))]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ("School must be between 5 and 100 characters!"))]
        public string School { get; set; }

        [Display(Name = "Degree: ")]
        [Required(ErrorMessage = "You must enter the title of Degree!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Degree must be between 2 and 100 characters!"))]
        public string Degree { get; set; }

        [Display(Name = "Field: ")]
        [Required(ErrorMessage = "You must enter the field of studies!", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Field name must be between 2 and 100 characters!"))]
        public string Field { get; set; }

        [Display(Name = "Grade: ")]
        [Range(5.0, 10.0, ErrorMessage = ("Grade must be between 5.00 and 10.00!"))]
        public double? Grade { get; set; }

        [Display(Name = "Started In: ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        
        public DateTime StartYear { get; set; }

        [Display(Name = "Until: ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}", NullDisplayText = "Present")]
        public DateTime? EndYear { get; set; }
    }
}