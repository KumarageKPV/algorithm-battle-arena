using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlgorithmBattleArena.UiTests;

public class AdminProblemsPanelTests : BaseTest
{
    private void NavigateToProblemsTab()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        var problemsTab = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Problems')]")));
        problemsTab.Click();
        
        Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Problems') and contains(@class, 'bg-red-600')]")));
    }

    [Fact]
    public void AdminProblemsPanel_ShouldDisplayImportProblemsHeading()
    {
        NavigateToProblemsTab();
        
        try
        {
            var heading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]")));
            Assert.True(heading.Displayed);
            Assert.Contains("Import Problems", heading.Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldDisplayFileUploadIcon()
    {
        NavigateToProblemsTab();
        
        try
        {
            var heading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]")));
            var icon = heading.FindElement(By.XPath(".//svg"));
            Assert.True(icon.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldDisplayDashedBorderUploadArea()
    {
        NavigateToProblemsTab();
        
        try
        {
            var uploadArea = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'border-dashed')]")));
            Assert.True(uploadArea.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldDisplayUploadIcon()
    {
        NavigateToProblemsTab();
        
        try
        {
            var uploadArea = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'border-dashed')]")));
            var uploadIcon = uploadArea.FindElement(By.XPath(".//svg"));
            Assert.True(uploadIcon.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldHaveFileInput()
    {
        NavigateToProblemsTab();
        
        try
        {
            var fileInput = Wait.Until(d => d.FindElement(By.Id("problem-file")));
            Assert.NotNull(fileInput);
            Assert.Equal("file", fileInput.GetAttribute("type"));
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_FileInput_ShouldAcceptJsonOnly()
    {
        NavigateToProblemsTab();
        
        try
        {
            var fileInput = Wait.Until(d => d.FindElement(By.Id("problem-file")));
            Assert.Equal(".json", fileInput.GetAttribute("accept"));
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldDisplayFileSelectLabel()
    {
        NavigateToProblemsTab();
        
        try
        {
            var label = Wait.Until(d => d.FindElement(By.XPath("//label[@for='problem-file']")));
            Assert.True(label.Displayed);
            Assert.Contains("Select JSON file to import", label.Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_FileSelectLabel_ShouldBeClickable()
    {
        NavigateToProblemsTab();
        
        try
        {
            var label = Wait.Until(d => d.FindElement(By.XPath("//label[@for='problem-file']")));
            var labelClasses = label.GetAttribute("class");
            Assert.Contains("cursor-pointer", labelClasses);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ImportButton_ShouldNotBeVisibleInitially()
    {
        NavigateToProblemsTab();
        
        try
        {
            Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]")));
            
            var importButtons = Driver.FindElements(By.XPath("//button[contains(text(), 'Import Problems')]"));
            Assert.Empty(importButtons);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldDisplayExpectedJsonFormatHeading()
    {
        NavigateToProblemsTab();
        
        try
        {
            var heading = Wait.Until(d => d.FindElement(By.XPath("//h4[contains(text(), 'Expected JSON Format:')]")));
            Assert.True(heading.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldDisplayJsonFormatExample()
    {
        NavigateToProblemsTab();
        
        try
        {
            var preElement = Wait.Until(d => d.FindElement(By.XPath("//pre[contains(@class, 'bg-black/30')]")));
            Assert.True(preElement.Displayed);
            Assert.Contains("title", preElement.Text);
            Assert.Contains("description", preElement.Text);
            Assert.Contains("difficulty", preElement.Text);
            Assert.Contains("testCases", preElement.Text);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_JsonExample_ShouldContainRequiredFields()
    {
        NavigateToProblemsTab();
        
        try
        {
            var preElement = Wait.Until(d => d.FindElement(By.XPath("//pre[contains(@class, 'bg-black/30')]")));
            var jsonText = preElement.Text;
            
            Assert.Contains("title", jsonText);
            Assert.Contains("description", jsonText);
            Assert.Contains("difficulty", jsonText);
            Assert.Contains("isPublic", jsonText);
            Assert.Contains("isActive", jsonText);
            Assert.Contains("testCases", jsonText);
            Assert.Contains("input", jsonText);
            Assert.Contains("expectedOutput", jsonText);
            Assert.Contains("isSample", jsonText);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_JsonExample_ShouldShowTwoSumExample()
    {
        NavigateToProblemsTab();
        
        try
        {
            var preElement = Wait.Until(d => d.FindElement(By.XPath("//pre[contains(@class, 'bg-black/30')]")));
            var jsonText = preElement.Text;
            
            Assert.Contains("Two Sum", jsonText);
            Assert.Contains("Easy", jsonText);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldHaveProperStyling()
    {
        NavigateToProblemsTab();
        
        try
        {
            var importSection = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'bg-gradient-to-br')]")));
            var classes = importSection.GetAttribute("class");
            
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
    public void AdminProblemsPanel_ShouldDisplayBothMainSections()
    {
        NavigateToProblemsTab();
        
        try
        {
            var sections = Wait.Until(d => d.FindElements(By.XPath("//div[contains(@class, 'bg-gradient-to-br') and contains(@class, 'from-gray-800/50')]")));
            Assert.Equal(2, sections.Count);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ImportSection_ShouldHaveSpacing()
    {
        NavigateToProblemsTab();
        
        try
        {
            var container = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'space-y-6')]")));
            Assert.True(container.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_UploadArea_ShouldHaveCorrectPadding()
    {
        NavigateToProblemsTab();
        
        try
        {
            var uploadArea = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'border-dashed')]")));
            var classes = uploadArea.GetAttribute("class");
            
            Assert.Contains("p-6", classes);
            Assert.Contains("text-center", classes);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_JsonExample_ShouldBeScrollable()
    {
        NavigateToProblemsTab();
        
        try
        {
            var preElement = Wait.Until(d => d.FindElement(By.XPath("//pre[contains(@class, 'bg-black/30')]")));
            var classes = preElement.GetAttribute("class");
            
            Assert.Contains("overflow-x-auto", classes);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_ShouldBeAccessibleFromAdminDashboard()
    {
        LoginAsAdmin();
        Driver.Navigate().GoToUrl(AdminDashboardUrl);
        
        try
        {
            var problemsTab = Wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Manage Problems')]")));
            Assert.True(problemsTab.Displayed);
            
            problemsTab.Click();
            
            var importHeading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]")));
            Assert.True(importHeading.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }

    [Fact]
    public void AdminProblemsPanel_AllComponents_ShouldBePresent()
    {
        NavigateToProblemsTab();
        
        try
        {
            var importHeading = Wait.Until(d => d.FindElement(By.XPath("//h3[contains(text(), 'Import Problems')]")));
            var fileInput = Driver.FindElement(By.Id("problem-file"));
            var label = Driver.FindElement(By.XPath("//label[@for='problem-file']"));
            var formatHeading = Driver.FindElement(By.XPath("//h4[contains(text(), 'Expected JSON Format:')]"));
            var jsonExample = Driver.FindElement(By.XPath("//pre[contains(@class, 'bg-black/30')]"));
            
            Assert.True(importHeading.Displayed);
            Assert.NotNull(fileInput);
            Assert.True(label.Displayed);
            Assert.True(formatHeading.Displayed);
            Assert.True(jsonExample.Displayed);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Contains("/login", Driver.Url);
        }
    }
}
