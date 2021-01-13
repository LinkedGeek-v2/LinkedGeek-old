using GroupProject.CustomValidations;
using System;

namespace GroupProject.ApiModels.PostsDTOs
{
    public class IncomingPostDto
    {
        [RequiredPostContent]
        public string Text { get; set; }

        [ImageType]
        public string ImageBase64 { get; set; }
        //public string VidBase64 { get; set; }

        public DateTime? DatePosted { get; set; }
    }
}