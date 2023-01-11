using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AutomationSDK.Pages;

public class HomePage : Page
{
    private const string SEARCH_TEXTBOX_SELECTOR = "[name = 'q']";

    private readonly TextBox _searchTextBox;

    public HomePage(IPage page) : base(page)
    {
        _searchTextBox = new TextBox(page, SEARCH_TEXTBOX_SELECTOR);
    }

    public async Task Search(string text)
    {
        await _searchTextBox.FillAsync(text);
    }
}