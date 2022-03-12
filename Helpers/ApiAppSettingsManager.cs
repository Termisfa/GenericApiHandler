using System.Configuration;

namespace CryptoAlertsBot
{
    public static class ApiAppSettingsManager
    {
        public static string GetDiscordBotKey()
        {
            return ConfigurationManager.AppSettings.Get("DiscordBotKey");
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("ConnectionString");
        }

        public static string GetCryptoAllertsDiscordServerId()
        {
            return ConfigurationManager.AppSettings.Get("CryptoAllertsDiscordServerId");
        }

        public static string GetApiBaseAddress()
        {
            return ConfigurationManager.AppSettings.Get("ApiBaseAddress");
        }

        public static string GetApiBaseUri()
        {
            return ConfigurationManager.AppSettings.Get("ApiBaseUri");
        }

        public static string GetApiDefaultSchema()
        {
            return ConfigurationManager.AppSettings.Get("ApiDefaultSchema");
        }

        public static string GetSchemaUserName()
        {
            return ConfigurationManager.AppSettings.Get("SchemaUserName");
        }

        public static string GetSchemaUserPassw()
        {
            return ConfigurationManager.AppSettings.Get("SchemaUserPassw");
        }
    }
}
