using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;

namespace Smokeball.Seo.Ui.Infrastructure
{
    internal static class Configuration
    {
        /// <summary>
        /// Gets the configuration root for the application
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public static IConfigurationRoot GetConfiguration(this Application _)
            => GetConfigurationBuilder().Build();

        private static IConfigurationBuilder GetConfigurationBuilder()
            => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    }
}
