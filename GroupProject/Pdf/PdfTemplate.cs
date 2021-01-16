using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Pdf
{
    public class PdfTemplate
    {
        private readonly ApplicationUser _user;

        private PdfTemplate()
        {

        }

        public PdfTemplate(ApplicationUser user)
        {
            _user = user;
        }
    }
}