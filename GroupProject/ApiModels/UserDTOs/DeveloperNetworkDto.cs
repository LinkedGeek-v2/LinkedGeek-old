namespace GroupProject.ApiModels.UserDTOs
{
    public class DeveloperNetworkDto
    {
        public string DeveloperID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public CompanyNetworkDto CompanyWorking { get; set; }
    }
}