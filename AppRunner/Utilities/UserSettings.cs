using AppRunner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppRunner.Utilities
{
    public class UserSettings
    {
        public string MsBuildPath = @"""c:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe""";

        public ObservableCollection<string> Workspaces = new ObservableCollection<string>();
        public List<ApplicationViewModel> Applications = new List<ApplicationViewModel>();
    }

    
}
