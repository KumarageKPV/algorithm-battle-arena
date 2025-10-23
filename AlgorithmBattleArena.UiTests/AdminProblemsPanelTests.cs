using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class AdminProblemsPanelTests : BaseTest
{
    private const string AdminDashboardUrl = BaseUrl + "/admin-dashboard";

    [Fact]
    public void AdminProblemsPanel_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(AdminDashboardUrl);

        // 1. Should show Import Problems section
        var importHeading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]")));
        Assert.True(importHeading.Displayed);

        // 2. Should show file upload area
        var uploadArea = Driver.FindElement(By.XPath("//label[@for='problem-file']"));
        Assert.True(uploadArea.Displayed);

        // 3. File input should be present
        var fileInput = Driver.FindElement(By.Id("problem-file"));
        Assert.NotNull(fileInput);
    }
}
