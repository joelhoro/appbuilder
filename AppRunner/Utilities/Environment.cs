using System.IO;

namespace AppRunner.Utilities
{
    public static class AppEnvironment
    {
        public static string SettingsFileName = @"C:\temp\settings.json";

        public static UserSettings Settings;

        static AppEnvironment()
        {
            LoadSettings();
            FileSystem.Initialize(Settings.Workspaces);
        }
        public static void LoadSettings()
        {
            if (!File.Exists(SettingsFileName))
            {
                Settings = new UserSettings();
                SaveSettings();
            }
            else
                Settings = Serializer.Load<UserSettings>(SettingsFileName);
        }

        public static void SaveSettings()
        {
            Serializer.Save(SettingsFileName,Settings);
        }
    }
}
