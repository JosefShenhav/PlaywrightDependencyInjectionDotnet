using Microsoft.Playwright;
using static Microsoft.Playwright.BrowserType;

namespace AutomationSDK.Config;

public class PlaywrightBrowserConfig : BrowserTypeLaunchOptions
{
    public string BrowserType { get; set; } = Chromium;
}