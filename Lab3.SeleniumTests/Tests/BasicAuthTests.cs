using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class BasicAuthTests : TestBase
{
    /// <summary>
    /// Valid credentials passed in the URL open the protected page with the success message.
    /// </summary>
    [Fact]
    public void BasicAuth_ValidCredentials_ShowsCongratulations()
    {
        Driver.Navigate().GoToUrl("https://admin:admin@the-internet.herokuapp.com/basic_auth");

        Assert.Equal("Basic Auth", Driver.FindElement(By.CssSelector(".example h3")).Text);
        Assert.Contains("Congratulations! You must have the proper credentials.",
                        Driver.FindElement(By.CssSelector(".example p")).Text);
        Screenshot();
    }

    /// <summary>
    /// Wrong credentials do not give access: the protected content is absent.
    /// </summary>
    [Fact]
    public void BasicAuth_WrongCredentials_AccessDenied()
    {
        Driver.Navigate().GoToUrl("https://admin:wrong@the-internet.herokuapp.com/basic_auth");

        Assert.Empty(Driver.FindElements(By.CssSelector(".example")));
        Assert.DoesNotContain("Congratulations", Driver.PageSource);
        Screenshot();
    }
}
