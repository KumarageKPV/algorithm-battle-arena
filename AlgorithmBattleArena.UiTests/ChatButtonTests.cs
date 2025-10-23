using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ChatButtonTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/student-dashboard";

    [Fact]
    public void ChatButton_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Should show chat button
        var chatButton = Wait.Until(d => d.FindElement(By.XPath("//button[contains(@class, 'fixed') and .//*[name()='svg']]")));
        Assert.True(chatButton.Displayed);

        // 2. Should be clickable
        Assert.True(chatButton.Enabled);

        // 3. Click to open chat
        chatButton.Click();

        // 4. Chat window should appear after clicking
        var chatWindow = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h3]")));
        Assert.True(chatWindow.Displayed);
    }
}
