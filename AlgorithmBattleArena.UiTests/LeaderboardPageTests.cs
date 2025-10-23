using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class LeaderboardPageTests : BaseTest
{
    private const string LeaderboardPageUrl = BaseUrl + "/leaderboard";

    [Fact]
    public void LeaderboardPage_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(LeaderboardPageUrl);

        // 1. Should show loading state (if fast, may skip)
        try
        {
            var loading = Driver.FindElement(By.XPath("//*[contains(text(), 'Loading leaderboard')]"));
            Assert.True(loading.Displayed);
        }
        catch (NoSuchElementException) { /* Loading may be too fast */ }

        // 2. Should show header and subtitle
        var header = Wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Leaderboard')]")));
        Assert.True(header.Displayed);
        var subtitle = Driver.FindElement(By.XPath("//p[contains(text(), 'Top performers')]"));
        Assert.True(subtitle.Displayed);

        // 3. Should show table headers
        var rankHeader = Driver.FindElement(By.XPath("//th[contains(text(), 'Rank')]"));
        var participantHeader = Driver.FindElement(By.XPath("//th[contains(text(), 'Participant')]"));
        var scoreHeader = Driver.FindElement(By.XPath("//th[contains(text(), 'Total Score')]"));
        var problemsHeader = Driver.FindElement(By.XPath("//th[contains(text(), 'Problems Completed')]"));
        Assert.True(rankHeader.Displayed);
        Assert.True(participantHeader.Displayed);
        Assert.True(scoreHeader.Displayed);
        Assert.True(problemsHeader.Displayed);

        // 4. Should show at least one row or empty state
        var rows = Driver.FindElements(By.XPath("//tbody/tr"));
        if (rows.Count > 0)
        {
            // Check first row for expected columns
            var firstRowCells = rows[0].FindElements(By.TagName("td"));
            Assert.True(firstRowCells.Count >= 4); // Rank, Participant, Score, Problems
            // Check rank badge
            var rankBadge = firstRowCells[0].FindElement(By.XPath(".//span[contains(@class, 'rounded-full')]"));
            Assert.True(rankBadge.Displayed);
            // Check participant email
            Assert.False(string.IsNullOrWhiteSpace(firstRowCells[1].Text));
        }
        else
        {
            // Should show empty state message
            var emptyMsg = Driver.FindElement(By.XPath("//p[contains(text(), 'No leaderboard data available')]"));
            Assert.True(emptyMsg.Displayed);
        }

        // 5. Should have a back link
        var backLink = Driver.FindElement(By.XPath("//a[contains(@href, '/student-dashboard')]"));
        Assert.True(backLink.Displayed);
        Assert.Contains("Back to Home", backLink.Text);
    }
}