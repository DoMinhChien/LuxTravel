using System;
using Microsoft.Extensions.Configuration;

namespace LuxTravel.Model
{
    public static class QueryHelper
    {

        public static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionStr = string.Empty;
            if (Environment.GetEnvironmentVariable("ENVIRONMENT_VARIABLE") == "Development")
            {
                connectionStr = configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                connectionStr = configuration.GetConnectionString("AzureConnection");
            }

            return connectionStr;

        }
    }
}
