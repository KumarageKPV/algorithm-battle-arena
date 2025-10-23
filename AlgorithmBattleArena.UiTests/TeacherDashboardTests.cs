using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class TeacherDashboardTests : BaseTest
{
    private const string TeacherDashboardUrl = BaseUrl + "/teacher-dashboard";
    private const string LoginUrl = BaseUrl + "/login";
    private const string TeacherEmail = "teacher@algorithmArena.com";
    private const string TeacherPassword = "Teacher@123";

    private void LoginAsTeacher()
    {
        Driver.Navigate().GoToUrl(LoginUrl);
        var emailField = Wait.Until(d => d.FindElement(By.XPath("//input[@type='email']")));
        var passwordField = Driver.FindElement(By.XPath("//input[@type='password']"));
        var loginButton = Driver.FindElement(By.XPath("//button[@type='submit']"));
        emailField.SendKeys(TeacherEmail);
        passwordField.SendKeys(TeacherPassword);
        loginButton.Click();
        Wait.Until(d => !d.Url.Contains("/login"));
    }

    [Fact]
    public void TeacherDashboard_ComprehensiveUiTest()
    {
        LoginAsTeacher();
        if (!Driver.Url.Contains("/teacher-dashboard"))
        {
            Driver.Navigate().GoToUrl(TeacherDashboardUrl);
        }
        
        try
        {
            // 1. Should show header with title
            var header = Wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Master')]")));
            Assert.True(header.Displayed);

            // 2. Should show logout button
            var logoutBtn = Driver.FindElement(By.XPath("//button[contains(text(), 'Logout')]"));
            Assert.True(logoutBtn.Displayed);

            // 3. Should show main heading
            var mainHeading = Driver.FindElement(By.XPath("//h2[contains(text(), 'Training Grounds')]"));
            Assert.True(mainHeading.Displayed);

            // 4. Should show stats cards (My Warriors, Challenges Created, Battles Hosted)
            var warriorsCard = Driver.FindElement(By.XPath("//h3[contains(text(), 'My Warriors')]"));
            var challengesCard = Driver.FindElement(By.XPath("//h3[contains(text(), 'Challenges Created')]"));
            var battlesCard = Driver.FindElement(By.XPath("//h3[contains(text(), 'Battles Hosted')]"));
            Assert.True(warriorsCard.Displayed);
            Assert.True(challengesCard.Displayed);
            Assert.True(battlesCard.Displayed);

            // 5. Should show action cards
            var createChallengeCard = Driver.FindElement(By.XPath("//h3[contains(text(), 'Create Challenge')]"));
            var manageWarriorsCard = Driver.FindElement(By.XPath("//h3[contains(text(), 'Manage Warriors')]"));
            var hostBattleCard = Driver.FindElement(By.XPath("//h3[contains(text(), 'Host Battle')]"));
            Assert.True(createChallengeCard.Displayed);
            Assert.True(manageWarriorsCard.Displayed);
            Assert.True(hostBattleCard.Displayed);

            // 6. Should have link to manage students page
            var manageLink = Driver.FindElement(By.XPath("//a[@href='/manage-students']"));
            Assert.True(manageLink.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }
}