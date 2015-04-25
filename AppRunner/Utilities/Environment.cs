using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Utilities
{
    public static class AppEnvironment
    {
        private static string SettingsFileName = @"C:\temp\settings.json";

        public static UserSettings Settings;

        static AppEnvironment()
        {
            Settings = Serializer.Load<UserSettings>(SettingsFileName);
        }

        public static void SaveSettings()
        {
            Serializer.Save(SettingsFileName,Settings);
        }
    }
}
