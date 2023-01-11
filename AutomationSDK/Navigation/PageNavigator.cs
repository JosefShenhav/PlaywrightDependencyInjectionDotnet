using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;

namespace AutomationSDK.Navigation;

public class PageNavigator
{
    private readonly IPage _page;
    private IEnumerable<Page> _pages;
    private readonly PageNavigatorConfig _pageNavigatorConfig;

    public PageNavigator(IPage page, IEnumerable<Page> pages, IOptions<PageNavigatorConfig> pageNavigatorConfig)
    {
        _page = page;
        _pages = pages;
        _pageNavigatorConfig = pageNavigatorConfig.Value;
    }

    public async Task RefreshAsync()
    {
        await _page.ReloadAsync();
    }

    public async Task<T> NavigateToAsync<T>() where T : Page
    {
        await NavigateToPageAsync(ExtractPageRoute<T>());

        return ExtractPage<T>();
    }

    private string ExtractPageRoute<T>()
    {
        return _pageNavigatorConfig.GetType().GetProperties()
            .First(property => property.Name.Contains(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
            .GetValue(_pageNavigatorConfig) as string
            ?? throw new Exception();
    }

    private async Task NavigateToPageAsync(string route)
    {
        await _page.GotoAsync(route);
    }

    private T ExtractPage<T>() where T : Page
    {
        return _pages.First(page => page.GetType() == typeof(T)) as T
        ?? throw new Exception();
    }
}