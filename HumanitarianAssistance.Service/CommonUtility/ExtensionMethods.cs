using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Service.CommonUtility
{
    public static class ExtensionMethods
    {
        public static int RoundOff(this decimal i)
        {
            return ((int)Math.Round((i / 10.0m))) * 10;
        }

        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            if (ext == ".docx" || ext == ".doc")
            {
                mimeType = "application/vnd.google-apps.document";
            }
            else if (ext == ".pdf")
            {
                //mimeType = "application/vnd.google-apps.file";
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                    mimeType = regKey.GetValue("Content Type").ToString();
            }
            else if (ext == ".xlsx" || ext == ".xls" || ext == ".csv")
            {
                mimeType = "application/vnd.google-apps.spreadsheet";
            }
            else if (ext == ".jpeg" || ext == ".png")
            {
                mimeType = "application/vnd.google-apps.photo";
            }
            return mimeType;
        }
    }
}
