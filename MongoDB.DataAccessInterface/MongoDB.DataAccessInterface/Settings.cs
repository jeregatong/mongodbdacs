using System.Configuration;

namespace MongoDB.DataAccessInterface
{
    public class Settings
    {
        public static string Database
        {
            get { return AppSettingConfig("db"); }
        }

        public static string DataSource
        {
            get { return ConnectionStringConfig("conStr"); }
        }

        private static string AppSettingConfig(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        private static string ConnectionStringConfig(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
