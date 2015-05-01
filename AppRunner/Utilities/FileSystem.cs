using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Utilities
{
    public static class FileSystem
    {
        public static string GetFirstDirName(string path, string mask, bool createDirectory = false)
        {
            int counter = 1;
            Func<int, string> dirName = i => path + @"\" + String.Format(mask, counter);
            while (Directory.Exists(dirName(counter)))
                counter++;
            var dir = dirName(counter);
            if (createDirectory)
                Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
