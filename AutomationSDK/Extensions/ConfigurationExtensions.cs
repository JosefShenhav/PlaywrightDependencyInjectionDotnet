using AutomationSDK.Config;
using AutomationSDK.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Playwright;

namespace AutomationSDK.Extensions;

public static class ConfigurationExtensions 
{
    private const bool IS_BASE_APPSETTINGS_OPTIONAL = false;
    private const bool RELOAD_BASE_APPSETTINGS_OM_CHANE = false;
    public const string BASE_APPSETTINGS_FILE_PATH = "appsettings.default.json";

    public static IServiceCollection ConfigurePlaywright(this IServiceCollection services, IConfiguration config)
    {
        PlaywrightBrowserConfig playwrightBrowserConfig = config.GetSection(nameof(PlaywrightBrowserConfig)).Get<PlaywrightBrowserConfig>();
        PlaywrightBrowserContextConfig playwrightBrowserContextConfig  = config.GetSection(nameof(PlaywrightBrowserContextConfig)).Get<PlaywrightBrowserContextConfig>();

        services.AddSingleton(_ => Playwright.CreateAsync().GetAwaiter().GetResult());
        services.AddSingleton(provider => provider.GetRequiredService<IPlaywright>()
            [playwrightBrowserConfig.BrowserType].LaunchAsync(playwrightBrowserConfig).GetAwaiter().GetResult());
        services.AddSingleton(provider => provider.GetRequiredService<IBrowser>().NewContextAsync(playwrightBrowserContextConfig).GetAwaiter().GetResult());
        services.AddSingleton(provider => provider.GetRequiredService<IBrowserContext>().NewPageAsync().GetAwaiter().GetResult());

        return services;
    }

    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        services.AddSingleton<Page, HomePage>();

        return services;
    }

    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<PageNavigatorConfig>(config.GetSection(nameof(PageNavigatorConfig)));
        services.Configure<PlaywrightBrowserConfig>(config.GetSection(nameof(PlaywrightBrowserConfig)));
        services.Configure<PlaywrightBrowserContextConfig>(config.GetSection(nameof(PlaywrightBrowserContextConfig)));

        return services;
    }

    public static IConfigurationBuilder AddDefaultAppsettings(this IConfigurationBuilder builder,
        bool isBaseAppsettingsOptional = IS_BASE_APPSETTINGS_OPTIONAL, bool reloadBaseAppsettingsOnChange = RELOAD_BASE_APPSETTINGS_OM_CHANE) 
    {
        IFileProvider sdkFileProvider = new EmbeddedFileProvider(typeof(Startup).Assembly);
        builder.AddJsonFile(sdkFileProvider, BASE_APPSETTINGS_FILE_PATH, isBaseAppsettingsOptional, reloadBaseAppsettingsOnChange);

        return builder;
    }
}