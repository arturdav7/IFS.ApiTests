using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFS.ApiTests.Helpers
{
    public static class ConfigurationHelper
    {
        public static ApiSettings GetSettings()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = new ApiSettings();
            config.GetSection("ApiSettings").Bind(settings);
            return settings;
        }
    }
}
