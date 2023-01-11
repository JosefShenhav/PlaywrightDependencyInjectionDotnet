using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomationSDK.MSTest;

[TestClass]
public abstract class PlaywrightBaseTest : SdkPlaywrightBaseTest
{
    [TestInitialize]
    public async Task SetUp()
    {
        await BeforeTestAsync();
    }

    [TestCleanup]
    public async Task TearDown()
    {
        await AfterTestAsync();
    }
}