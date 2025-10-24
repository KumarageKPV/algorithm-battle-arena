using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AlgorithmBattleArena.UiTests;

/// <summary>
/// Base test class providing common functionality for all UI tests.
/// Handles WebDriver initialization, cleanup, and common helper methods.
/// </summary>
public abstract class BaseTest : IDisposable
{
    protected IWebDriver Driver { get; private set; }
    protected WebDriverWait Wait { get; private set; }
    protected const string BaseUrl = "http://localhost:5173";
    
    // Common URLs
    protected const string LoginUrl = BaseUrl + "/login";
    protected const string RegisterUrl = BaseUrl + "/register";
    protected const string LandingPageUrl = BaseUrl + "/";
    protected const string AdminDashboardUrl = BaseUrl + "/admin-dashboard";
    protected const string StudentDashboardUrl = BaseUrl + "/student-dashboard";
    protected const string TeacherDashboardUrl = BaseUrl + "/teacher-dashboard";
    protected const string LobbyPageUrl = BaseUrl + "/lobby";
    protected const string LeaderboardPageUrl = BaseUrl + "/leaderboard";

    // Test credentials
    protected const string AdminEmail = "admin@algorithmArena.com";
    protected const string AdminPassword = "Admin@123";
    protected const string StudentEmail = "samudithasamarasinghe@gmail.com";
    protected const string StudentPassword = "@12345678Aa";
    protected const string TeacherEmail = "teacher@algorithmArena.com";
    protected const string TeacherPassword = "Teacher@123";

    protected BaseTest()
    {
        var options = new ChromeOptions();
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument($"--user-data-dir={Path.GetTempPath()}selenium_{Guid.NewGuid()}");
        // options.AddArgument("--headless"); // Uncomment for headless mode
        // options.AddArgument("--disable-web-security"); // For debugging only

        Driver = new ChromeDriver(options);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

        // Preflight: Navigate to base URL and clear any persisted session data for isolation
        try
        {
            Driver.Navigate().GoToUrl(LandingPageUrl);
            WaitForPageLoad();
            ClearBrowserData();
        }
        catch (WebDriverException)
        {
            // If the app isn't running yet, tests may fail later; we still proceed to allow environment-managed startups
        }
    }

    /// <summary>
    /// Ensures the application is responding at BaseUrl by attempting a lightweight load with retries.
    /// </summary>
    protected void EnsureAppIsRunning(int timeoutSeconds = 30)
    {
        var end = DateTime.UtcNow.AddSeconds(timeoutSeconds);
        Exception? lastEx = null;
        while (DateTime.UtcNow < end)
        {
            try
            {
                Driver.Navigate().GoToUrl(LandingPageUrl);
                WaitForPageLoad();
                // Consider the app up if we can find a body element
                var body = Driver.FindElement(By.TagName("body"));
                if (body != null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                lastEx = ex;
                System.Threading.Thread.Sleep(500);
            }
        }
        // If still not available, throw a clear error to help diagnostics
        throw new WebDriverTimeoutException($"BaseUrl '{BaseUrl}' did not respond within {timeoutSeconds}s. Last error: {lastEx?.Message}");
    }

    /// <summary>
    /// Logs in as an admin user with default credentials.
    /// </summary>
    protected void LoginAsAdmin()
    {
        LoginAsAdmin(AdminEmail, AdminPassword);
    }

    /// <summary>
    /// Logs in as an admin user with custom credentials.
    /// </summary>
    protected void LoginAsAdmin(string email, string password)
    {
        Login(email, password);
    }

    /// <summary>
    /// Logs in as a student user with default credentials.
    /// </summary>
    protected void LoginAsStudent()
    {
        LoginAsStudent(StudentEmail, StudentPassword);
    }

    /// <summary>
    /// Logs in as a student user with custom credentials.
    /// </summary>
    protected void LoginAsStudent(string email, string password)
    {
        Login(email, password);
    }

    /// <summary>
    /// Logs in as a teacher user with default credentials.
    /// </summary>
    protected void LoginAsTeacher()
    {
        LoginAsTeacher(TeacherEmail, TeacherPassword);
    }

    /// <summary>
    /// Logs in as a teacher user with custom credentials.
    /// </summary>
    protected void LoginAsTeacher(string email, string password)
    {
        Login(email, password);
    }

    /// <summary>
    /// Generic login method that handles the login flow.
    /// Navigates to login page, enters credentials, submits form, and waits for navigation away from login.
    /// </summary>
    protected void Login(string email, string password)
    {
        // Ensure app is up and start from a clean session
        EnsureAppIsRunning();
        ClearBrowserData();
        Driver.Navigate().GoToUrl(LoginUrl);
        WaitForPageLoad();
        // Locate inputs and submit using multiple robust selectors and handle timeouts gracefully
        var emailSelectors = new List<By>
        {
            By.Name("email"),
            By.CssSelector("input[name='email']"),
            By.Id("email"),
            By.XPath("//input[@type='email']"),
            By.XPath("//input[contains(translate(@placeholder,'EMAIL','email'),'email')]")
        };
        var passwordSelectors = new List<By>
        {
            By.Name("password"),
            By.CssSelector("input[name='password']"),
            By.Id("password"),
            By.XPath("//input[@type='password']"),
            By.XPath("//input[contains(translate(@placeholder,'PASSWORD','password'),'password')]")
        };
        var submitSelectors = new List<By>
        {
            By.CssSelector("button[type='submit']"),
            By.CssSelector("input[type='submit']"),
            By.XPath("//button[contains(., 'Sign In') or contains(., 'Sign in') or contains(., 'Log in') or contains(., 'Login')]")
        };

        var emailInput = WaitForFirstVisibleElement(emailSelectors, timeoutSeconds: 30);
        var passwordInput = WaitForFirstVisibleElement(passwordSelectors, timeoutSeconds: 30);
        var loginButton = WaitForFirstVisibleElement(submitSelectors, timeoutSeconds: 30);

        // Fill and submit
        try { emailInput.Clear(); } catch { /* ignore */ }
        emailInput.SendKeys(email);
        try { passwordInput.Clear(); } catch { /* ignore */ }
        passwordInput.SendKeys(password);

        try
        {
            loginButton.Click();
        }
        catch (ElementClickInterceptedException)
        {
            // Fallback to JS click if something overlays the button
            ExecuteJavaScript("arguments[0].click();", loginButton);
        }

        // Wait until we've navigated away from login or a dashboard element is present
        Wait.Until(d => !d.Url.Contains("/login"));
    }

    /// <summary>
    /// Logs out the current user by navigating to landing page and clearing session.
    /// </summary>
    protected void Logout()
    {
        Driver.Navigate().GoToUrl(LandingPageUrl);
        Driver.Manage().Cookies.DeleteAllCookies();
        Driver.Navigate().Refresh();
    }

    /// <summary>
    /// Waits for an element to be visible on the page.
    /// </summary>
    protected IWebElement WaitForElement(By locator, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(d => d.FindElement(locator));
    }

    /// <summary>
    /// Waits for an element to be clickable.
    /// </summary>
    protected IWebElement WaitForClickableElement(By locator, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(d =>
        {
            try
            {
                var el = d.FindElement(locator);
                return (el.Displayed && el.Enabled) ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });
    }

    /// <summary>
    /// Checks if an element is present on the page (without throwing exception).
    /// </summary>
    protected bool IsElementPresent(By locator)
    {
        try
        {
            Driver.FindElement(locator);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if an element is visible on the page.
    /// </summary>
    protected bool IsElementVisible(By locator)
    {
        try
        {
            var element = Driver.FindElement(locator);
            return element.Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    /// <summary>
    /// Waits for the first visible element among the given selectors. Throws if none appear before timeout.
    /// </summary>
    protected IWebElement WaitForFirstVisibleElement(IEnumerable<By> locators, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(d =>
        {
            foreach (var by in locators)
            {
                try
                {
                    var el = d.FindElement(by);
                    if (el.Displayed)
                        return el;
                }
                catch (NoSuchElementException)
                {
                    // continue trying others
                }
                catch (StaleElementReferenceException)
                {
                    // try again in next poll
                }
            }
            return null!;
        });
    }

    /// <summary>
    /// Scrolls to an element to bring it into view.
    /// </summary>
    protected void ScrollToElement(IWebElement element)
    {
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
    }

    /// <summary>
    /// Executes JavaScript in the browser.
    /// </summary>
    protected object ExecuteJavaScript(string script, params object[] args)
    {
        return ((IJavaScriptExecutor)Driver).ExecuteScript(script, args);
    }

    /// <summary>
    /// Takes a screenshot and saves it to the specified path.
    /// </summary>
    protected void TakeScreenshot(string fileName)
    {
        var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots", fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        screenshot.SaveAsFile(filePath);
    }

    /// <summary>
    /// Waits for the page to load completely.
    /// </summary>
    protected void WaitForPageLoad()
    {
        Wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
    }

    /// <summary>
    /// Clears browser cookies and local storage.
    /// </summary>
    protected void ClearBrowserData()
    {
        Driver.Manage().Cookies.DeleteAllCookies();
        ExecuteJavaScript("window.localStorage.clear();");
        ExecuteJavaScript("window.sessionStorage.clear();");
    }

    /// <summary>
    /// Switches to an iframe by index, name, or element.
    /// </summary>
    protected void SwitchToIFrame(IWebElement frameElement)
    {
        Driver.SwitchTo().Frame(frameElement);
    }

    /// <summary>
    /// Switches back to the default content from an iframe.
    /// </summary>
    protected void SwitchToDefaultContent()
    {
        Driver.SwitchTo().DefaultContent();
    }

    /// <summary>
    /// Handles alert dialogs by accepting or dismissing them.
    /// </summary>
    protected void HandleAlert(bool accept = true)
    {
        try
        {
            var alert = Wait.Until(d => d.SwitchTo().Alert());
            if (accept)
                alert.Accept();
            else
                alert.Dismiss();
        }
        catch (NoAlertPresentException)
        {
            // No alert present, continue
        }
    }

    /// <summary>
    /// Gets the text from an alert dialog.
    /// </summary>
    protected string GetAlertText()
    {
        try
        {
            var alert = Wait.Until(d => d.SwitchTo().Alert());
            return alert.Text;
        }
        catch (NoAlertPresentException)
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Waits for a specific URL to be loaded.
    /// </summary>
    protected void WaitForUrl(string expectedUrl, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
        wait.Until(d => d.Url.Contains(expectedUrl));
    }

    /// <summary>
    /// Waits for text to be present in an element.
    /// </summary>
    protected bool WaitForTextInElement(By locator, string text, int timeoutSeconds = 10)
    {
        try
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d => d.FindElement(locator).Text.Contains(text));
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    /// <summary>
    /// Refreshes the current page.
    /// </summary>
    protected void RefreshPage()
    {
        Driver.Navigate().Refresh();
    }

    /// <summary>
    /// Navigates back in browser history.
    /// </summary>
    protected void NavigateBack()
    {
        Driver.Navigate().Back();
    }

    /// <summary>
    /// Navigates forward in browser history.
    /// </summary>
    protected void NavigateForward()
    {
        Driver.Navigate().Forward();
    }

    /// <summary>
    /// Gets the current page title.
    /// </summary>
    protected string GetPageTitle()
    {
        return Driver.Title;
    }

    /// <summary>
    /// Maximizes the browser window.
    /// </summary>
    protected void MaximizeWindow()
    {
        Driver.Manage().Window.Maximize();
    }

    /// <summary>
    /// Sets the browser window size.
    /// </summary>
    protected void SetWindowSize(int width, int height)
    {
        Driver.Manage().Window.Size = new System.Drawing.Size(width, height);
    }

    public void Dispose()
    {
        System.Threading.Thread.Sleep(2000); // 2 second delay to see test automation
        Driver?.Quit();
        Driver?.Dispose();
    }
}