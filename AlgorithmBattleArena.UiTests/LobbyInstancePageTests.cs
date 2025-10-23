using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class LobbyInstancePageTests : BaseTest
{
    private const string LobbyInstancePageUrl = BaseUrl + "/lobby-instance";

    [Fact]
    public void LobbyInstancePage_ComprehensiveUiTest()
    {
        Driver.Navigate().GoToUrl(LobbyInstancePageUrl);

        // 1. Should show player list (at least one PlayerCard)
        var playerCards = Wait.Until(d => d.FindElements(By.XPath("//div[contains(@class, 'PlayerCard') or contains(@class, 'bg-white/10')]")));
        Assert.True(playerCards.Count > 0);

        // 2. Should show chat window input
        var chatInput = Driver.FindElement(By.XPath("//input[@placeholder='Type your message...']"));
        Assert.True(chatInput.Displayed);

        // 3. Should show ProblemBrowserModal trigger (button or icon)
        // This step is a placeholder; actual modal test may require clicking a button

        // 4. If host, should show kick buttons for other players (simulate if possible)
        // This step is a placeholder; actual kick test may require login as host
    }
}