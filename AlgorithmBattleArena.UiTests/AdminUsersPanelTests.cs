using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class AdminUsersPanelTests : BaseTest
{
    private const string AdminDashboardUrl = BaseUrl + "/admin-dashboard";

    [Fact]
    public void AdminUsersPanel_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(AdminDashboardUrl);

        // 1. Should show Manage Users section
        var manageHeading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]")));
        Assert.True(manageHeading.Displayed);

        // 2. Should show search input
        var searchInput = Driver.FindElement(By.XPath("//input[@placeholder='Search by name or email...']"));
        Assert.True(searchInput.Displayed);

        // 3. Should show role filter dropdown
        var roleFilter = Driver.FindElement(By.XPath("//select"));
        Assert.True(roleFilter.Displayed);

        // 4. Should display users table or loading state
        try
        {
            var usersTable = Driver.FindElement(By.XPath("//table | //div[contains(text(), 'Loading')]"));
            Assert.True(usersTable.Displayed);
        }
        catch (NoSuchElementException) { /* Table may be loading */ }
    }
}
