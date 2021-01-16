using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Pdf
{
    public interface IBioPdf
    {
        void CreatePdfFile(ApplicationUser user);
    }
}