using System.Threading.Tasks;
using AutomationSDK.NUnit;
using AutomationSDK.Pages;
using NUnit.Framework;

namespace AutomationTests.NUnit;

public class GoogleTestsNUnit : PlaywrightBaseTest
{
    [Test]
    public async Task WhenGivenThenNUnit()
    {
        HomePage homePage = await PageNavigator.NavigateToAsync<HomePage>();
        await homePage.Search("Change it!");
    }
}
