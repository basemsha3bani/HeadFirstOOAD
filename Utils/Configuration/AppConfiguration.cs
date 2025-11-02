using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils.Configuration
{
    public class AppConfiguration
    {
        public readonly string _connectionString = string.Empty;
        private readonly IConfigurationSection _jwt;
        private readonly string _EventBusSettingsUri;

        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DbCoreConnectionString").Value;
            var appSetting = root.GetSection("ApplicationSettings");
            _jwt = root.GetSection("JWT");
            _EventBusSettingsUri=root.GetSection("EventBusSettings").GetSection("Uri").Value;   
        }
        public string ConnectionString
        {
            get => _connectionString;
        }

        public IConfigurationSection JWT
        {
            get => _jwt;
        }

        public string EventBusSettingsUri
        {
            get => _EventBusSettingsUri;
        }
    }
}
