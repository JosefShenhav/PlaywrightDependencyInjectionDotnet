using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutomationSDK.Extensions;
using AutomationSDK.Navigation;

namespace AutomationSDK;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) 
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        services.AddSingleton<PageNavigator>();
        services.ConfigurePlaywright(Configuration);
        services.AddConfiguration(Configuration);
        services.AddPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {}
}