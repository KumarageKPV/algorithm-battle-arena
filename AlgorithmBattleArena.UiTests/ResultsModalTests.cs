using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ResultsModalTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/match"; // Adjust as needed

    [Fact]
    public void ResultsModal_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Trigger the Results modal (simulate if button exists)
        try
        {
            var showModalBtn = Wait.Until(d => d.FindElement(By.Id("show-results-modal")));
            showModalBtn.Click();
        }
        catch (WebDriverTimeoutException) { /* Modal may be triggered by other means */ }
        catch (NoSuchElementException) { /* Modal may be triggered by other means */ }

        // 2. Should show modal with title
        var modal = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Submission Results')]]")));
        Assert.True(modal.Displayed);

        // 3. Should show score summary
        var scoreLabel = modal.FindElement(By.XPath(".//span[contains(text(), 'Overall Score')]"));
        Assert.True(scoreLabel.Displayed);
        var scoreValue = modal.FindElement(By.XPath(".//span[contains(@class, 'text-2xl') and contains(text(), '%')]"));
        Assert.True(scoreValue.Displayed);

        // 4. Should show test cases section
        var testCasesHeading = modal.FindElement(By.XPath(".//h3[contains(text(), 'Test Cases')]"));
        Assert.True(testCasesHeading.Displayed);

        // 5. Should show at least one test case result
        var testCases = modal.FindElements(By.XPath(".//div[contains(@class, 'border-green-500') or contains(@class, 'border-red-500')]"));
        Assert.True(testCases.Count > 0);

        // 6. Should show action buttons
        var closeBtn = modal.FindElement(By.XPath(".//button[contains(text(), 'Close')]"));
        Assert.True(closeBtn.Displayed);

        // 7. Close the modal
        closeBtn.Click();

        // 8. Modal should close (wait for it to disappear)
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
        bool modalClosed = wait.Until(d =>
        {
            try { d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Submission Results')]]")); return false; }
            catch (NoSuchElementException) { return true; }
        });
        Assert.True(modalClosed);
    }
}