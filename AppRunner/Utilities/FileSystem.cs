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
        public static Dictionary<string,int> Initialize(IEnumerable<string> directories, bool refreshCache = false)
        {
            if (refreshCache || !File.Exists(fileCache))
            {
//                var root = @"c:\Users\Joel\Documents\Visual Studio 2013\Projects";
                _solutionscache = new Dictionary<string, List<string>>();
                foreach (var dir in directories)
                {
                    var files = Directory.GetFiles(dir, "*.sln", SearchOption.AllDirectories).ToList();
                    _solutionscache[dir] = files.Select(f => f.Replace(dir, "")).ToList();
                }
                Serializer.Save(fileCache,_solutionscache);
            }
            else
                _solutionscache = Serializer.Load<Dictionary<string, List<string>>>(fileCache);
            return _solutionscache.ToDictionary(elt => elt.Key, elt => elt.Value.Count);
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
