using GroupProject.ApiModels.PostsDTOs;

namespace GroupProject.ViewModels
{
    public class PostViewModel : UserViewModel
    {
        public bool IsDeveloper { get; set; }
        public OutgoingPostDto Post { get; set; }
    }
}