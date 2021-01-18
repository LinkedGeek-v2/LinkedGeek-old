namespace GroupProject.ApiModels.ChatDTOs
{
    public class ChatApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ImageName { get; set; }
        public bool IsDeveloper{ get; set; }
        public ChatCompanyDto Company{ get; set; }
        public ChatDeveloperDto Developer{ get; set; }
    }
}