using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class LobbyChatSidebarTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/lobby"; // Adjust as needed

    [Fact]
    public void LobbyChatSidebar_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Open the sidebar if closed (click floating button)
        try
        {
            var openBtn = Wait.Until(d => d.FindElement(By.XPath("//button[.//*[name()='svg' and @data-icon='message-circle']]")));
            openBtn.Click();
        }
        catch (WebDriverTimeoutException) { /* Sidebar may be open by default */ }
        catch (NoSuchElementException) { /* Sidebar may be open by default */ }

        // 2. Should show sidebar header
        var header = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Lobby Chat')]")));
        Assert.True(header.Displayed);

        // 3. Should show message input
        var chatInput = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Type a message...']")));
        Assert.True(chatInput.Displayed);

        // 4. Send a message
        string testMessage = "Hello from Lobby Sidebar!";
        chatInput.SendKeys(testMessage);
        var sendBtn = Driver.FindElement(By.XPath("//button[contains(@class, 'bg-gradient-to-r') and .//*[name()='svg']]"));
        sendBtn.Click();

        // 5. Should display the sent message
        var message = Wait.Until(d => d.FindElement(By.XPath($"//div[contains(text(), '{testMessage}')]")));
        Assert.True(message.Displayed);

        // 6. Close the sidebar
        var closeBtn = Driver.FindElement(By.XPath("//button[.//*[name()='svg' and @data-icon='x']]"));
        closeBtn.Click();
    }
}