using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class DynamicLoadingTests : TestBase
{
    /// <summary>
    /// Example 1: the hidden element becomes visible after Start, the loader disappears.
    /// </summary>
    [Fact]
    public void DynamicLoading_HiddenElement_BecomesVisibleAfterStart()
    {
        Open("dynamic_loading/1");
        Assert.False(Driver.FindElement(By.Id("finish")).Displayed);

        Driver.FindElement(By.CssSelector("#start button")).Click();

        var text = WaitVisible(By.CssSelector("#finish h4"));
        Assert.Equal("Hello World!", text.Text);
        Assert.False(Driver.FindElement(By.Id("loading")).Displayed);
        Screenshot();
    }

    /// <summary>
    /// Example 2: the element does not exist in the DOM before Start and is rendered after loading.
    /// </summary>
    [Fact]
    public void DynamicLoading_RenderedElement_AppearsInDomAfterStart()
    {
        Open("dynamic_loading/2");
        Assert.Empty(Driver.FindElements(By.Id("finish")));

        Driver.FindElement(By.CssSelector("#start button")).Click();

        Assert.Equal("Hello World!", WaitVisible(By.CssSelector("#finish h4")).Text);
        Screenshot();
    }
}
