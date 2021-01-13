using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.FixedModels
{
    public class City
    {
        public int CityID { get; set; }

        [StringLength(255)]
        public string CityName { get; set; }

        [StringLength(20)]
        public string Latitude { get; set; }

        [StringLength(20)]
        public string Longtitude { get; set; }

        [ForeignKey("Country")]
        public string CountryIsoID { get; set; }
        public Country Country { get; set; }
    }
}