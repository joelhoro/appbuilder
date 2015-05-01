using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Utilities
{
    public static class FileSystem
    {
        private static Dictionary<string,List<string>> _solutionscache = new Dictionary<string, List<string>>();

        private static string fileCache = @"C:\temp\filecache";
        public static void Initialize(bool refreshCache = false)
        {
            if (refreshCache)
            {
                var root = @"c:\Users\Joel\Documents\Visual Studio 2013\Projects";
                var files = Directory.GetFiles(root, "*.sln", SearchOption.AllDirectories).ToList();
                _solutionscache = new Dictionary<string, List<string>>();
                _solutionscache[root] = files;
                Serializer.Save(fileCache,_solutionscache);
            }
            else
            {
                if (!File.Exists(fileCache))
                {
                    _solutionscache = new Dictionary<string, List<string>>();
                    Serializer.Save(fileCache,_solutionscache);                    
                }
                _solutionscache = Serializer.Load<Dictionary<string, List<string>>>(fileCache);
            }
        }

        public static List<string> FileList(string path)
        {
            if (_solutionscache.ContainsKey(path))
                return _solutionscache[path];
            else
                return new List<string>();
        }

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
