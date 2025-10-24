using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AlgorithmBattleArena.UiTests;

public class AdminDashboardTests : BaseTest
{
    [Fact]
    public void AdminDashboard_ShouldLoadAfterLogin()
    {
        LoginAsAdmin();
        
        if (!Driver.Url.Contains("/admin-dashboard"))
        {
            Driver.Navigate().GoToUrl(AdminDashboardUrl);
        }
        
        var header = Wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Guardian Command')]")));
        Assert.True(header.Displayed);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayHeaderWithShieldIcon()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        // Be robust to icon class differences across lucide-react versions: assert an SVG exists in the header next to the title
        var header = Wait.Until(d => d.FindElement(By.XPath("//header")));
        var headerTitle = header.FindElement(By.XPath(".//h1[contains(text(), 'Guardian Command')]"));
        var iconSvg = header.FindElement(By.XPath(".//*[name()='svg']"));

        Assert.True(headerTitle.Displayed);
        Assert.True(iconSvg.Displayed);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayLogoutButton()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var logoutButton = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Logout')]")));
        Assert.True(logoutButton.Displayed);
        Assert.True(logoutButton.Enabled);
    }

    [Fact]
    public void AdminDashboard_LogoutButton_ShouldWork()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var logoutButton = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Logout')]")));
        logoutButton.Click();
        
        Wait.Until(d => d.Url.Contains("/login") || d.Url.Equals(BaseUrl + "/"));
        Assert.True(Driver.Url.Contains("/login") || Driver.Url.Equals(BaseUrl + "/"));
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayArenaControlCenterTitle()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var title = Wait.Until(d => d.FindElement(By.XPath("//h2[contains(text(), 'Arena Control Center')]")));
        Assert.True(title.Displayed);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayTotalWarriorsCard()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var usersIcon = Wait.Until(d => d.FindElement(By.XPath("//*[name()='svg' and contains(@class, 'lucide-users')]")));
        var warriorsTitle = Driver.FindElement(By.XPath("//h3[contains(text(), 'Total Warriors')]"));
        var warriorsCount = Driver.FindElement(By.XPath("//p[contains(text(), '1,247')]"));
        
        Assert.True(usersIcon.Displayed);
        Assert.True(warriorsTitle.Displayed);
        Assert.True(warriorsCount.Displayed);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayMastersCard()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var crownIcon = Wait.Until(d => d.FindElement(By.XPath("//*[name()='svg' and contains(@class, 'lucide-crown')]")));
        var mastersTitle = Driver.FindElement(By.XPath("//h3[contains(text(), 'Masters')]"));
        var mastersCount = Driver.FindElement(By.XPath("//p[contains(text(), '89')]"));
        
        Assert.True(crownIcon.Displayed);
        Assert.True(mastersTitle.Displayed);
        Assert.True(mastersCount.Displayed);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayActiveBattlesCard()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var trophyIcon = Wait.Until(d => d.FindElement(By.XPath("//*[name()='svg' and contains(@class, 'lucide-trophy')]")));
        var battlesTitle = Driver.FindElement(By.XPath("//h3[contains(text(), 'Active Battles')]"));
        var battlesCount = Driver.FindElement(By.XPath("//p[contains(text(), '23')]"));
        
        Assert.True(trophyIcon.Displayed);
        Assert.True(battlesTitle.Displayed);
        Assert.True(battlesCount.Displayed);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayBattlesTodayCard()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var chartIcon = Wait.Until(d => d.FindElement(By.XPath("//*[name()='svg' and contains(@class, 'lucide-bar-chart-3')]")));
        var todayTitle = Driver.FindElement(By.XPath("//h3[contains(text(), 'Battles Today')]"));
        var todayCount = Driver.FindElement(By.XPath("//p[contains(text(), '156')]"));
        
        Assert.True(chartIcon.Displayed);
        Assert.True(todayTitle.Displayed);
        Assert.True(todayCount.Displayed);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayManageUsersSection()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var usersIcon = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'rounded-xl')]//h3[contains(text(), 'Manage Users')]/preceding-sibling::*[name()='svg']")));
        var title = Driver.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]"));
        var description = Driver.FindElement(By.XPath("//p[contains(text(), 'Control warriors and masters')]"));
        var accessButton = Driver.FindElement(By.XPath("//button[contains(text(), 'Access')]"));
        
        Assert.True(usersIcon.Displayed);
        Assert.True(title.Displayed);
        Assert.True(description.Displayed);
        Assert.True(accessButton.Displayed);
        Assert.True(accessButton.Enabled);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayBattleOversightSection()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var trophyIcon = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'rounded-xl')]//h3[contains(text(), 'Battle Oversight')]/preceding-sibling::*[name()='svg']")));
        var title2 = Driver.FindElement(By.XPath("//h3[contains(text(), 'Battle Oversight')]"));
        var description2 = Driver.FindElement(By.XPath("//p[contains(text(), 'Monitor all arena activities')]"));
        var monitorButton = Driver.FindElement(By.XPath("//button[contains(text(), 'Monitor')]"));
        
        Assert.True(trophyIcon.Displayed);
        Assert.True(title2.Displayed);
        Assert.True(description2.Displayed);
        Assert.True(monitorButton.Displayed);
        Assert.True(monitorButton.Enabled);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayProblemLibrarySection()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var fileIcon2 = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'rounded-xl')]//h3[contains(text(), 'Problem Library')]/preceding-sibling::*[name()='svg']")));
        var title3 = Driver.FindElement(By.XPath("//h3[contains(text(), 'Problem Library')]"));
        var description3 = Driver.FindElement(By.XPath("//p[contains(text(), 'Import and manage challenges')]"));
        var manageButton = Driver.FindElement(By.XPath("//button[contains(text(), 'Manage')]"));
        
        Assert.True(fileIcon2.Displayed);
        Assert.True(title3.Displayed);
        Assert.True(description3.Displayed);
        Assert.True(manageButton.Displayed);
        Assert.True(manageButton.Enabled);
    }

    [Fact]
    public void AdminDashboard_ActionButtons_ShouldBeClickable()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var accessButton2 = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Access')]")));
        var monitorButton2 = Driver.FindElement(By.XPath("//button[contains(text(), 'Monitor')]"));
        var manageButton2 = Driver.FindElement(By.XPath("//h3[contains(text(), 'Problem Library')]/following-sibling::p/following-sibling::button[contains(text(), 'Manage')]"));
        
        Assert.True(accessButton2.Enabled);
        Assert.True(monitorButton2.Enabled);
        Assert.True(manageButton2.Enabled);
        
        accessButton2.Click();
        System.Threading.Thread.Sleep(500);
        
        monitorButton2.Click();
        System.Threading.Thread.Sleep(500);
        
        manageButton2.Click();
        System.Threading.Thread.Sleep(500);
    }

    [Fact]
    public void AdminDashboard_ShouldRedirectToLoginWhenNotAuthenticated()
    {
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        Wait.Until(d => d.Url.Contains("/login"));
        Assert.Contains("/login", Driver.Url);
    }

    [Fact]
    public void AdminDashboard_ShouldHaveCorrectGradientBackground()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var mainContainer = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'min-h-screen') and contains(@class, 'bg-gradient-to-br')]")));
        Assert.True(mainContainer.Displayed);
    }

    [Fact]
    public void AdminDashboard_StatisticsCards_ShouldHaveCorrectColors()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var redCard = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'from-red-800')]")));
        var purpleCard = Driver.FindElement(By.XPath("//div[contains(@class, 'from-purple-800')]"));
        var blueCard = Driver.FindElement(By.XPath("//div[contains(@class, 'from-blue-800')]"));
        var greenCard = Driver.FindElement(By.XPath("//div[contains(@class, 'from-green-800')]"));
        
        Assert.True(redCard.Displayed);
        Assert.True(purpleCard.Displayed);
        Assert.True(blueCard.Displayed);
        Assert.True(greenCard.Displayed);
    }

    [Fact]
    public void AdminDashboard_ResponsiveDesign_ShouldWork()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        Driver.Manage().Window.Size = new System.Drawing.Size(768, 1024);
        
        var header2 = Wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Guardian Command')]")));
        Assert.True(header2.Displayed);
        
        Driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayAllStatisticsInCorrectOrder()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var statsGrid = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'grid') and contains(@class, 'md:grid-cols-4')]")));
        var statCards = statsGrid.FindElements(By.XPath(".//div[contains(@class, 'bg-gradient-to-br')]"));
        
        Assert.Equal(4, statCards.Count);
        
        Assert.Contains("Total Warriors", statCards[0].Text);
        Assert.Contains("Masters", statCards[1].Text);
        Assert.Contains("Active Battles", statCards[2].Text);
        Assert.Contains("Battles Today", statCards[3].Text);
    }

    [Fact]
    public void AdminDashboard_ShouldDisplayAllActionSectionsInCorrectOrder()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var actionsGrid = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'grid') and contains(@class, 'lg:grid-cols-3')]")));
        var actionCards = actionsGrid.FindElements(By.XPath(".//div[contains(@class, 'bg-gradient-to-br')]"));
        
        Assert.Equal(3, actionCards.Count);
        
        Assert.Contains("Manage Users", actionCards[0].Text);
        Assert.Contains("Battle Oversight", actionCards[1].Text);
        Assert.Contains("Problem Library", actionCards[2].Text);
    }

    [Fact]
    public void AdminDashboard_TabNavigation_ShouldDisplayOverviewByDefault()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var overviewTab = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Overview') and contains(@class, 'bg-red-600')]")));
        Assert.True(overviewTab.Displayed);
        
        var statsGrid2 = Driver.FindElement(By.XPath("//div[contains(@class, 'grid') and contains(@class, 'md:grid-cols-4')]"));
        Assert.True(statsGrid2.Displayed);
    }

    [Fact]
    public void AdminDashboard_TabNavigation_ShouldShowUsersTab()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var usersTab = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Users')]")));
        usersTab.Click();
        
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Users') and contains(@class, 'bg-red-600')]")));
        
        var usersPanel = Driver.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]"));
        Assert.True(usersPanel.Displayed);
    }

    [Fact]
    public void AdminDashboard_TabNavigation_ShouldShowProblemsTab()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var problemsTab = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Problems')]")));
        problemsTab.Click();
        
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Problems') and contains(@class, 'bg-red-600')]")));
        
        var problemsPanel = Driver.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]"));
        Assert.True(problemsPanel.Displayed);
    }

    [Fact]
    public void AdminDashboard_TabNavigation_ShouldSwitchBetweenTabs()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var overviewTab3 = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Overview')]")));
        var usersTab3 = Driver.FindElement(By.XPath("//button[contains(text(), 'Manage Users')]"));
        var problemsTab3 = Driver.FindElement(By.XPath("//button[contains(text(), 'Manage Problems')]"));
        
        usersTab3.Click();
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Users') and contains(@class, 'bg-red-600')]")));
        
        problemsTab3.Click();
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Problems') and contains(@class, 'bg-red-600')]")));
        
        overviewTab3.Click();
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Overview') and contains(@class, 'bg-red-600')]")));
    }

    [Fact]
    public void AdminDashboard_ManageUsersButton_ShouldSwitchToUsersTab()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var accessBtn = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Access')]")));
        accessBtn.Click();
        
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Users') and contains(@class, 'bg-red-600')]")));
        var usersPanel2 = Driver.FindElement(By.XPath("//h3[contains(text(), 'Manage Users')]"));
        Assert.True(usersPanel2.Displayed);
    }

    [Fact]
    public void AdminDashboard_ProblemLibraryButton_ShouldSwitchToProblemsTab()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var manageBtn = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Problem Library')]/following-sibling::p/following-sibling::button[contains(text(), 'Manage')]")));
        manageBtn.Click();
        
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Problems') and contains(@class, 'bg-red-600')]")));
        var problemsPanel2 = Driver.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]"));
        Assert.True(problemsPanel2.Displayed);
    }
}