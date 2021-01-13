namespace GroupProject.ApiModels.UserDTOs
{
    public class ApplicationUserNetworkDto
    {
        public DeveloperNetworkDto Developer { get; set; }
        public CompanyNetworkDto Company { get; set; }
        public string ImageBase64 { get; set; }
    }
}