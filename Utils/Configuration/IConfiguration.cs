using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Configuration
{
    public abstract class CustomConfiguration
    {
        public  string _connectionString = string.Empty;
        public  object _jwt;
        public  object _EventBusSettingsUri;

        protected CustomConfiguration()
        {
            this.ReadValues();
        }

        internal abstract void ReadValues();

    }
   public class localConfiguration : CustomConfiguration
    {
        internal override void ReadValues()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DbCoreConnectionString").Value;
            var appSetting = root.GetSection("ApplicationSettings");
            _jwt = root.GetSection("JWT");
            _EventBusSettingsUri = root.GetSection("EventBusSettings").GetSection("Uri").Value;
           
        }
    }

    public class ProductionConfiguration : CustomConfiguration
    {
      

        internal override void ReadValues()
        {
            var keyVaultName = "FirstWebAppDbConnection";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(true));

            KeyVaultSecret secret = client.GetSecret("connectionstring");
            _connectionString = secret.Value;

            keyVaultName = "JWTConfigurations";
            kvUri = $"https://{keyVaultName}.vault.azure.net";
            _jwt = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(true));


            


            keyVaultName = "EventBusSettings";
            kvUri = $"https://{keyVaultName}.vault.azure.net";
            _EventBusSettingsUri = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(true));


        }
    }

}
