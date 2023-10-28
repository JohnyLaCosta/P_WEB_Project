using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class Constants
    {
        private static string _path = "~/Images/";
        private static float _profit = 0.1F;
        private static int _accountID = 1;

        public static string Path
        {
            get
            {
                return _path;
            }
            set
            {
                return;
            }
        }

        public static float Profit
        {
            get
            {
                return _profit;
            }
            set
            {
                return;
            }
        }

        public static int AccountId
        {
            get
            {
                return _accountID;
            }
            set
            {
                return;
            }
        }

        public static string CreateFile(HttpPostedFileBase file, string path, string originalPath)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
            string extension = System.IO.Path.GetExtension(file.FileName);

            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string temp = System.IO.Path.Combine(path, fileName);

            file.SaveAs(temp);

            return originalPath + fileName;
        }
    }
}