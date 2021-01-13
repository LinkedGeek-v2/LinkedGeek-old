using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models.FixedModels
{
    public class Country
    {
        [Key]
        [StringLength(2)]
        public string CountryIsoID { get; set; }

        [StringLength(255)]
        public string CountryName { get; set; }
    }
}