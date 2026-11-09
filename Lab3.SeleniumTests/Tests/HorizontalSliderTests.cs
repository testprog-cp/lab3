using Lab3.SeleniumTests.Infrastructure;
using OpenQA.Selenium;

namespace Lab3.SeleniumTests.Tests;

public class HorizontalSliderTests : TestBase
{
    /// <summary>
    /// Each ArrowRight press moves the slider by 0.5; the value label updates accordingly.
    /// </summary>
    [Theory]
    [InlineData(1, "0.5")]
    [InlineData(3, "1.5")]
    [InlineData(6, "3")]
    public void Slider_ArrowRight_ChangesValueByHalfStep(int presses, string expected)
    {
        Open("horizontal_slider");
        var slider = Driver.FindElement(By.CssSelector("input[type='range']"));

        slider.Click();                 // focus the slider
        slider.SendKeys(Keys.Home);     // reset to minimum
        for (var i = 0; i < presses; i++) slider.SendKeys(Keys.ArrowRight);

        Assert.Equal(expected, Driver.FindElement(By.Id("range")).Text);
        Screenshot($"{nameof(Slider_ArrowRight_ChangesValueByHalfStep)}_{presses}");
    }

    /// <summary>
    /// End / Home keys move the slider to the max (5) and min (0) bounds.
    /// </summary>
    [Fact]
    public void Slider_EndAndHome_ReachBoundaries()
    {
        Open("horizontal_slider");
        var slider = Driver.FindElement(By.CssSelector("input[type='range']"));
        var label = Driver.FindElement(By.Id("range"));

        slider.Click();
        slider.SendKeys(Keys.End);
        Assert.Equal("5", label.Text);

        slider.SendKeys(Keys.Home);
        Assert.Equal("0", label.Text);
        Screenshot();
    }
}
