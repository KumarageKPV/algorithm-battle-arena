using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ConversationListTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/lobby"; // Adjust as needed

    [Fact]
    public void ConversationList_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Should show conversation list (by heading or class)
        var conversationList = Wait.Until(d => d.FindElement(By.XPath("//*[contains(text(), 'Conversations') or contains(@class, 'ConversationList')]")));
        Assert.True(conversationList.Displayed);

        // 2. Should show at least one conversation
        var conversations = Driver.FindElements(By.XPath("//*[contains(@class, 'ConversationList')]//div[contains(@class, 'cursor-pointer')]"));
        Assert.True(conversations.Count > 0);

        // 3. Select the first conversation
        conversations[0].Click();
        // Should highlight as active (bg-blue-50 or border-blue-200)
        var active = conversations[0].GetAttribute("class");
        Assert.Contains("bg-blue-50", active);

        // 4. If last message preview exists, check it
        try
        {
            var lastMsg = conversations[0].FindElement(By.XPath(".//p[contains(@class, 'text-xs')]"));
            Assert.True(lastMsg.Displayed);
        }
        catch (NoSuchElementException) { /* No last message preview */ }
    }
}