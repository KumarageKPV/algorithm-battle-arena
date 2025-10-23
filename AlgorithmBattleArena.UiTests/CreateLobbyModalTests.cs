using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class CreateLobbyModalTests : BaseTest
{
    private const string LobbyPageUrl = BaseUrl + "/lobby";

    [Fact]
    public void CreateLobbyModal_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(LobbyPageUrl);

        // 1. Trigger the Create Lobby modal (simulate if button exists)
        var showModalBtn = Wait.Until(d => d.FindElement(By.Id("create-lobby-btn")));
        showModalBtn.Click();

        // 2. Should show modal with title
        var modal = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Create New Lobby')]]")));
        Assert.True(modal.Displayed);

        // 3. Fill out the form
        var nameInput = modal.FindElement(By.XPath(".//input[@type='text']"));
        nameInput.Clear();
        nameInput.SendKeys("Selenium Test Lobby");

        var modeSelect = modal.FindElement(By.XPath(".//select"));
        var selectElement = new SelectElement(modeSelect);
        selectElement.SelectByValue("Team");

        var maxPlayersInput = modal.FindElement(By.XPath(".//input[@type='number']"));
        maxPlayersInput.Clear();
        maxPlayersInput.SendKeys("5");

        // 4. Click Create Lobby
        var createBtn = modal.FindElement(By.XPath(".//button[contains(text(), 'Create Lobby')]"));
        createBtn.Click();

        // 5. Modal should close (wait for it to disappear)
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
        bool modalClosed = wait.Until(d =>
        {
            try { d.FindElement(By.XPath("//div[contains(@class, 'fixed') and .//h2[contains(text(), 'Create New Lobby')]]")); return false; }
            catch (NoSuchElementException) { return true; }
        });
        Assert.True(modalClosed);
    }
}