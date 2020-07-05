using System.Configuration;
using System.Windows.Forms;

namespace TryWindowsForms
{
    static class Settings
    {
        public static void Write(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static void Write(string key, bool value)
        {
            Write(key, value ? true.ToString() : false.ToString());
        }

        public static string Read(string key)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return config.AppSettings.Settings[key]?.Value;
        }

        public static bool ReadBoolean(string key)
        {
            return bool.Parse(Read(key));
        }
    }
}
