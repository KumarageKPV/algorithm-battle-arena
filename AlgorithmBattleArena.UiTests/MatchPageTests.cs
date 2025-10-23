using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class MatchPageTests : BaseTest
{
    private const string MatchPageUrl = BaseUrl + "/match";

    [Fact]
    public void MatchPage_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(MatchPageUrl);

        // 1. Should show code editor (Monaco or textarea fallback)
        var editor = Wait.Until(d => d.FindElement(By.CssSelector(".monaco-editor, textarea, [data-testid='code-editor']")));
        Assert.True(editor.Displayed);

        // 2. Should show at least one problem in the problem list
        var problemTabs = Driver.FindElements(By.XPath("//button[contains(@class, 'problem-tab')]"));
        Assert.True(problemTabs.Count > 0);

        // 3. Should show a submit button
        var submitBtn = Driver.FindElement(By.XPath("//button[contains(text(), 'Submit')]"));
        Assert.True(submitBtn.Displayed);
        Assert.True(submitBtn.Enabled);

        // 4. Should show a run button
        var runBtn = Driver.FindElement(By.XPath("//button[contains(text(), 'Run')]"));
        Assert.True(runBtn.Displayed);
        Assert.True(runBtn.Enabled);

        // 5. Should show chat window
        var chatInput = Driver.FindElement(By.XPath("//input[@placeholder='Type your message...']"));
        Assert.True(chatInput.Displayed);

        // 6. Should show timer/countdown
        var timer = Driver.FindElement(By.XPath("//*[contains(text(), ':') and contains(@class, 'timer')]"));
        Assert.True(timer.Displayed);

        // 7. Should show results modal when triggered (simulate if possible)
        // This step is a placeholder; actual modal test may require triggering a submission

        // 8. Should show confirmation dialog when exiting (simulate if possible)
        // This step is a placeholder; actual dialog test may require clicking exit
    }
}