using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ProblemBrowserModalTests : BaseTest
{
    private const string SomePageUrl = BaseUrl + "/problems"; // Adjust as needed

    [Fact]
    public void ProblemBrowserModal_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(SomePageUrl);

        // 1. Trigger the Problem Browser modal (simulate if button exists)
        var showModalBtn = Wait.Until(d => d.FindElement(By.Id("show-problem-browser-modal")));
        showModalBtn.Click();

        // 2. Should show modal with title
        var modal = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Browse Problems')]]")));
        Assert.True(modal.Displayed);

        // 3. Wait for problems to load
        Wait.Until(d => d.FindElements(By.XPath("//div[contains(@class, 'bg-slate-700') or contains(@class, 'bg-purple-600')]")));
        var problems = modal.FindElements(By.XPath(".//div[contains(@class, 'bg-slate-700') or contains(@class, 'bg-purple-600')]"));
        Assert.True(problems.Count > 0);

        // 4. Select the first problem
        problems[0].Click();
        Assert.Contains("bg-purple-600", problems[0].GetAttribute("class"));

        // 5. Click Add Problems (submit)
        var addBtn = modal.FindElement(By.XPath(".//button[contains(text(), 'Add Problems')]"));
        addBtn.Click();

        // 6. Modal should close (wait for it to disappear)
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
        bool modalClosed = wait.Until(d =>
        {
            try { d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Browse Problems')]]")); return false; }
            catch (NoSuchElementException) { return true; }
        });
        Assert.True(modalClosed);
    }
}