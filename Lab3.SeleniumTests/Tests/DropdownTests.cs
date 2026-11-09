using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lab3.SeleniumTests.Tests;

public class DropdownTests : TestBase
{
    /// <summary>
    /// By default the placeholder is selected and disabled; the list has exactly 3 options.
    /// </summary>
    [Fact]
    public void Dropdown_Default_PlaceholderSelectedAndDisabled()
    {
        Open("dropdown");
        var select = new SelectElement(Driver.FindElement(By.Id("dropdown")));

        Assert.Equal("Please select an option", select.SelectedOption.Text);
        Assert.False(select.SelectedOption.Enabled);
        Assert.Equal(new[] { "Please select an option", "Option 1", "Option 2" },
                     select.Options.Select(o => o.Text));
        Screenshot();
    }

    /// <summary>
    /// Selecting an option by visible text makes it the only selected option with the matching value.
    /// </summary>
    [Theory]
    [InlineData("Option 1", "1")]
    [InlineData("Option 2", "2")]
    public void Dropdown_SelectByText_OptionBecomesSelected(string text, string value)
    {
        Open("dropdown");
        var select = new SelectElement(Driver.FindElement(By.Id("dropdown")));

        select.SelectByText(text);

        Assert.Equal(text, select.SelectedOption.Text);
        Assert.Equal(value, select.SelectedOption.GetDomProperty("value"));
        Assert.Single(select.AllSelectedOptions);
        Screenshot($"{nameof(Dropdown_SelectByText_OptionBecomesSelected)}_{value}");
    }
}
