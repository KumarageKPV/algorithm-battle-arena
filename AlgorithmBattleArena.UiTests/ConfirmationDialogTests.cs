using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ConfirmationDialogTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/student-dashboard"; // Adjust as needed

    [Fact]
    public void ConfirmationDialog_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Trigger the confirmation dialog (simulate if button exists)
        try
        {
            var showDialogBtn = Wait.Until(d => d.FindElement(By.Id("show-confirmation-dialog")));
            showDialogBtn.Click();
        }
        catch (WebDriverTimeoutException) { /* Dialog may be triggered by other means */ }
        catch (NoSuchElementException) { /* Dialog may be triggered by other means */ }

        // 2. Should show dialog with title and summary
        var dialog = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Leave Match?')]]")));
        Assert.True(dialog.Displayed);
        var score = dialog.FindElement(By.XPath(".//span[contains(text(), '%')]"));
        Assert.True(score.Displayed);
        var problems = dialog.FindElement(By.XPath(".//span[contains(text(), '/') and contains(@class, 'font-semibold')]"));
        Assert.True(problems.Displayed);
        var timer = dialog.FindElement(By.XPath(".//span[contains(text(), ':') and contains(@class, 'font-semibold')]"));
        Assert.True(timer.Displayed);

        // 3. Should show Stay and Leave buttons
        var stayBtn = dialog.FindElement(By.XPath(".//button[contains(text(), 'Stay in Match')]"));
        var leaveBtn = dialog.FindElement(By.XPath(".//button[contains(text(), 'Leave Match')]"));
        Assert.True(stayBtn.Displayed);
        Assert.True(leaveBtn.Displayed);

        // 4. Click Stay in Match and ensure dialog closes
        stayBtn.Click();
        // Wait for dialog to disappear
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
        bool dialogClosed = wait.Until(d =>
        {
            try { d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Leave Match?')]]")); return false; }
            catch (NoSuchElementException) { return true; }
        });
        Assert.True(dialogClosed);
    }
}