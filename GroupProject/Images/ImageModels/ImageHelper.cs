using System;
using System.IO;
using System.Web;

namespace GroupProject.Images
{
    public class ImageHelper
    {
        //Public Static Members that are available for use

        public static readonly string GenericMaleUserImage = "/Images/UserProfilePics/NoImageMale.png";
        public static readonly string GenericFemaleUserImage = "/Images/UserProfilePics/NoImageFemale.jpg";
        public static readonly string GenericCompanyUserImage = "/Images/UserProfilePics/NoImageCompany.png";

       

        public static string SavePostImg(string base64Image)
            =>SavePicture(base64Image, PostImagesPath);
        public static string SaveUserImage(string base64Image) 
            => SavePicture(base64Image, UserImagesPath);
        public static string SaveUserImage(string base64Image,string imageName)
        {
            DeleteImage(imageName);
            return SavePicture(base64Image, UserImagesPath);
        }


        private static readonly string UserImagesPath = HttpContext.Current.Server.MapPath(@"~/Images/UserProfilePics/");
        private static readonly string PostImagesPath = HttpContext.Current.Server.MapPath(@"~/Images/PostsImages/");
        private static string SavePicture(string base64Image,string folderPath)
        {
            
            string ImageName = Guid.NewGuid().ToString();
            string extension = GetImageExtension(base64Image);

            // "PictureName" + ".jpg"
            ImageName += extension;

            string fullpath = Path.Combine(folderPath, ImageName);

            //Remove the header of the data-URI scheme because its not an actual valid base64string in c#
            base64Image = base64Image.Substring(base64Image.IndexOf("base64,") + 7);
            byte[] imagebytes = Convert.FromBase64String(base64Image);

            //Overwrite file or if it doesnt exist create it with byte data
            File.WriteAllBytes(fullpath, imagebytes);


            string relativePath = fullpath.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/").Substring(1);
            return relativePath;
        }



        private static void DeleteImage(string fullpath)
        {
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }

        private static string GetImageExtension(string base64Image)
        {
            string imgExtension = "";
            int charIndex=base64Image.IndexOf("base64,") + 7;
            switch (base64Image[charIndex])
            {
                case 'i':
                    imgExtension= ".png";
                    break;
                case '/':
                    imgExtension= ".jpg";
                    break;
                case 'R':
                    imgExtension = ".gif";
                    break;
            }
            return imgExtension;
        }

        private static string GetRelativeFromPhysicalPath(string path)
        {
            return path.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/").Substring(1);
        }

    }
}