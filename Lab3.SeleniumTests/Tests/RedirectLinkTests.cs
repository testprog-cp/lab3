using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class RedirectLinkTests : TestBase
{
    /// <summary>
    /// Clicking the "here" link redirects to the Status Codes page.
    /// </summary>
    [Fact]
    public void RedirectLink_Click_OpensStatusCodesPage()
    {
        Open("redirector");

        Driver.FindElement(By.Id("redirect")).Click();

        Wait.Until(d => d.Url.EndsWith("/status_codes"));
        Assert.Equal("Status Codes", Driver.FindElement(By.CssSelector(".example h3")).Text);
        Screenshot();
    }

    /// <summary>
    /// After the redirect, the Status Codes page lists links for codes 200, 301, 404 and 500.
    /// </summary>
    [Fact]
    public void RedirectLink_AfterRedirect_StatusCodeLinksPresent()
    {
        Open("redirector");
        Driver.FindElement(By.Id("redirect")).Click();
        Wait.Until(d => d.Url.EndsWith("/status_codes"));

        var codes = Driver.FindElements(By.CssSelector(".example li a")).Select(a => a.Text).ToList();

        Assert.Equal(new[] { "200", "301", "404", "500" }, codes);
        Screenshot();
    }
}
