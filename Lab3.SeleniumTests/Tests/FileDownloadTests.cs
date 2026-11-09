using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class FileDownloadTests : TestBase
{
    /// <summary>
    /// The page contains a non-empty list of links, each pointing into /download/.
    /// </summary>
    [Fact]
    public void FileDownload_PageLoaded_ListsDownloadLinks()
    {
        Open("download");
        var links = Driver.FindElements(By.CssSelector(".example a"));

        Assert.NotEmpty(links);
        Assert.All(links, l => Assert.Contains("/download/", l.GetDomProperty("href")));
        Screenshot();
    }

    /// <summary>
    /// Clicking a .txt link saves the file to the download folder and the file is not empty.
    /// </summary>
    [Fact]
    public void FileDownload_ClickTxtLink_FileIsSaved()
    {
        Open("download");
        var link = Driver.FindElements(By.CssSelector(".example a"))
                         .First(l => l.Text.EndsWith(".txt"));
        var fileName = link.Text;

        link.Click();

        var path = Path.Combine(DownloadDir, fileName);
        Wait.Until(_ => File.Exists(path) && new FileInfo(path).Length > 0);
        Assert.True(File.Exists(path));
        Assert.True(new FileInfo(path).Length > 0);
        Screenshot();
    }
}
