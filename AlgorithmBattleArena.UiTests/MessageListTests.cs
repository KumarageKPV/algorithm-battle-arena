using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class MessageListTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/lobby";

    [Fact]
    public void MessageList_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Should show message list container
        var messageList = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'overflow-y-auto')]")));
        Assert.True(messageList.Displayed);

        // 2. Should show either messages or empty state
        try
        {
            var emptyState = Driver.FindElement(By.XPath("//div[contains(text(), 'No messages yet')]"));
            Assert.True(emptyState.Displayed);
        }
        catch (NoSuchElementException)
        {
            // If no empty state, should have messages
            var messages = Driver.FindElements(By.XPath("//div[contains(@class, 'bg-blue-500') or contains(@class, 'bg-gray-200')]"));
            Assert.True(messages.Count > 0);
        }
    }
}
