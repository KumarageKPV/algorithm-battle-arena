using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ChatWindowTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/lobby"; // Adjust as needed

    [Fact]
    public void ChatWindow_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Open chat window (simulate if button exists)
        try
        {
            var chatBtn = Wait.Until(d => d.FindElement(By.Id("open-chat-btn")));
            chatBtn.Click();
        }
        catch (WebDriverTimeoutException) { /* Chat may be open by default */ }
        catch (NoSuchElementException) { /* Chat may be open by default */ }

        // 2. Should show conversation list
        var conversationList = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'ConversationList')]")));
        Assert.True(conversationList.Displayed);

        // 3. Select a conversation if not already selected
        var conversations = Driver.FindElements(By.XPath("//div[contains(@class, 'ConversationList')]//button"));
        if (conversations.Count > 0)
        {
            conversations[0].Click();
        }

        // 4. Should show message input
        var chatInput = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Type a message...']")));
        Assert.True(chatInput.Displayed);

        // 5. Send a message
        string testMessage = "Hello from Selenium!";
        chatInput.SendKeys(testMessage);
        var sendBtn = Driver.FindElement(By.XPath("//button[contains(text(), 'Send')]"));
        sendBtn.Click();

        // 6. Should display the sent message
        var message = Wait.Until(d => d.FindElement(By.XPath($"//div[contains(text(), '{testMessage}')]")));
        Assert.True(message.Displayed);

        // 7. Close chat window
        var closeBtn = Driver.FindElement(By.XPath("//button[contains(@class, 'text-gray-500') or contains(@aria-label, 'close')]"));
        closeBtn.Click();
    }
}