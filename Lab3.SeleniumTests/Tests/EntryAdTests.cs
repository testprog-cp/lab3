using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class EntryAdTests : TestBase
{
    /// <summary>
    /// The modal ad appears automatically after the page loads and has the expected title.
    /// </summary>
    [Fact]
    public void EntryAd_OnPageLoad_ModalIsDisplayed()
    {
        Open("entry_ad");

        var modal = WaitVisible(By.Id("modal"));

        Assert.True(modal.Displayed);
        Assert.Equal("THIS IS A MODAL WINDOW",
                     modal.FindElement(By.CssSelector(".modal-title h3")).Text.ToUpperInvariant());
        Screenshot();
    }

    /// <summary>
    /// Clicking "Close" in the modal footer hides the modal window.
    /// </summary>
    [Fact]
    public void EntryAd_ClickClose_ModalIsHidden()
    {
        Open("entry_ad");
        var modal = WaitVisible(By.Id("modal"));

        modal.FindElement(By.CssSelector(".modal-footer p")).Click();

        Wait.Until(_ => !modal.Displayed);
        Assert.False(Driver.FindElement(By.Id("modal")).Displayed);
        Screenshot();
    }
}
