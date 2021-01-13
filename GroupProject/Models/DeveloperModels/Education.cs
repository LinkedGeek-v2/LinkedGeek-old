using AutoMapper;
using GroupProject.ApiModels.Incoming.ProfilePage;
using GroupProject.CustomValidations;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models.DeveloperModels
{
    public class Education
    {
        public int EducationID { get; private set; }

        [Required(ErrorMessage = ("You must enter the school name!"))]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ("School must be between 5 and 100 characters!"))]
        public string School { get; private set; }

        [Required(ErrorMessage = "You must enter the title of Degree!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Degree must be between 2 and 100 characters!"))]
        public string Degree { get; private set; }

        [Required(ErrorMessage = "You must enter the field of studies!", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ("Field name must be between 2 and 100 characters!"))]
        public string Field { get; private set; }

        [Range(5.0, 10.0, ErrorMessage = ("Grade must be between 5.00 and 10.00!"))]
        public double? Grade { get; private set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        public DateTime StartYear { get; private set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "--")]
        [FutureDate("StartYear")]
        public DateTime? EndYear { get; private set; }

        public string DeveloperID { get; private set; }                     //does this needs to be required for database???
        public Developer Developer { get; set; }

        private Education() { }

        /// <summary>
        /// Contructor used to create an Education and mark its state as Deleted.
        /// </summary>
        /// <param name="educationID"></param>
        public Education(int educationID)
        {
            EducationID = educationID;
        }

        //public static Education CreateOrUpdate(EducationPostDto educationPostDto) => Mapper.Map<EducationPostDto, Education>(educationPostDto);

        public static Education Create(EducationPostDto educationPostDto,string UserID)
        {
            educationPostDto.DeveloperID = UserID;

           return Mapper.Map<EducationPostDto, Education>(educationPostDto);
          
        }

        public static Education Delete(int educationID) => new Education(educationID);
    }
}