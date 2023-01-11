using System.Threading.Tasks;
using AutomationSDK.Extensions;
using AutomationSDK.Navigation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace AutomationSDK;

public abstract class SdkPlaywrightBaseTest
{
    protected TestServer TestServer;
    protected IPlaywright Playwright;
    protected IBrowser Browser;
    protected IBrowserContext BrowserContext;
    protected IPage Page;
    protected PageNavigator PageNavigator;

    protected virtual string AppsettingsJsonFilePath => "appsettings.development.json";
    private const bool JSON_FILE_IS_OPTIONAL = false;

    protected virtual async Task BeforeTestAsync()
    {
        TestServer = InitializeTestServer();
        InitializeServices();
    }

    protected virtual async Task AfterTestAsync()
    {
        await Page.CloseAsync();
        await Browser.CloseAsync();
        await BrowserContext.CloseAsync();
        Playwright.Dispose();
    }

    protected virtual TestServer InitializeTestServer()
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddDefaultAppsettings();
        IConfiguration configuration = ConfigureJsonFiles(configurationBuilder).AddEnvironmentVariables().Build();
        IWebHostBuilder webHostBuilder = new WebHostBuilder().UseConfiguration(configuration).UseStartup<Startup>()
            .ConfigureTestServices(ConfigureServices);

        return new TestServer(webHostBuilder);
    }

    protected virtual void ConfigureServices(IServiceCollection service)
    {
    }

    protected virtual IConfigurationBuilder ConfigureJsonFiles(IConfigurationBuilder builder)
    {
        builder.AddJsonFile(AppsettingsJsonFilePath, JSON_FILE_IS_OPTIONAL);

        return builder;
    }

    protected virtual void InitializeServices()
    {
        PageNavigator = TestServer.Services.GetRequiredService<PageNavigator>();
        Playwright = TestServer.Services.GetRequiredService<IPlaywright>();
        Browser = TestServer.Services.GetRequiredService<IBrowser>();
        BrowserContext = TestServer.Services.GetRequiredService<IBrowserContext>();
        Page = TestServer.Services.GetRequiredService<IPage>();
    }
}