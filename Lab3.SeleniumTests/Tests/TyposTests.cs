using System.Text.RegularExpressions;
using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class TyposTests : TestBase
{
    private const string CorrectText = "Sometimes you'll see a typo, other times you won't.";

    private string SecondParagraph() =>
        Driver.FindElements(By.CssSelector(".example p"))[1].Text.Trim();

    /// <summary>
    /// The page shows the heading and the second paragraph matches the expected sentence
    /// (with or without the random typo).
    /// </summary>
    [Fact]
    public void Typos_PageLoaded_TextMatchesExpectedPattern()
    {
        Open("typos");

        Assert.Equal("Typos", Driver.FindElement(By.CssSelector(".example h3")).Text);
        Assert.Matches(new Regex(@"^Sometimes you'll see a typo, other times you won.t\.$"), SecondParagraph());
        Screenshot();
    }

    /// <summary>
    /// Reloads the page several times and detects the typo: the text differs between loads
    /// and one of the variants is the correct spelling.
    /// </summary>
    [Fact]
    public void Typos_SeveralReloads_TypoIsDetected()
    {
        Open("typos");
        var seen = new HashSet<string>();
        for (var i = 0; i < 20 && seen.Count < 2; i++)
        {
            seen.Add(SecondParagraph());
            if (seen.Count < 2) Driver.Navigate().Refresh();
        }

        Assert.Contains(CorrectText, seen);
        Assert.True(seen.Count > 1, "Typo was not reproduced in 20 reloads");
        Assert.Contains(seen, t => t != CorrectText);   // the typo variant
        Screenshot();
    }
}
