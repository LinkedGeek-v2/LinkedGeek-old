using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace GroupProject.HubModels
{
    public static class FindPath
    {
        public static string FindOrCreateFolderPath(string senderID, string receiverID)
        {
            string path = HttpContext.Current.Server.MapPath(@"~/ChatLogs/" + $"{senderID}{receiverID}");
            if (!Directory.Exists(path))
            {
                path = HttpContext.Current.Server.MapPath(@"~/ChatLogs/" + $"{receiverID}{senderID}");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            return path;
        }

        public static string FindOrCreateFilePath(string folderPath,DateTime fileName)
        {
            string name = fileName.Date.ToString("d").Replace('/','.') + ".txt";
            string path = $"{folderPath}\\{name}";

            if (File.Exists(path))
                return path;
            else
                File.Create(path).Dispose();

            return path;
        }

        public static IEnumerable<string> GetTxtFileNamesOfFolder(string path)
        {
            return Directory.EnumerateFiles(path);
        }
    }
}