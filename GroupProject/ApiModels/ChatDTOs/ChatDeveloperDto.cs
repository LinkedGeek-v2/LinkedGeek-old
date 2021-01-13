namespace GroupProject.ApiModels.ChatDTOs
{
    public class ChatDeveloperDto
    {
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
