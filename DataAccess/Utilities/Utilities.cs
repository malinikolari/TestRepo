using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DataAccess.Utilities
{
    public  class Utilities
    {
        public static string GetPath(string directoryPath)
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = null;
            if (outPutDirectory != null) path = Path.Combine(outPutDirectory, directoryPath);
            return path;
        }
    }
}
