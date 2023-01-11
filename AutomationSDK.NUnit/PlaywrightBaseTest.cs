using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationSDK.NUnit;

[TestFixture]
public abstract class PlaywrightBaseTest : SdkPlaywrightBaseTest
{
    [SetUp]
    public async Task SetUp()
    {
        await BeforeTestAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await AfterTestAsync();
    }
}