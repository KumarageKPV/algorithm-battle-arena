using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ContactsSectionTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/lobby"; // Adjust as needed

    [Fact]
    public void ContactsSection_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Should show contacts section (by class or heading)
        var contactsSection = Wait.Until(d => d.FindElement(By.XPath("//*[contains(@class, 'ContactsSection') or contains(text(), 'Contacts') or contains(text(), 'Teachers')]")));
        Assert.True(contactsSection.Displayed);

        // 2. Should show at least one contact (teacher or friend)
        var contacts = Driver.FindElements(By.XPath("//div[contains(@class, 'ContactsSection')]//button | //button[contains(@class, 'contact')]"));
        Assert.True(contacts.Count > 0);

        // 3. Expand chat for first contact
        contacts[0].Click();
        var chatInput = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Type a message...']")));
        Assert.True(chatInput.Displayed);

        // 4. Send a message
        string testMessage = "Hello from Contacts!";
        chatInput.SendKeys(testMessage);
        var sendBtn = Driver.FindElement(By.XPath("//button[contains(@class, 'bg-gradient-to-r') and .//*[name()='svg']]"));
        sendBtn.Click();

        // 5. Should display the sent message
        var message = Wait.Until(d => d.FindElement(By.XPath($"//div[contains(text(), '{testMessage}')]")));
        Assert.True(message.Displayed);

        // 6. Collapse chat (if collapse button exists)
        try
        {
            var collapseBtn = Driver.FindElement(By.XPath("//button[contains(@class, 'chevron') or .//*[name()='svg' and contains(@class, 'chevron')]]"));
            collapseBtn.Click();
        }
        catch (NoSuchElementException) { /* Collapse may not be present */ }
    }
}