using GroupProject.ApiModels.FixedDTOs;

namespace GroupProject.ApiModels.SharedDTOs
{
    public class AddressDto
    {
        public string AddressID { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public CityDto City { get; set; }
    }
}