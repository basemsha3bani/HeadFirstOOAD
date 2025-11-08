using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.JWTConfiguration
{
    public class JWTPopulator
    {
        public void PopulateJWTFromConfig(JWT jwt, IConfigurationSection root)
        {
            jwt.DurationInDays = Convert.ToDouble(root.GetSection("DurationInDays").Value);
            jwt.Audience = root.GetSection("Audience").Value;
            jwt.Issuer = root.GetSection("Issuer").Value;
            jwt.Key = root.GetSection("Key").Value;
        }

        public void PopulateJWTFromSecretValues(JWT jwt, SecretClient secretClient)
        {
            KeyVaultSecret duration = secretClient.GetSecret("Duration");
            jwt.DurationInDays = double.Parse(duration.Value);
            KeyVaultSecret Audience = secretClient.GetSecret("Audience");
            jwt.Audience = (duration.Value);
            KeyVaultSecret issuer = secretClient.GetSecret("Issuer");
            jwt.Issuer = (issuer.Value);
            KeyVaultSecret Key = secretClient.GetSecret("Key");

            jwt.Key = Key.Value;

        }
    }
}
