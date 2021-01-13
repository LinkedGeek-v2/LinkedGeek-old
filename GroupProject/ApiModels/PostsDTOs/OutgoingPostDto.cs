using GroupProject.CustomValidations;
using System;

namespace GroupProject.ApiModels.PostsDTOs
{
    public class OutgoingPostDto
    {    
        public DateTime DatePosted { get; set; }
        [RequiredPostContent]
        public string Text { get; set; }
        [ImageType]
        public string ImageName { get; set; }
    }
}