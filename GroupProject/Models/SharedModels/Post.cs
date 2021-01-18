using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Models.SharedModels
{
    public class Post
    {
        public int PostID { get; set; }
        public DateTime DatePosted { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        //public string GetPostPhotoPath()
        //{
        //    if (ImageName == null)
        //        throw new NullReferenceException("Trying to retrieve a post's image that doesn't exist..!");

        //    //so the file is here: Images/UserImages/{Id}
        //    string path = "/Images/PostsImages/" + ImageName;


        //    return path;
        //}

        //public string GetPostPhotoBase64()
        //{
        //    if (ImageName == null)
        //        throw new NullReferenceException("Trying to retrieve a post's image that doesn't exist..!");
        //    // The user's profile pic is named after the user's ID

        //    //so the file is here: Images/UserImages/{Id}
        //    string path = HttpContext.Current.Server.MapPath(@"~/Images/PostsImages");
        //    string fullpath = Path.Combine(path, ImageName);
        //    byte[] imagebytes = File.ReadAllBytes(fullpath);
        //    string imagebase64 = Convert.ToBase64String(imagebytes);
        //    imagebase64 = AddHeaddersToBase64String(imagebase64);
        //    return imagebase64;
        //}

        //private string AddHeaddersToBase64String(string base64file)
        //{
        //    if (base64file == null)
        //        throw new NullReferenceException("base64file given is empty..!");
        //    if (base64file[0] == 'i')
        //        return "data:image/png;base64," + base64file;
        //    else if (base64file[0] == '/')
        //        return "data:image/jpeg;base64," + base64file;
        //    else
        //        throw new FormatException("base64file given not in the correct format! It needs to start with an 'i' or a '/'...");
        //}
    }
}