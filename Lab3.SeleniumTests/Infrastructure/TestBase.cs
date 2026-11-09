using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Lab3.SeleniumTests.Infrastructure;

/// <summary>
/// Base class for all UI tests: creates a fresh Chrome instance per test,
/// provides explicit waits, screenshots and a download folder.
/// </summary>
public abstract class TestBase : IDisposable
{
    protected const string BaseUrl = "https://the-internet.herokuapp.com";
    protected static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);

    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;
    protected readonly string DownloadDir;

    protected TestBase()
    {
        DownloadDir = Path.Combine(Path.GetTempPath(), "lab3-downloads", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(DownloadDir);

        var options = new ChromeOptions();
        if (Environment.GetEnvironmentVariable("HEADED") != "1")
            options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1366,900");
        options.AddUserProfilePreference("download.default_directory", DownloadDir);
        options.AddUserProfilePreference("download.prompt_for_download", false);

        Driver = new ChromeDriver(options);
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        Wait = new WebDriverWait(Driver, DefaultTimeout);
        Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
    }

    /// <summary>Opens a page of the-internet by its relative path.</summary>
    protected void Open(string path) => Driver.Navigate().GoToUrl($"{BaseUrl}/{path.TrimStart('/')}");

    /// <summary>Explicit wait until the element exists and is displayed.</summary>
    protected IWebElement WaitVisible(By by) =>
        Wait.Until(d => d.FindElement(by) is { Displayed: true } e ? e : null)!;

    /// <summary>Waits for a JavaScript alert and switches to it.</summary>
    protected IAlert WaitAlert() =>
        Wait.Until(d =>
        {
            try { return d.SwitchTo().Alert(); }
            catch (NoAlertPresentException) { return null; }
        })!;

    /// <summary>Saves a PNG screenshot to the Screenshots folder next to the test assembly.</summary>
    protected void Screenshot([CallerMemberName] string name = "")
    {
        var dir = Path.Combine(AppContext.BaseDirectory, "Screenshots");
        Directory.CreateDirectory(dir);
        ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(Path.Combine(dir, name + ".png"));
    }

    public void Dispose()
    {
        Driver.Quit();
        try { Directory.Delete(DownloadDir, true); } catch { /* ignore */ }
        GC.SuppressFinalize(this);
    }
}
