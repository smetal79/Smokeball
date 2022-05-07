using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smokeball.Seo.Ui.Services;
using System;
using System.Windows;

namespace Smokeball.Seo.Ui.Infrastructure
{
    internal static class HostExtensions
    {
        /// <summary>
        /// Builds and gets the program abstraction with services configured in the DI container
        /// </summary>
        /// <param name="_"></param>
        /// <param name="configuration"><see cref="IConfigurationRoot"</param>
        /// <returns></returns>
        public static IHost GetHost(this Application _, IConfigurationRoot configuration)
            => CreateHostBuilder(configuration).Build();

        private static IHostBuilder CreateHostBuilder(IConfigurationRoot configuration)
            => Host
                .CreateDefaultBuilder(Array.Empty<string>())
                .ConfigureServices(services =>
                {
                    services.AddHttpClient<IFetchSeoStatus, FetchSeoStatus>(httpClient =>
                    {
                        var searchBaseUri = configuration.GetValue<string>("SeoBaseUri");
                        httpClient.BaseAddress = new Uri(searchBaseUri);
                    });
                    services.AddSingleton<MainWindow>();
                });
    }
}
