using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class ManageStudentsPageTests : BaseTest
{
    private const string ManageStudentsPageUrl = BaseUrl + "/manage-students";

    [Fact]
    public void ManageStudentsPage_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(ManageStudentsPageUrl);

        // 1. Should show main header
        var header = Wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Manage Warriors')]")));
        Assert.True(header.Displayed);

        // 2. Should show Pending Requests and My Warriors sections
        var pendingHeader = Driver.FindElement(By.XPath("//h2[contains(text(), 'Pending Requests')]"));
        var approvedHeader = Driver.FindElement(By.XPath("//h2[contains(text(), 'My Warriors')]"));
        Assert.True(pendingHeader.Displayed);
        Assert.True(approvedHeader.Displayed);

        // 3. Should show pending students or empty state
        var pendingList = Driver.FindElements(By.XPath("//h2[contains(text(), 'Pending Requests')]/following-sibling::div//div[contains(@class, 'bg-gray-800')]"));
        if (pendingList.Count > 0)
        {
            var acceptBtn = pendingList[0].FindElement(By.XPath(".//button[contains(text(), 'Accept')]"));
            var rejectBtn = pendingList[0].FindElement(By.XPath(".//button[contains(text(), 'Reject')]"));
            Assert.True(acceptBtn.Displayed);
            Assert.True(rejectBtn.Displayed);
        }
        else
        {
            var emptyPending = Driver.FindElement(By.XPath("//p[contains(text(), 'No pending requests')]"));
            Assert.True(emptyPending.Displayed);
        }

        // 4. Should show approved students or empty state
        var approvedList = Driver.FindElements(By.XPath("//h2[contains(text(), 'My Warriors')]/following-sibling::div//div[contains(@class, 'bg-gray-800')]"));
        if (approvedList.Count > 0)
        {
            var name = approvedList[0].FindElement(By.XPath(".//p[@class='font-semibold']"));
            Assert.True(name.Displayed);
        }
        else
        {
            var emptyApproved = Driver.FindElement(By.XPath("//p[contains(text(), 'No approved students yet')]"));
            Assert.True(emptyApproved.Displayed);
        }

        // 5. Should have a back link
        var backLink = Driver.FindElement(By.XPath("//a[contains(@href, '/teacher')]"));
        Assert.True(backLink.Displayed);
        Assert.Contains("Back to Dashboard", backLink.Text);
    }
}