using GroupProject.Models.FixedModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.SharedModels
{
    public class Address
    {
        [Key]
        [ForeignKey("User")]
        public string AddressID { get; set; }

        public ApplicationUser User{ get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; private set; }

        [Display(Name = "Street Number")]
        public string StreetNumber { get; private set; }

        public int CityID { get; private set; }
        public City City { get; private set; }
    }
}