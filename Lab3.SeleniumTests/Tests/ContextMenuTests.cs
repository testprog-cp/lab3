using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Lab3.SeleniumTests.Tests;

public class ContextMenuTests : TestBase
{
    /// <summary>
    /// Right-click inside the hot spot triggers a JS alert with the expected text;
    /// after accepting it no alert remains.
    /// </summary>
    [Fact]
    public void ContextMenu_RightClickHotSpot_ShowsAlert()
    {
        Open("context_menu");
        var hotSpot = Driver.FindElement(By.Id("hot-spot"));

        new Actions(Driver).ContextClick(hotSpot).Perform();

        var alert = WaitAlert();
        Assert.Equal("You selected a context menu", alert.Text);
        alert.Accept();
        Assert.Throws<NoAlertPresentException>(() => Driver.SwitchTo().Alert());
        Screenshot();
    }

    /// <summary>
    /// A normal left click on the hot spot does not open any alert.
    /// </summary>
    [Fact]
    public void ContextMenu_LeftClickHotSpot_NoAlert()
    {
        Open("context_menu");

        Driver.FindElement(By.Id("hot-spot")).Click();

        Assert.Throws<NoAlertPresentException>(() => Driver.SwitchTo().Alert());
        Screenshot();
    }
}
