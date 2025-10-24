using OpenQA.Selenium;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ChatButtonTests : BaseTest
{
    private void NavigateToTeacherDashboard()
    {
        LoginAsTeacher();
        Driver.Navigate().GoToUrl(TeacherDashboardUrl);
        WaitForPageLoad();
    }

    [Fact]
    public void ChatButton_ShouldBeVisible_OnTeacherDashboard()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and contains(@class, 'bottom-4') and contains(@class, 'right-4')]"));
        Assert.True(chatButton.Displayed);
    }

    [Fact]
    public void ChatButton_ShouldContainMessageIcon()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and .//*[name()='svg']]"));
        var icon = chatButton.FindElement(By.XPath(".//*[name()='svg']"));
        Assert.True(icon.Displayed);
    }

    [Fact]
    public void ChatButton_ShouldHaveCorrectStyling()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and contains(@class, 'bottom-4') and contains(@class, 'right-4')]"));
        var classes = chatButton.GetAttribute("class");
        Assert.Contains("bg-blue-600", classes);
        Assert.Contains("rounded-full", classes);
        Assert.Contains("z-50", classes);
    }

    [Fact]
    public void ChatButton_Click_ShouldOpenChatWindowOverlay()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and contains(@class, 'bottom-4') and contains(@class, 'right-4')]"));
        chatButton.Click();

        var overlay = WaitForElement(By.XPath("//div[contains(@class, 'fixed') and contains(@class, 'inset-0') and contains(@class, 'bg-black') and contains(@class, 'z-50')]"));
        Assert.True(overlay.Displayed);
    }

    [Fact]
    public void ChatButton_Click_ShouldShowChatWindowContainer()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and contains(@class, 'bottom-4') and contains(@class, 'right-4')]"));
        chatButton.Click();

        var container = WaitForElement(By.XPath("//div[contains(@class, 'bg-white') and contains(@class, 'rounded-lg') and contains(@class, 'shadow-xl')]"));
        Assert.True(container.Displayed);
    }

    [Fact]
    public void ChatWindow_ShouldShowSelectAConversationInitially()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and contains(@class, 'bottom-4') and contains(@class, 'right-4')]"));
        chatButton.Click();

        var selectText = WaitForElement(By.XPath("//div[contains(text(), 'Select a conversation')]"));
        Assert.True(selectText.Displayed);
    }

    [Fact]
    public void ChatWindow_CloseButton_ShouldCloseWindow()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and contains(@class, 'bottom-4') and contains(@class, 'right-4')]"));
        chatButton.Click();

        var closeButton = WaitForElement(By.XPath("//button[.//*[name()='svg' and @height='20' or @width='20']]"));
        closeButton.Click();

        // Overlay should disappear
        Assert.False(IsElementPresent(By.XPath("//div[contains(@class, 'fixed') and contains(@class, 'inset-0') and contains(@class, 'bg-black') and contains(@class, 'z-50')]")));
    }

    [Fact]
    public void ChatButton_ShouldNotShowUnreadBadgeByDefault()
    {
        NavigateToTeacherDashboard();

        var chatButton = WaitForElement(By.XPath("//button[contains(@class, 'fixed') and contains(@class, 'bottom-4') and contains(@class, 'right-4')]"));
        var badges = chatButton.FindElements(By.XPath(".//span[contains(@class, 'absolute') and contains(@class, '-top-2') and contains(@class, '-right-2')]"));
        Assert.Empty(badges);
    }
}
