using System.Threading.Tasks;
using AutomationSDK.MSTest;
using AutomationSDK.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomationTests.MSTest;

[TestClass]
public class GoogleTestsMSTest : PlaywrightBaseTest
{
    [TestMethod]
    public async Task WhenGivenThenMSTest()
    {
        HomePage homePage = await PageNavigator.NavigateToAsync<HomePage>();
        await homePage.Search("Change it!");
    }
}