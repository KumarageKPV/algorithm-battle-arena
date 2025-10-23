using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class MessageInputTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/lobby"; // Adjust as needed

    [Fact]
    public void MessageInput_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Should show message input
        var input = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Type a message...']")));
        Assert.True(input.Displayed);
        Assert.True(input.Enabled);

        // 2. Enter text and send
        string testMessage = "Test message from MessageInput!";
        input.SendKeys(testMessage);
        var sendBtn = Driver.FindElement(By.XPath("//button[contains(@class, 'bg-blue-500') and .//*[name()='svg']]"));
        Assert.True(sendBtn.Enabled);
        sendBtn.Click();

        // 3. Should display the sent message
        var message = Wait.Until(d => d.FindElement(By.XPath($"//div[contains(text(), '{testMessage}')]")));
        Assert.True(message.Displayed);
    }
}