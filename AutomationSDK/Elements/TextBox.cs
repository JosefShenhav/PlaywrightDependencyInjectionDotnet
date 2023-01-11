using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AutomationSDK.Elements;

public class TextBox : AbstractElement
{
    public TextBox(ILocator locator) : base(locator)
    {
    }

    public TextBox(IPage page, string selector, PageLocatorOptions? options = default) : base(page, selector, options)
    {
    }

    public virtual async Task FillAsync(string value)
    {
        await Locator.FillAsync(value);
    }
}