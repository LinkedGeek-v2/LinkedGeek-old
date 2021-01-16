using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Pdf
{
    public class BioPdf:IBioPdf
    {
        public string Path{ get; set; }





        private BioPdf()
        {

        }

        public BioPdf(string userId)
        {
           Path = HttpContext.Current.Server.MapPath(@"~/Pdf/UserBios" + $"{userId}.pdf");
        }



        public void CreatePdfFile(ApplicationUser user)
        {

        }


    }
}