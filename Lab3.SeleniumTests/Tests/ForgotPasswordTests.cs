using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class ForgotPasswordTests : TestBase
{
    /// <summary>
    /// The Forgot Password page shows the e-mail field and the Retrieve password button.
    /// </summary>
    [Fact]
    public void ForgotPassword_PageLoaded_EmailFieldAndButtonAreVisible()
    {
        Open("forgot_password");

        var email = Driver.FindElement(By.Id("email"));
        var button = Driver.FindElement(By.Id("form_submit"));

        Assert.Equal("Forgot Password", Driver.FindElement(By.TagName("h2")).Text);
        Assert.True(email.Displayed && email.Enabled);
        Assert.Equal("Retrieve password", button.Text);
        Screenshot();
    }

    /// <summary>
    /// Submitting a valid e-mail redirects to /email_sent with a confirmation message.
    /// </summary>
    [Fact]
    public void ForgotPassword_SubmitEmail_ShowsEmailSentMessage()
    {
        Open("forgot_password");

        Driver.FindElement(By.Id("email")).SendKeys("student@kpi.ua");
        Driver.FindElement(By.Id("form_submit")).Click();

        Wait.Until(d => d.Url.EndsWith("/email_sent"));
        Assert.Contains("Your e-mail's been sent!", Driver.FindElement(By.Id("content")).Text);
        Screenshot();
    }
}
