using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(GSTFunction.Startup))]
namespace GSTFunction
{
    public class Startup : FunctionsStartup
    {
        public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(config);
            services.AddSingleton<IXmlProcessService, XMLProcessService>();
            return services;

        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.setting.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configReader = configBuilder.Build();

            var GSTPersentage = configReader["GSTpercentage"];
            double convertedGSTPersentage;
            if (String.IsNullOrEmpty(GSTPersentage) || !double.TryParse(GSTPersentage, out convertedGSTPersentage))
            {
                throw new Exception("GST Percentage is not maintained properly in the configuration");
            }
            
            var serviceProvider = ConfigureServices(builder.Services, configReader).BuildServiceProvider(true);
        }

    }
}
