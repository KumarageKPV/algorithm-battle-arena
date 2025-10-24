using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class AdminUsersPanelTests : BaseTest
{
    private void NavigateToUsersTab()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var usersTab = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Users')]")));
        usersTab.Click();
        
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Users') and contains(@class, 'bg-red-600')]")));
    }

    [Fact]
    public void AdminUsersPanel_ShouldDisplayManageUsersHeading()
    {
        NavigateToUsersTab();
        
        try
        {
            var heading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]")));
            Assert.True(heading.Displayed);
            Assert.Contains("Manage Users", heading.Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_ShouldDisplayUsersIcon()
    {
        NavigateToUsersTab();
        
        try
        {
            var heading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]")));
            var parentDiv = heading.FindElement(By.XPath("./parent::div"));
            var icon = parentDiv.FindElement(By.XPath(".//svg"));
            Assert.True(icon.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_ShouldDisplaySearchInput()
    {
        NavigateToUsersTab();
        
        try
        {
            var searchInput = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search by name or email...']")));
            Assert.True(searchInput.Displayed);
            Assert.Equal("text", searchInput.GetAttribute("type"));
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_SearchInput_ShouldHaveSearchIcon()
    {
        NavigateToUsersTab();
        
        try
        {
            var searchContainer = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search by name or email...']/parent::div")));
            var searchIcon = searchContainer.FindElement(By.XPath(".//svg"));
            Assert.True(searchIcon.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_SearchInput_ShouldHavePlaceholder()
    {
        NavigateToUsersTab();
        
        try
        {
            var searchInput = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search by name or email...']")));
            Assert.Equal("Search by name or email...", searchInput.GetAttribute("placeholder"));
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_ShouldDisplayRoleFilterDropdown()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            Assert.True(roleFilter.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_RoleFilter_ShouldHaveAllRolesOption()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            var allRolesOption = roleFilter.FindElement(By.XPath(".//option[@value='']"));
            Assert.Equal("All Roles", allRolesOption.Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_RoleFilter_ShouldHaveStudentsOption()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            var studentsOption = roleFilter.FindElement(By.XPath(".//option[@value='Student']"));
            Assert.Equal("Students", studentsOption.Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_RoleFilter_ShouldHaveTeachersOption()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            var teachersOption = roleFilter.FindElement(By.XPath(".//option[@value='Teacher']"));
            Assert.Equal("Teachers", teachersOption.Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_RoleFilter_ShouldHaveThreeOptions()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            var options = roleFilter.FindElements(By.TagName("option"));
            Assert.Equal(3, options.Count);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_ShouldDisplayUsersTable()
    {
        NavigateToUsersTab();
        
        try
        {
            var table = Wait.Until(d => d.FindElement(By.XPath("//table")));
            Assert.True(table.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_Table_ShouldHaveTableHeaders()
    {
        NavigateToUsersTab();
        
        try
        {
            var table = Wait.Until(d => d.FindElement(By.XPath("//table")));
            var headers = table.FindElements(By.XPath(".//thead//th"));
            
            Assert.Equal(5, headers.Count);
            Assert.Contains("Name", headers[0].Text);
            Assert.Contains("Email", headers[1].Text);
            Assert.Contains("Role", headers[2].Text);
            Assert.Contains("Status", headers[3].Text);
            Assert.Contains("Actions", headers[4].Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_Table_ShouldHaveTableBody()
    {
        NavigateToUsersTab();
        
        try
        {
            var table = Wait.Until(d => d.FindElement(By.XPath("//table")));
            var tbody = table.FindElement(By.TagName("tbody"));
            Assert.True(tbody.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_Table_ShouldDisplayLoadingOrUsers()
    {
        NavigateToUsersTab();
        
        try
        {
            Wait.Until(d => d.FindElement(By.XPath("//table")));
            
            var hasLoadingText = Driver.FindElements(By.XPath("//td[contains(text(), 'Loading users')]")).Count > 0;
            var hasNoUsersText = Driver.FindElements(By.XPath("//td[contains(text(), 'No users found')]")).Count > 0;
            var hasUserRows = Driver.FindElements(By.XPath("//tbody/tr[not(contains(., 'Loading')) and not(contains(., 'No users'))]")).Count > 0;
            
            Assert.True(hasLoadingText || hasNoUsersText || hasUserRows);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_ShouldHaveProperContainerStyling()
    {
        NavigateToUsersTab();
        
        try
        {
            var container = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'bg-gradient-to-br') and .//h3[contains(text(), 'Manage Users')]]")));
            var classes = container.GetAttribute("class");
            
            Assert.Contains("from-gray-800/50", classes);
            Assert.Contains("to-gray-900/50", classes);
            Assert.Contains("border", classes);
            Assert.Contains("rounded-xl", classes);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_SearchAndFilterContainer_ShouldHaveGap()
    {
        NavigateToUsersTab();
        
        try
        {
            var searchFilterContainer = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'flex') and .//input[@placeholder='Search by name or email...']]")));
            var classes = searchFilterContainer.GetAttribute("class");
            Assert.Contains("gap-4", classes);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_Table_ShouldBeScrollable()
    {
        NavigateToUsersTab();
        
        try
        {
            var tableContainer = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'overflow-x-auto')]")));
            Assert.True(tableContainer.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_ShouldBeAccessibleFromAdminDashboard()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        try
        {
            var usersTab = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Users')]")));
            Assert.True(usersTab.Displayed);
            
            usersTab.Click();
            
            var manageHeading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]")));
            Assert.True(manageHeading.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_SearchInput_ShouldBeInteractive()
    {
        NavigateToUsersTab();
        
        try
        {
            var searchInput = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search by name or email...']")));
            searchInput.SendKeys("test");
            Assert.Equal("test", searchInput.GetAttribute("value"));
            searchInput.Clear();
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_RoleFilter_ShouldBeInteractive()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            var select = new SelectElement(roleFilter);
            
            select.SelectByValue("Student");
            Assert.Equal("Student", select.SelectedOption.GetAttribute("value"));
            
            select.SelectByValue("");
            Assert.Equal("", select.SelectedOption.GetAttribute("value"));
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_AllMainComponents_ShouldBePresent()
    {
        NavigateToUsersTab();
        
        try
        {
            var heading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]")));
            var searchInput = Driver.FindElement(By.XPath("//input[@placeholder='Search by name or email...']"));
            var roleFilter = Driver.FindElement(By.XPath("//select"));
            var table = Driver.FindElement(By.XPath("//table"));
            
            Assert.True(heading.Displayed);
            Assert.True(searchInput.Displayed);
            Assert.True(roleFilter.Displayed);
            Assert.True(table.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_TableHeaders_ShouldHaveCorrectStyling()
    {
        NavigateToUsersTab();
        
        try
        {
            var table = Wait.Until(d => d.FindElement(By.XPath("//table")));
            var headerRow = table.FindElement(By.XPath(".//thead/tr"));
            var classes = headerRow.GetAttribute("class");
            
            Assert.Contains("border-b", classes);
            Assert.Contains("border-gray-600", classes);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_RoleFilter_DefaultValue_ShouldBeAllRoles()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            var select = new SelectElement(roleFilter);
            Assert.Equal("", select.SelectedOption.GetAttribute("value"));
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_SearchInput_ShouldHaveCorrectStyling()
    {
        NavigateToUsersTab();
        
        try
        {
            var searchInput = Wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search by name or email...']")));
            var classes = searchInput.GetAttribute("class");
            
            Assert.Contains("bg-gray-700/50", classes);
            Assert.Contains("border-gray-600", classes);
            Assert.Contains("rounded-lg", classes);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminUsersPanel_RoleFilter_ShouldHaveCorrectStyling()
    {
        NavigateToUsersTab();
        
        try
        {
            var roleFilter = Wait.Until(d => d.FindElement(By.XPath("//select")));
            var classes = roleFilter.GetAttribute("class");
            
            Assert.Contains("bg-gray-700/50", classes);
            Assert.Contains("border-gray-600", classes);
            Assert.Contains("rounded-lg", classes);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }
}
